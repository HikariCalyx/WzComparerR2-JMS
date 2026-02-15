using System;
using System.IO;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WzComparerR2
{
    public class Updater
    {
        private const string checkUpdateURL = "https://api.hikaricalyx.com/WcR2-JMS/v1/GetLatestUpdate";

        public Updater()
        {
            string appVersion = Program.ApplicationVersion;
            if (TryParseApplicationVersion(appVersion, out var version))
            {
                this.CurrentVersion = version;
                this.CurrentVersionString = $"{version.Build}.{version.Revision}";
            }
            else
            {
                this.CurrentVersion = default;
                this.CurrentVersionString = appVersion;
            }
        }

        public bool LatestReleaseFetched { get; private set; }
        public bool UpdateAvailable { get; private set; }
        public Version CurrentVersion { get; private set; }
        public Version LatestVersion { get; private set; }
        public string CurrentVersionString { get; private set; }
        public string LatestVersionString { get; private set; }

        public HCTAPIResponse Release { get; private set; }

        private readonly TimeSpan defaultRequestTimeout = TimeSpan.FromSeconds(15);

        public async Task QueryUpdateAsync(CancellationToken cancellationToken = default)
        {
            // send request
            using var client = new HttpClient();
            client.Timeout = defaultRequestTimeout;
            using var request = new HttpRequestMessage(HttpMethod.Get, checkUpdateURL);
            request.Headers.Accept.ParseAdd("application/json");
            request.Headers.UserAgent.ParseAdd($"WzComparerR2-JMS/1.0");
            using var response = await client.SendAsync(request, cancellationToken).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            // parse payload
            using var responseStream = await response.Content.ReadAsStreamAsync();
            using var jsonTextReader = new JsonTextReader(new StreamReader(responseStream));
            var serializer = new JsonSerializer();
            var release = serializer.Deserialize<HCTAPIResponse>(jsonTextReader);

            // reset all
            this.LatestReleaseFetched = true;
            this.Release = release;
            this.Net48Asset = null;
            this.Net6Asset = null;
            this.Net8Asset = null;
            this.Net10Asset = null;

            // check version
            if (TryParseBuildVersion(release.Name, out var releaseVer))
            {
                this.LatestVersion = releaseVer;
                this.LatestVersionString = $"{releaseVer.Build}.{releaseVer.Revision}";
                if (this.CurrentVersion != default)
                {
                    this.UpdateAvailable = (releaseVer.Build > this.CurrentVersion.Build)
                        || (releaseVer.Build == this.CurrentVersion.Build && releaseVer.Revision > this.CurrentVersion.Revision);
                }
            }
            else
            {
                this.LatestVersion = default;
                this.LatestVersionString = release.Name;
            }

            // check assets
            if (release.Assets != null)
            {
                foreach (var asset in release.Assets)
                {
                    if (asset.Name != null)
                    {
                        if (asset.Name.Contains("net6"))
                        {
                            this.Net6Asset = asset;
                        }
                        else if (asset.Name.Contains("net8"))
                        {
                            this.Net8Asset = asset;
                        }
                        else if (asset.Name.Contains("net10"))
                        {
                            this.Net10Asset = asset;
                        }
                        else
                        {
                            this.Net48Asset = asset;
                        }
                    }
                }
            }
        }

        public async Task DownloadAssetAsync(string assetUrl, string fileName, OnProgressCallback onProgress = null, CancellationToken cancellationToken = default)
        {
            // send request
            using var client = new HttpClient();
            client.Timeout = defaultRequestTimeout;
            using var request = new HttpRequestMessage(HttpMethod.Get, assetUrl);
            request.Headers.UserAgent.ParseAdd($"WzComparerR2-JMS/1.0");
            using var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            long fileSize = response.Content.Headers.ContentLength;
            // copy to file
            bool fileCreated = false;
            try
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                using var fs = File.Create(fileName);
                fileCreated = true;
                byte[] buffer = new byte[16 * 1024];
                long downloadedBytes = 0;
                while (true)
                {
                    int len = await responseStream.ReadAsync(buffer, 0, buffer.Length, cancellationToken);
                    if (len <= 0)
                    {
                        break;
                    }
                    fs.Write(buffer, 0, len);
                    downloadedBytes += len;
                    onProgress?.Invoke(downloadedBytes, fileSize);
                }
                await responseStream.CopyToAsync(fs, 16 * 1024, cancellationToken).ConfigureAwait(false);
            }
            catch
            {
                if (fileCreated && File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
                throw;
            }
        }

        private static bool TryParseBuildVersion(string releaseName, out Version result)
        {
            var m = Regex.Match(releaseName, @"(\d{8})\.(\d+)");
            if (m.Success 
                && int.TryParse(m.Groups[1].Value, out int build)
                && int.TryParse(m.Groups[2].Value, out int revision))
            {
                result = new Version(0, 0, build, revision);
                return true;
            }

            result = default;
            return false;
        }

        private static bool TryParseApplicationVersion(string appVersion, out Version result)
        {
            return Version.TryParse(appVersion, out result);
        }

        public delegate void OnProgressCallback(long downloadedBytes, long totalBytes);

        public class HCTAPIResponse
        {
            [JsonProperty("MajorVersion")]
            public string MajorVersion { get; set; }
            [JsonProperty("BuildNumber")]
            public string NuildNumber { get; set; }
            [JsonProperty("ChangeTitle")]
            public string ChangeTitle { get; set; }
            [JsonProperty("Changelog")]
            public string Changelog { get; set; }
            [JsonProperty("net48-url")]
            public string Net48Url { get; set; }
            [JsonProperty("net60-url")]
            public string Net60Url { get; set; }
            [JsonProperty("net80-url")]
            public string Net80Url { get; set; }
        }
    }
}

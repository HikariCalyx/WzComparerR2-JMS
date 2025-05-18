﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using DevComponents.AdvTree;
using Newtonsoft.Json;
using WzComparerR2.Common;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO;
using System.Security.Policy;

namespace WzComparerR2
{
    public partial class FrmUpdater : DevComponents.DotNetBar.Office2007Form
    {
        public FrmUpdater()
        {
            InitializeComponent();
#if NET6_0_OR_GREATER
            // https://learn.microsoft.com/en-us/dotnet/core/compatibility/fx-core#controldefaultfont-changed-to-segoe-ui-9pt
            this.Font = new Font(new FontFamily("Microsoft Sans Serif"), 8f);
#endif

            // this.lblClrVer.Text = string.Format("{0} ({1})", Environment.Version, Program.GetArchitecture());
            this.lblCurrentVer.Text = BuildInfo.BuildTime;
            // this.lblLatestVer.Text = GetFileVersion().ToString();
            var updateSession = new UpdaterSession();
            // this.lblUpdateContent.Text = GetAsmCopyright().ToString();
            Task.Run(() => this.ExecuteUpdateAsync(updateSession, updateSession.CancellationToken));
            // GetPluginInfo();
        }

        private UpdaterSession updateSession;
        private string net462url;
        private string net60url;
        private string net80url;
        private string fileurl;

        private string updaterURL = "https://github.com/HikariCalyx/WzComparerR2Updater/releases/download/v1.0.0.250318-1934/Updater.exe";
        private static string checkUpdateURL = "https://api.github.com/repos/PirateIzzy/WzComparerR2/releases/latest";

        public static async Task<bool> QueryUpdate()
        {
            var request = (HttpWebRequest)WebRequest.Create(checkUpdateURL);
            request.Accept = "application/json";
            request.UserAgent = "WzComparerR2/1.0";
            try
            {
                using (WebResponse response = await request.GetResponseAsync())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    string responseString = reader.ReadToEnd();
                    JObject UpdateContent = JObject.Parse(responseString);
                    string BuildNumber = UpdateContent.SelectToken("tag_name").ToString();
                    return Int64.Parse(BuildNumber) > Int64.Parse(BuildInfo.BuildTime);
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private async Task ExecuteUpdateAsync(UpdaterSession session, CancellationToken cancellationToken)
        {
            var request = (HttpWebRequest)WebRequest.Create(checkUpdateURL);
            request.Accept = "application/json";
            request.UserAgent = "WzComparerR2/1.0";
            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                JObject UpdateContent = JObject.Parse(responseString);
                string BuildNumber = UpdateContent.SelectToken("tag_name").ToString();
                string ChangeTitle = UpdateContent.SelectToken("name").ToString();
                string Changelog = UpdateContent.SelectToken("body").ToString();
                JArray assets = (JArray)UpdateContent["assets"];
                string[] downloadUrls = new string[assets.Count];
                for (int i = 0; i < assets.Count; i++)
                {
                    downloadUrls[i] = assets[i]["browser_download_url"]?.ToString();
                }
                // This part is for builds that are separated into 3 packages
                foreach (string url in downloadUrls)
                {
                    if (url.Contains("net6.0")) net60url = url;
                    else if (url.Contains("net8.0")) net80url = url;
                    else net462url = url;
                }

                // fileurl = downloadUrls[0];

                this.lblLatestVer.Text = BuildNumber;
                AppendText(ChangeTitle + "\r\n", Color.Red);
                AppendText(Changelog, Color.Black);
                this.richTextBoxEx1.SelectionStart = 0;

                if (Int64.Parse(BuildNumber) > Int64.Parse(BuildInfo.BuildTime))
                {
                    buttonX1.Enabled = true;
                    this.lblUpdateContent.Text = "Update available";
                }
                else
                {
                    this.lblUpdateContent.Text = "Already using the latest version";
                }
            }
            catch (Exception ex)
            {
                this.lblUpdateContent.Text = "Update check failed";
            }
        }

        private async Task DownloadUpdateAsync(string url, UpdaterSession session, CancellationToken cancellationToken)
        {
            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string savePath = Path.Combine(currentDirectory, "update.zip");
            try
            {
                buttonX1.Enabled = false;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        using (Stream responseStream = response.GetResponseStream())
                        {
                            using (FileStream fileStream = new FileStream(savePath, FileMode.Create, FileAccess.Write))
                            {
                                responseStream.CopyTo(fileStream);
                            }
                        }
                    }
                }
                if (!File.Exists(Path.Combine(currentDirectory, "Updater.exe")))
                {
                    request = (HttpWebRequest)WebRequest.Create(updaterURL);
                    request.Method = "GET";
                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            using (Stream responseStream = response.GetResponseStream())
                            {
                                using (FileStream fileStream = new FileStream(Path.Combine(currentDirectory, "Updater.exe"), FileMode.Create, FileAccess.Write))
                                {
                                    responseStream.CopyTo(fileStream);
                                }
                            }
                        }
                    }
                }
                RunProgram("Updater.exe", "\"" + savePath + "\"");
            }
            catch (Exception ex)
            {
                this.lblUpdateContent.Text = "Failed to download update";
            }
            finally
            {
                buttonX1.Enabled = true;
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            this.lblUpdateContent.Text = "Downloading update...";
            buttonX1.Enabled = false;
            string selectedURL = "";
            updateSession = new UpdaterSession();
            switch (Environment.Version.Major)
            {
                default:
                case 4:
                    selectedURL = net462url;
                    break;
                case 6:
                    selectedURL = net60url;
                    break;
                case 8:
                    selectedURL = net80url;
                    break;
            }
            Task.Run(() => this.DownloadUpdateAsync(selectedURL, updateSession, updateSession.CancellationToken));
        }

        private void RunProgram(string url, string argument)
        {
#if NET6_0_OR_GREATER
            Process.Start(new ProcessStartInfo
            {
                UseShellExecute = true,
                FileName = url,
                Arguments = argument,
            });
#else
            Process.Start(url, argument);
#endif
        }

        private void AppendText(string text, Color color)
        {
            this.richTextBoxEx1.SelectionStart = this.richTextBoxEx1.TextLength;
            this.richTextBoxEx1.SelectionLength = 0;

            this.richTextBoxEx1.SelectionColor = color;
            this.richTextBoxEx1.AppendText(text);
            this.richTextBoxEx1.SelectionColor = this.richTextBoxEx1.ForeColor;
        }

        class UpdaterSession
        {
            public UpdaterSession()
            {
                this.cancellationTokenSource = new CancellationTokenSource();
            }
            public Task UpdateExecTask;

            public CancellationToken CancellationToken => this.cancellationTokenSource.Token;
            private CancellationTokenSource cancellationTokenSource;
            private TaskCompletionSource<bool> tcsWaiting;

            public void Cancel()
            {
                this.cancellationTokenSource.Cancel();
            }

            public async Task WaitForContinueAsync()
            {
                var tcs = new TaskCompletionSource<bool>();
                this.tcsWaiting = tcs;
                this.cancellationTokenSource.Token.Register(() => tcs.TrySetCanceled());
                await tcs.Task;
            }

            public void Continue()
            {
                if (this.tcsWaiting != null)
                {
                    this.tcsWaiting.SetResult(true);
                }
            }
        }
    }
}
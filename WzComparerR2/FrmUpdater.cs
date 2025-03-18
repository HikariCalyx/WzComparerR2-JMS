using System;
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

namespace WzComparerR2
{
    public partial class FrmUpdater : DevComponents.DotNetBar.Office2007Form
    {
        public FrmUpdater()
        {
            InitializeComponent();
#if NET6_0_OR_GREATER
            // https://learn.microsoft.com/en-us/dotnet/core/compatibility/fx-core#controldefaultfont-changed-to-segoe-ui-9pt
            this.Font = new Font(new FontFamily("MS Gothic"), 9f);
#endif

            // this.lblClrVer.Text = string.Format("{0} ({1})", Environment.Version, Program.GetArchitecture());
            this.lblCurrentVer.Text = Program.WcR2MajorVersion + BuildInfo.BuildTime;
            // this.lblLatestVer.Text = GetFileVersion().ToString();
            var updateSession = new UpdaterSession();
            // this.lblUpdateContent.Text = GetAsmCopyright().ToString();
            Task.Run(() => this.ExecuteUpdateAsync(updateSession, updateSession.CancellationToken));
            // GetPluginInfo();
        }

        private UpdaterSession updateSession;
        private string net48url;
        private string net60url;
        private string net80url;

        private void GetPluginInfo()
        {
            this.advTree1.Nodes.Clear();

            // this.advTree1.Nodes.Add(new Node("JMS <font color=\"#808080\">" + Program.WcR2MajorVersion + BuildInfo.BuildTime + "</font>"));
            // this.advTree1.Nodes.Add(new Node(LocalizedString_JP.FRMABOUT_VERSION));

            string nodeTxt = "<font color=\"#808080\">" + LocalizedString_JP.FRMABOUT_NO_AVAILABLE_PLUGINS + "</font>";
            Node node = new Node(nodeTxt);
            this.advTree1.Nodes.Add(node);
        }

        private async Task ExecuteUpdateAsync(UpdaterSession session, CancellationToken cancellationToken)
        {
            var request = (HttpWebRequest)WebRequest.Create(Program.CheckUpdateURL);
            request.Accept = "application/json";
            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                JObject UpdateContent = JObject.Parse(responseString);
                string MajorVersion = UpdateContent.SelectToken("MajorVersion").ToString();
                string BuildNumber = UpdateContent.SelectToken("BuildNumber").ToString();
                string ChangeTitle = UpdateContent.SelectToken("ChangeTitle").ToString();
                string Changelog = UpdateContent.SelectToken("Changelog").ToString();
                net48url = UpdateContent.SelectToken("net48-url").ToString();
                net60url = UpdateContent.SelectToken("net60-url").ToString();
                net80url = UpdateContent.SelectToken("net80-url").ToString();

                this.lblLatestVer.Text = MajorVersion + "." + BuildNumber;
                this.advTree1.Nodes.Add(new Node("<font color=\"#FF0000\">" + ChangeTitle + "</font>"));
                foreach (string line in Changelog.Split('\r'))
                {
                    this.advTree1.Nodes.Add(new Node(line));
                }

                if (Int64.Parse(BuildNumber.Replace("-", "")) > Int64.Parse(BuildInfo.BuildTime.Replace("-", "")))
                {
                    buttonX1.Enabled = true;
                    this.lblUpdateContent.Text = LocalizedString_JP.FRMUPDATER_UPDATE_AVAILABLE;
                }
                else
                {
                    this.lblUpdateContent.Text = LocalizedString_JP.FRMUPDATER_UPDATE_ALREADYLATEST;
                }
                
            }
            catch (Exception ex)
            {
                this.lblUpdateContent.Text = LocalizedString_JP.FRMUPDATER_UPDATE_CHECKFAIL;
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            switch (Environment.Version.Major)
            {
                case 4:
                    DownloadURL(net48url);
                    break;
                case 6:
                    DownloadURL(net60url);
                    break;
                case 8:
                    DownloadURL(net80url);
                    break;
            }
        }

        private void DownloadURL(string url)
        {
#if NET6_0_OR_GREATER
            Process.Start(new ProcessStartInfo
            {
                UseShellExecute = true,
                FileName = url,
            });
#else
            Process.Start(url);
#endif
        }

        class UpdaterSession
        {
            public UpdaterSession()
            {
                this.cancellationTokenSource = new CancellationTokenSource();
            }

            public string LatestMajorVersion;
            public string LatestBuildNumber;
            public string ChangeTitle;
            public string Changelog;
            public string Net48URL;
            public string Net60URL;
            public string Net80URL;

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
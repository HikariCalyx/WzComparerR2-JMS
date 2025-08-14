using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using DevComponents.AdvTree;
using WzComparerR2.Common;

namespace WzComparerR2
{
    public partial class FrmAbout : DevComponents.DotNetBar.Office2007Form
    {
        public FrmAbout()
        {
            InitializeComponent();
#if NET6_0_OR_GREATER
            // https://learn.microsoft.com/en-us/dotnet/core/compatibility/fx-core#controldefaultfont-changed-to-segoe-ui-9pt
            this.Font = new Font(new FontFamily("MS Gothic"), 9f);
#endif

            this.lblClrVer.Text = string.Format("{0} ({1})", Environment.Version, Program.GetArchitecture());
            this.lblAsmVer.Text = GetAsmVersion().ToString();
            this.lblFileVer.Text = GetFileVersion().ToString();
            this.lblCopyright.Text = GetAsmCopyright().ToString();
            GetPluginInfo();
        }

        private Version GetAsmVersion()
        {
            return this.GetType().Assembly.GetName().Version;
        }

        private string GetFileVersion()
        {
            return this.GetAsmAttr<AssemblyInformationalVersionAttribute>()?.InformationalVersion
                ?? this.GetAsmAttr<AssemblyFileVersionAttribute>()?.Version;
        }

        private string GetAsmCopyright()
        {
            return this.GetAsmAttr<AssemblyCopyrightAttribute>()?.Copyright;
        }

        private void GetPluginInfo()
        {
            this.advTree1.Nodes.Clear();

            this.advTree1.Nodes.Add(new Node("JMS <font color=\"#808080\">" + Program.WcR2MajorVersion + BuildInfo.BuildTime + "</font>"));
            this.advTree1.Nodes.Add(new Node(LocalizedString_JP.FRMABOUT_VERSION));

            foreach (var contribution in new[]
            {
                Tuple.Create("[KMS] 各種機能追加、最終翻訳", "パク・ヒョンミン"),
                Tuple.Create("[KMS] フレーズ翻訳", "シュリンニャン"),
                Tuple.Create("[KMS] フレーズエラー情報","インソーヤドットコムシルバー"),
                Tuple.Create("[KMS] フレーズエラー情報", "jusir_@naver.com"),
                Tuple.Create("[KMS] 機器ツールチップエラー情報", "@Sunaries"),
                Tuple.Create("[KMS] 重複着用不可文句エラー情報", "インソヤドットコム進流"),
                Tuple.Create("[KMS] アバター保存機能を追加", "@craftingmod"),
                Tuple.Create("[KMS] アバター読み込みエラー情報", "インソヤドットコム一感"),
                Tuple.Create("[KMS] ファイルを保存する際の名前規則エラー情報", "@mabooky"),
                Tuple.Create("[KMS] アバターハイレフ耳エラー情報", "メープルインベンヌリシンドローム"),
                Tuple.Create("[KMS] 各種エラー情報、GMS 情報提供", "@Sunaries"),
                Tuple.Create("[KMS] 利用可能なジョブフレーズエラー情報", "@tanyoucai"),
                Tuple.Create("[KMS] クエスト状態パーティクル未適用エラー情報", "メープルインベンダーパルダー"),
                Tuple.Create("[KMS] アバターエラー情報", "@giraffebin"),
                Tuple.Create("[KMS] フレーズ、ツールチップ位置エラーの修正と情報提供、ウィンドウサイズの保存機能、カインサポートの追加", "@OniOniOn-"),
                Tuple.Create("[KMS]パッチとの比較時のエラー情報", "@lowrt"),
                Tuple.Create("[KMS] アバターすべてエクスポートエラー情報", "@pid011"),
                Tuple.Create("[KMS] ツールチップ関連機能の追加、エラーの修正と情報提供", "@sh-cho"),
                Tuple.Create("[KMS] 各種機能追加", "シャンバー@seotbeo"),
                Tuple.Create("[KMS] アバターミックス色の組み合わせ方法の実装", "snlt7d"),
            })
            {
                string nodeTxt = string.Format("{0} <font color=\"#808080\">{1}</font>",
                        contribution.Item1,
                        contribution.Item2);
                Node node = new Node(nodeTxt);
                this.advTree1.Nodes.Add(node);
            }

            if (PluginBase.PluginManager.LoadedPlugins.Count > 0)
            {
                foreach (var plugin in PluginBase.PluginManager.LoadedPlugins)
                {
                    string nodeTxt = string.Format("{0} <font color=\"#808080\">{1} ({2})</font>",
                        plugin.Instance.Name,
                        plugin.Instance.Version,
                        plugin.Instance.FileVersion);
                    Node node = new Node(nodeTxt);
                    this.advTree1.Nodes.Add(node);
                }
            }
            else
            {
                string nodeTxt = "<font color=\"#808080\">" + LocalizedString_JP.FRMABOUT_NO_AVAILABLE_PLUGINS + "</font>";
                Node node = new Node(nodeTxt);
                this.advTree1.Nodes.Add(node);
            }

            {
                var NANUMGOTHIC_SOURCEINFO = "\r\n이 프로그램 일부에는 네이버에서 제공한 나눔글꼴이 적용되어 있습니다.\r\n";
                Node node = new Node(NANUMGOTHIC_SOURCEINFO);
                this.advTree1.Nodes.Add(node);
            }
        }

        private T GetAsmAttr<T>()
        {
            object[] attr = this.GetType().Assembly.GetCustomAttributes(typeof(T), true);
            if (attr != null && attr.Length > 0)
            {
                return (T)attr[0];
            }
            return default(T);
        }
    }
}
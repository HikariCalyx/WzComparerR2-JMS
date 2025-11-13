using DevComponents.DotNetBar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WzComparerR2.WzLib;

namespace WzComparerR2
{
    public partial class FrmSkillTooltipExport : DevComponents.DotNetBar.Office2007Form
    {
        public FrmSkillTooltipExport()
        {
            InitializeComponent();
#if NET6_0_OR_GREATER
            // https://learn.microsoft.com/en-us/dotnet/core/compatibility/fx-core#controldefaultfont-changed-to-segoe-ui-9pt
            this.Font = new Font(new FontFamily("MS PGothic"), 9f);
#endif
            this.clbJobName.Items.Add("その他", false);
            foreach (var i in jobNameToCode.Keys)
            {
                this.clbJobName.Items.Add(i, false);
            }
        }

        public string ExportFolderPath { get; private set; }
        public List<int> SelectedJobCodes { get; private set; }
        public Wz_Node skillNode { get; set; }
        private bool sorted = false;

        private static Dictionary<string, int[]> jobNameToCode = new Dictionary<string, int[]>()
        {
            { "ヒーロー", new int[] { 100, 110, 111, 112, 114 } },
            { "パラディン", new int[] { 100, 120, 121, 122, 124 } },
            { "ダークナイト", new int[] { 100, 130, 131, 132, 134 } },
            { "アークメイジ(火・毒)", new int[] { 200, 210, 211, 212, 214 } },
            { "アークメイジ(氷・雷)", new int[] { 200, 220, 221, 222, 224 } },
            { "ビショップ", new int[] { 200, 230, 231, 232, 234 } },
            { "ボウマスター", new int[] { 300, 310, 311, 312, 314 } },
            { "クロスボウマスター", new int[] { 300, 320, 321, 322, 324 } },
            { "パスファインダー", new int[] { 301, 330, 331, 332, 334 } },
            { "ナイトロード", new int[] { 400, 410, 411, 412, 414 } },
            { "シャドー", new int[] { 400, 420, 421, 422, 424 } },
            { "デュアルブレイド", new int[] { 400, 430, 431, 432, 433, 434, 436 } },
            { "バイパー", new int[] { 500, 510, 511, 512, 514 } },
            { "キャプテン", new int[] { 500, 520, 521, 522, 524 } },
            { "キャノンマスター", new int[] { 501, 530, 531, 532, 534 } },
            { "ジェット", new int[] { 508, 570, 571, 572, 574 } },
            { "ソウルマスター", new int[] { 1000, 1100, 1110, 1111, 1112, 1114 } },
            { "フレイムウィザード", new int[] { 1000, 1200, 1210, 1211, 1212, 1214 } },
            { "ウインドシューター", new int[] { 1000, 1300, 1310, 1311, 1312, 1314 } },
            { "ナイトウォーカー", new int[] { 1000, 1400, 1410, 1411, 1412, 1414 } },
            { "ストライカー", new int[] { 1000, 1500, 1510, 1511, 1512, 1514 } },
            { "アラン", new int[] { 2000, 2100, 2110, 2111, 2112, 2114 } },
            { "エヴァン", new int[] { 2001, 2200, 2210, 2211, 2212, 2213, 2214, 2215, 2216, 2217, 2218, 2219, 2220 } },
            { "メルセデス", new int[] { 2002, 2300, 2310, 2311, 2312, 2314 } },
            { "ファントム", new int[] { 2003, 2400, 2410, 2411, 2412, 2414 } },
            { "ルミナス", new int[] { 2004, 2700, 2710, 2711, 2712, 2714 } },
            { "隠月", new int[] { 2005, 2500, 2510, 2511, 2512, 2514 } },
            { "デーモンスレイヤー", new int[] { 3001, 3100, 3110, 3111, 3112, 3114 } },
            { "デーモンアヴェンジャー", new int[] { 3001, 3101, 3120, 3121, 3122, 3124 } },
            { "ブラスター", new int[] { 3000, 3700, 3710, 3711, 3712, 3714 } },
            { "バトルメイジ", new int[] { 3000, 3200, 3210, 3211, 3212, 3214 } },
            { "ワイルドハンター", new int[] { 3000, 3300, 3310, 3311, 3312, 3314 } },
            { "メカニック", new int[] { 3000, 3500, 3510, 3511, 3512, 3514 } },
            { "ゼノン", new int[] { 3002, 3600, 3610, 3611, 3612, 3614 } },
            { "ハヤト", new int[] { 4001, 4100, 4110, 4111, 4112, 4114 } },
            { "カンナ", new int[] { 4002, 4200, 4210, 4211, 4212, 4214 } },
            { "ミハエル", new int[] { 5000, 5100, 5110, 5111, 5112, 5114 } },
            { "カイザー", new int[] { 6000, 6100, 6110, 6111, 6112, 6114 } },
            { "カイン", new int[] { 6003, 6300, 6310, 6311, 6312, 6314 } },
            { "カデナ", new int[] { 6002, 6400, 6410, 6411, 6412, 6414 } },
            { "エンジェリックバスター", new int[] { 6001, 6500, 6510, 6511, 6512, 6514 } },
            // { "アビリティ", new int[] { 7000 } },
            // { "ユニオン", new int[] { 7100 } },
            // { "モンスターライフ", new int[] { 7200 } },
            // { "ギルド", new int[] { 9100 } },
            // { "専業技術", new int[] { 9200, 9201, 9202, 9203, 9204 } },
            { "ゼロ", new int[] { 10000, 10100, 10110, 10111, 10112, 10114 } },
            { "ビーストテイマー", new int[] { 11000, 11200, 11210, 11211, 11212 } },
            { "竈門炭治郎", new int[] { 12000, 12005, 12100 } },
            { "サイタマ", new int[] { 12006, 12200 } },
            { "ピンクビーン", new int[] { 13000, 13100 } },
            { "イェティ", new int[] { 13001, 13500 } },
            { "キネシス", new int[] { 14000, 14200, 14210, 14211, 14212, 14214 } },
            { "アデル", new int[] { 15002, 15100, 15110, 15111, 15112, 15114 } },
            { "イリウム", new int[] { 15000, 15200, 15210, 15211, 15212, 15214 } },
            { "カーリー", new int[] { 15003, 15400, 15410, 15411, 15412, 15414 } },
            { "アーク", new int[] { 15001, 15500, 15510, 15511, 15512, 15514 } },
            { "レン", new int[] { 16002, 16100, 16110, 16111, 16112, 16114 } },
            { "ララ", new int[] { 16001, 16200, 16210, 16211, 16212, 16214 } },
            { "虎影", new int[] { 16000, 16400, 16410, 16411, 16412, 16414 } },
            { "墨玄", new int[] { 17000, 17500, 17510, 17511, 17512, 17514 } },
            { "リン", new int[] { 17001, 17200, 17210, 17211, 17212, 17214 } },
            { "エリル・ライト", new int[] { 18001, 18100, 18110, 18111, 18112, 18114 } },
            { "シア・アステル", new int[] { 18000, 18200, 18210, 18211, 18212, 18214 } },
            { "アイエル", new int[] { 18002, 18300, 18310, 18311, 18312, 18314 } },
            { "5次スキル(その他)", new int[] { 40000, 40001, 40002, 40003, 40004, 40005 } },
            { "6次スキル(その他)", new int[] { 50000, 50006, 50007 } },
        };

        private static Dictionary<string, int[]> jobNameToCodeSorted = new Dictionary<string, int[]>()
        {
            { "アーク", new int[] { 15001, 15500, 15510, 15511, 15512, 15514 } },
            { "アークメイジ(氷・雷)", new int[] { 200, 220, 221, 222, 224 } },
            { "アークメイジ(火・毒)", new int[] { 200, 210, 211, 212, 214 } },
            { "アイエル", new int[] { 18002, 18300, 18310, 18311, 18312, 18314 } },
            { "アデル", new int[] { 15002, 15100, 15110, 15111, 15112, 15114 } },
            // { "アビリティ", new int[] { 7000 } },
            { "アラン", new int[] { 2000, 2100, 2110, 2111, 2112, 2114 } },
            { "イェティ", new int[] { 13001, 13500 } },
            { "イリウム", new int[] { 15000, 15200, 15210, 15211, 15212, 15214 } },
            { "ウインドシューター", new int[] { 1000, 1300, 1310, 1311, 1312, 1314 } },
            { "エヴァン", new int[] { 2001, 2200, 2210, 2211, 2212, 2213, 2214, 2215, 2216, 2217, 2218, 2219, 2220 } },
            { "エリル・ライト", new int[] { 18001, 18100, 18110, 18111, 18112, 18114 } },
            { "エンジェリックバスター", new int[] { 6001, 6500, 6510, 6511, 6512, 6514 } },
            { "カーリー", new int[] { 15003, 15400, 15410, 15411, 15412, 15414 } },
            { "カイザー", new int[] { 6000, 6100, 6110, 6111, 6112, 6114 } },
            { "カイン", new int[] { 6003, 6300, 6310, 6311, 6312, 6314 } },
            { "カデナ", new int[] { 6002, 6400, 6410, 6411, 6412, 6414 } },
            { "竈門炭治郎", new int[] { 12000, 12005, 12100 } },
            { "カンナ", new int[] { 4002, 4200, 4210, 4211, 4212, 4214 } },
            { "キネシス", new int[] { 14000, 14200, 14210, 14211, 14212, 14214 } },
            { "キャノンマスター", new int[] { 501, 530, 531, 532, 534 } },
            { "キャプテン", new int[] { 500, 520, 521, 522, 524 } },
            // { "ギルド", new int[] { 9100 } },
            { "クロスボウマスター", new int[] { 300, 320, 321, 322, 324 } },
            { "虎影", new int[] { 16000, 16400, 16410, 16411, 16412, 16414 } },
            { "サイタマ", new int[] { 12006, 12200 } },
            { "シア・アステル", new int[] { 18000, 18200, 18210, 18211, 18212, 18214 } },
            { "ジェット", new int[] { 508, 570, 571, 572, 574 } },
            { "シャドー", new int[] { 400, 420, 421, 422, 424 } },
            { "ストライカー", new int[] { 1000, 1500, 1510, 1511, 1512, 1514 } },
            { "ゼノン", new int[] { 3002, 3600, 3610, 3611, 3612, 3614 } },
            { "ゼロ", new int[] { 10000, 10100, 10110, 10111, 10112, 10114 } },
            // { "専業技術", new int[] { 9200, 9201, 9202, 9203, 9204 } },
            { "ソウルマスター", new int[] { 1000, 1100, 1110, 1111, 1112, 1114 } },
            { "ダークナイト", new int[] { 100, 130, 131, 132, 134 } },
            { "デーモンアヴェンジャー", new int[] { 3001, 3101, 3120, 3121, 3122, 3124 } },
            { "デーモンスレイヤー", new int[] { 3001, 3100, 3110, 3111, 3112, 3114 } },
            { "デュアルブレイド", new int[] { 400, 430, 431, 432, 433, 434, 436 } },
            { "ナイトウォーカー", new int[] { 1000, 1400, 1410, 1411, 1412, 1414 } },
            { "ナイトロード", new int[] { 400, 410, 411, 412, 414 } },
            { "バイパー", new int[] { 500, 510, 511, 512, 514 } },
            { "パスファインダー", new int[] { 301, 330, 331, 332, 334 } },
            { "バトルメイジ", new int[] { 3000, 3200, 3210, 3211, 3212, 3214 } },
            { "ハヤト", new int[] { 4001, 4100, 4110, 4111, 4112, 4114 } },
            { "パラディン", new int[] { 100, 120, 121, 122, 124 } },
            { "ビーストテイマー", new int[] { 11000, 11200, 11210, 11211, 11212 } },
            { "ヒーロー", new int[] { 100, 110, 111, 112, 114 } },
            { "ビショップ", new int[] { 200, 230, 231, 232, 234 } },
            { "ピンクビーン", new int[] { 13000, 13100 } },
            { "ファントム", new int[] { 2003, 2400, 2410, 2411, 2412, 2414 } },
            { "ブラスター", new int[] { 3000, 3700, 3710, 3711, 3712, 3714 } },
            { "フレイムウィザード", new int[] { 1000, 1200, 1210, 1211, 1212, 1214 } },
            { "ボウマスター", new int[] { 300, 310, 311, 312, 314 } },
            { "墨玄", new int[] { 17000, 17500, 17510, 17511, 17512, 17514 } },
            { "ミハエル", new int[] { 5000, 5100, 5110, 5111, 5112, 5114 } },
            { "メカニック", new int[] { 3000, 3500, 3510, 3511, 3512, 3514 } },
            { "メルセデス", new int[] { 2002, 2300, 2310, 2311, 2312, 2314 } },
            // { "モンスターライフ", new int[] { 7200 } },
            { "隠月", new int[] { 2005, 2500, 2510, 2511, 2512, 2514 } },
            // { "ユニオン", new int[] { 7100 } },
            { "ララ", new int[] { 16001, 16200, 16210, 16211, 16212, 16214 } },
            { "リン", new int[] { 17001, 17200, 17210, 17211, 17212, 17214 } },
            { "ルミナス", new int[] { 2004, 2700, 2710, 2711, 2712, 2714 } },
            { "レン", new int[] { 16002, 16100, 16110, 16111, 16112, 16114 } },
            { "ワイルドハンター", new int[] { 3000, 3300, 3310, 3311, 3312, 3314 } },
            { "5次スキル(その他)", new int[] { 40000, 40001, 40002, 40003, 40004, 40005 } },
            { "6次スキル(その他)", new int[] { 50000, 50006, 50007 } },
        };

        private static HashSet<int> AllClassesCode()
        {
            HashSet<int> hsClassCode = new HashSet<int>() { };
            foreach (var i in jobNameToCode.Values)
            {
                foreach (var j in i)
                {
                    hsClassCode.Add(j);
                }
            }
            return hsClassCode;
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            this.sorted = !this.sorted;
            this.btnSort.Text = this.sorted ? "アイウエオ順" : "初期順";
            this.clbJobName.Items.Clear();
            this.clbJobName.Items.Add("その他", this.clbJobName.CheckedItems.Contains("その他"));
            var sourceDict = this.sorted ? jobNameToCodeSorted : jobNameToCode;
            foreach (var i in sourceDict.Keys)
            {
                this.clbJobName.Items.Add(i, this.clbJobName.CheckedItems.Contains(i));
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            int checkedCount = this.clbJobName.CheckedItems.Contains("その他") ? this.clbJobName.CheckedItems.Count - 1 : this.clbJobName.CheckedItems.Count;
            bool checkedStatus = (checkedCount < this.clbJobName.Items.Count - 1);
            for (int i = 1; i < this.clbJobName.Items.Count; i++)
            {
                this.clbJobName.SetItemChecked(i, checkedStatus);
            }
        }

        private void btnReverseSelect_Click(object sender, EventArgs e)
        {
            for (int i = 1; i < this.clbJobName.Items.Count; i++)
            {
                this.clbJobName.SetItemChecked(i, !this.clbJobName.GetItemChecked(i));
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (this.clbJobName.CheckedItems.Count == 0)
            {
                MessageBoxEx.Show("エクスポートする職業を少なくとも1つ選択してください。", "職業未選択", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.Description = "保存先のフォルダーを選択します。";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                bool allSelected = this.clbJobName.CheckedItems.Count == this.clbJobName.Items.Count;

                List<int> skillImg = new List<int>() { };
                foreach (Wz_Node node in skillNode.Nodes)
                {
                    Wz_Image currentImg = node.GetValue<Wz_Image>();
                    if (currentImg != null && Int32.TryParse(currentImg.Name.Replace(".img", ""), out int jobCode))
                    {
                        skillImg.Add(jobCode);
                    }
                }
                List<int> selectedJob = skillImg.Intersect(this.clbJobName.CheckedItems.Cast<string>().SelectMany(name => jobNameToCode.ContainsKey(name) ? jobNameToCode[name] : new int[] { }).ToList()).ToList();
                if (this.clbJobName.CheckedItems.Contains("その他"))
                {
                    selectedJob.AddRange(skillImg.Except(AllClassesCode()));
                }
                ExportFolderPath = dlg.SelectedPath;
                SelectedJobCodes = allSelected ? skillImg : selectedJob;
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}

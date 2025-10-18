using System;
using System.Collections.Generic;
using System.Text;
using WzComparerR2.Config;
using WzComparerR2.WzLib;
using WzComparerR2.PluginBase;
using DevComponents.DotNetBar;

namespace WzComparerR2.Db2
{
    public class Entry : PluginEntry
    {
        public Entry(PluginContext context)
            : base(context)
        {
            Instance = this;
        }

        internal static Entry Instance { get; private set; }

        protected override void OnLoad()
        {
            var bar = this.Context.AddRibbonBar("Tools", "DB2");
            ItemContainer db2Container1 = new ItemContainer();
            ItemContainer db2Container1_0 = new ItemContainer();
            ItemContainer db2Container1_1 = new ItemContainer();
            ItemContainer db2Container1_2 = new ItemContainer();
            ButtonItem btnItemSearchByIcon = new ButtonItem("btnItemSearchByIcon", "アイコンでアイテムを検索");
            ButtonItem btnMapObjLookup = new ButtonItem("btnMapObjLookup", "マップオブジェクトを検索");
            ButtonItem btnAssetImageViewer = new ButtonItem("btnAssetImageViewer", "アセットイメージビューア");
            db2Container1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            db2Container1.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
            db2Container1.Name = "db2Container1";
            db2Container1.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            db2Container1_0,
            db2Container1_1,
            db2Container1_2});

            db2Container1_0.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            db2Container1_0.Name = "db2Container1_0";
            db2Container1_0.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            btnItemSearchByIcon});

            db2Container1_1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            db2Container1_1.Name = "db2Container1_1";
            db2Container1_1.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            btnMapObjLookup});

            db2Container1_2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            db2Container1_2.Name = "db2Container1_1";
            db2Container1_2.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            btnAssetImageViewer});

            btnItemSearchByIcon.Click += btnItemSearchByIcon_Click;
            bar.Items.Add(db2Container1);

            ConfigManager.RegisterAllSection(this.GetType().Assembly);
        }

        DB2Form frm;

        void btnItemSearchByIcon_Click(object sender, EventArgs e)
        {
            Wz_Node baseNode = Context.SelectedNode1;
            if (baseNode == null || baseNode.GetNodeWzFile().Type != Wz_Type.Base)
            {
                MessageBoxEx.Show("ロードしたBase.wzを選択してください。", "注意");
                return;
            }
            if (frm == null || frm.IsDisposed)
            {
                frm = new DB2Form();
                frm.Owner = Context.MainForm;
                frm.baseWzNode = baseNode;
                
            }
            frm.Show();
            frm.Focus();
        }

        protected override void OnUnload()
        {
            base.OnUnload();
        }
    }
}

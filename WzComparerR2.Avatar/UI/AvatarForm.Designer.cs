﻿using System.Windows.Forms;

namespace WzComparerR2.Avatar.UI
{
    partial class AvatarForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dotNetBarManager1 = new DevComponents.DotNetBar.DotNetBarManager(this.components);
            this.dockSite4 = new DevComponents.DotNetBar.DockSite();
            this.dockSite1 = new DevComponents.DotNetBar.DockSite();
            this.dockSite2 = new DevComponents.DotNetBar.DockSite();
            this.bar1 = new DevComponents.DotNetBar.Bar();
            this.panelDockContainer1 = new DevComponents.DotNetBar.PanelDockContainer();
            this.itemPanel1 = new DevComponents.DotNetBar.ItemPanel();
            this.dockContainerItem1 = new DevComponents.DotNetBar.DockContainerItem();
            this.bar2 = new DevComponents.DotNetBar.Bar();
            this.panelDockContainer2 = new DevComponents.DotNetBar.PanelDockContainer();
            this.cmbEar = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cmbWeaponIdx = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cmbWeaponType = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.chkTamingPlay = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.chkEmotionPlay = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.chkBodyPlay = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.cmbTamingFrame = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cmbEmotionFrame = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            // virtual comboboxes for item effects, not shown
            this.cmbEffectFrames = new DevComponents.DotNetBar.Controls.ComboBoxEx[18];
            this.cmbActionEffects = new DevComponents.DotNetBar.Controls.ComboBoxEx[18];
            for (int i = 0; i < 18; i++)
            {
                var t1 = new DevComponents.DotNetBar.Controls.ComboBoxEx();
                t1.SelectedIndexChanged += new System.EventHandler(this.cmbEffectFrames_SelectedIndexChanged);
                cmbEffectFrames[i] = t1;
                var t2 = new DevComponents.DotNetBar.Controls.ComboBoxEx();
                t2.SelectedIndexChanged += new System.EventHandler(this.cmbActionEffect_SelectedIndexChanged);
                cmbActionEffects[i] = t2;
            }
            this.cmbBodyFrame = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cmbActionTaming = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cmbEmotion = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.cmbActionBody = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.chkHairShade = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.chkHairCover = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.chkApplyBRM = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.cmbGroupChair = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.dockContainerItem2 = new DevComponents.DotNetBar.DockContainerItem();
            this.dockSite8 = new DevComponents.DotNetBar.DockSite();
            this.dockSite5 = new DevComponents.DotNetBar.DockSite();
            this.dockSite6 = new DevComponents.DotNetBar.DockSite();
            this.dockSite7 = new DevComponents.DotNetBar.DockSite();
            this.bar3 = new DevComponents.DotNetBar.Bar();
            this.btnCode = new DevComponents.DotNetBar.ButtonItem();
            this.btnCharac = new DevComponents.DotNetBar.ButtonItem();
            this.btnMale = new DevComponents.DotNetBar.ButtonItem();
            this.btnFemale = new DevComponents.DotNetBar.ButtonItem();
            this.btnZero = new DevComponents.DotNetBar.ButtonItem();
            this.btnBeastTamer = new DevComponents.DotNetBar.ButtonItem();
            this.btnPathfinder = new DevComponents.DotNetBar.ButtonItem();
            this.btnLara = new DevComponents.DotNetBar.ButtonItem();
            this.btnLynn = new DevComponents.DotNetBar.ButtonItem();
            this.btnHayato = new DevComponents.DotNetBar.ButtonItem();
            this.btnKanna = new DevComponents.DotNetBar.ButtonItem();
            this.btnAngelicBuster = new DevComponents.DotNetBar.ButtonItem();
            this.btnOldBokugen = new DevComponents.DotNetBar.ButtonItem();
            this.btnNewBokugen = new DevComponents.DotNetBar.ButtonItem();
            this.btnPopuko = new DevComponents.DotNetBar.ButtonItem();
            this.btnPipimi = new DevComponents.DotNetBar.ButtonItem();
            this.btnMegumin = new DevComponents.DotNetBar.ButtonItem();
            this.btnAqua = new DevComponents.DotNetBar.ButtonItem();
            this.btnDarkness = new DevComponents.DotNetBar.ButtonItem();
            this.btnTanjiroKamado = new DevComponents.DotNetBar.ButtonItem();
            this.btnNezukoKamado = new DevComponents.DotNetBar.ButtonItem();
            this.btnZenitsuAgatsuma = new DevComponents.DotNetBar.ButtonItem();
            this.btnInosukeHashibira = new DevComponents.DotNetBar.ButtonItem();
            this.btnLaraTheSheep = new DevComponents.DotNetBar.ButtonItem();
            this.btnCustomPreset = new DevComponents.DotNetBar.ButtonItem();
            this.Separator1 = new DevComponents.DotNetBar.Separator();
            this.Separator2 = new DevComponents.DotNetBar.Separator();
            this.Separator3 = new DevComponents.DotNetBar.Separator();
            this.Separator4 = new DevComponents.DotNetBar.Separator();
            this.btnAPI = new DevComponents.DotNetBar.ButtonItem();
            this.btnReset = new DevComponents.DotNetBar.ButtonItem();
            this.btnLock = new DevComponents.DotNetBar.ButtonItem();
            this.btnSaveAsGif = new DevComponents.DotNetBar.ButtonItem();
            this.btnSaveOptions = new DevComponents.DotNetBar.ButtonItem();
            this.btnEnableAutosave = new DevComponents.DotNetBar.ButtonItem();
            this.btnSpecifySavePath = new DevComponents.DotNetBar.ButtonItem();
            this.btnExport = new DevComponents.DotNetBar.ButtonItem();
            this.dockSite3 = new DevComponents.DotNetBar.DockSite();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.avatarContainer1 = new WzComparerR2.Avatar.UI.AvatarContainer();
            this.dockSite2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).BeginInit();
            this.bar1.SuspendLayout();
            this.panelDockContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bar2)).BeginInit();
            this.bar2.SuspendLayout();
            this.panelDockContainer2.SuspendLayout();
            this.dockSite7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bar3)).BeginInit();
            this.SuspendLayout();
            // 
            // dotNetBarManager1
            // 
            this.dotNetBarManager1.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.F1);
            this.dotNetBarManager1.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlC);
            this.dotNetBarManager1.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlA);
            this.dotNetBarManager1.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlV);
            this.dotNetBarManager1.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlX);
            this.dotNetBarManager1.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlZ);
            this.dotNetBarManager1.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlY);
            this.dotNetBarManager1.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.Del);
            this.dotNetBarManager1.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.Ins);
            this.dotNetBarManager1.BottomDockSite = this.dockSite4;
            this.dotNetBarManager1.EnableFullSizeDock = false;
            this.dotNetBarManager1.LeftDockSite = this.dockSite1;
            this.dotNetBarManager1.ParentForm = this;
            this.dotNetBarManager1.RightDockSite = this.dockSite2;
            this.dotNetBarManager1.ShowCustomizeContextMenu = false;
            this.dotNetBarManager1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dotNetBarManager1.ToolbarBottomDockSite = this.dockSite8;
            this.dotNetBarManager1.ToolbarLeftDockSite = this.dockSite5;
            this.dotNetBarManager1.ToolbarRightDockSite = this.dockSite6;
            this.dotNetBarManager1.ToolbarTopDockSite = this.dockSite7;
            this.dotNetBarManager1.TopDockSite = this.dockSite3;
            // 
            // dockSite4
            // 
            this.dockSite4.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.dockSite4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dockSite4.DocumentDockContainer = new DevComponents.DotNetBar.DocumentDockContainer();
            this.dockSite4.Location = new System.Drawing.Point(0, 539);
            this.dockSite4.Name = "dockSite4";
            this.dockSite4.Size = new System.Drawing.Size(775, 0);
            this.dockSite4.TabIndex = 3;
            this.dockSite4.TabStop = false;
            // 
            // dockSite1
            // 
            this.dockSite1.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.dockSite1.Dock = System.Windows.Forms.DockStyle.Left;
            this.dockSite1.DocumentDockContainer = new DevComponents.DotNetBar.DocumentDockContainer();
            this.dockSite1.Location = new System.Drawing.Point(0, 27);
            this.dockSite1.Name = "dockSite1";
            this.dockSite1.Size = new System.Drawing.Size(0, 512);
            this.dockSite1.TabIndex = 0;
            this.dockSite1.TabStop = false;
            // 
            // dockSite2
            // 
            this.dockSite2.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.dockSite2.Controls.Add(this.bar1);
            this.dockSite2.Controls.Add(this.bar2);
            this.dockSite2.Dock = System.Windows.Forms.DockStyle.Right;
            this.dockSite2.DocumentDockContainer = new DevComponents.DotNetBar.DocumentDockContainer(new DevComponents.DotNetBar.DocumentBaseContainer[] {
            ((DevComponents.DotNetBar.DocumentBaseContainer)(new DevComponents.DotNetBar.DocumentBarContainer(this.bar1, 213, 278))),
            ((DevComponents.DotNetBar.DocumentBaseContainer)(new DevComponents.DotNetBar.DocumentBarContainer(this.bar2, 213, 231)))}, DevComponents.DotNetBar.eOrientation.Vertical);
            this.dockSite2.Location = new System.Drawing.Point(559, 27);
            this.dockSite2.Name = "dockSite2";
            this.dockSite2.Size = new System.Drawing.Size(250, 512);
            this.dockSite2.TabIndex = 1;
            this.dockSite2.TabStop = false;
            // 
            // bar1
            // 
            this.bar1.AccessibleDescription = "DotNetBar Bar (bar1)";
            this.bar1.AccessibleName = "DotNetBar Bar";
            this.bar1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.bar1.AutoSyncBarCaption = true;
            this.bar1.CloseSingleTab = true;
            this.bar1.Controls.Add(this.panelDockContainer1);
            this.bar1.Font = new System.Drawing.Font("MS Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.bar1.GrabHandleStyle = DevComponents.DotNetBar.eGrabHandleStyle.Caption;
            this.bar1.IsMaximized = false;
            this.bar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.dockContainerItem1});
            this.bar1.LayoutType = DevComponents.DotNetBar.eLayoutType.DockContainer;
            this.bar1.Location = new System.Drawing.Point(3, 0);
            this.bar1.Name = "bar1";
            this.bar1.Size = new System.Drawing.Size(213, 278);
            this.bar1.Stretch = true;
            this.bar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bar1.TabIndex = 0;
            this.bar1.TabStop = false;
            this.bar1.Text = "パネル";
            // 
            // panelDockContainer1
            // 
            this.panelDockContainer1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelDockContainer1.Controls.Add(this.itemPanel1);
            this.panelDockContainer1.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelDockContainer1.Location = new System.Drawing.Point(3, 23);
            this.panelDockContainer1.Name = "panelDockContainer1";
            this.panelDockContainer1.Size = new System.Drawing.Size(207, 252);
            this.panelDockContainer1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelDockContainer1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.panelDockContainer1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.panelDockContainer1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.panelDockContainer1.Style.GradientAngle = 90;
            this.panelDockContainer1.TabIndex = 0;
            this.panelDockContainer1.Visible = true;
            // 
            // itemPanel1
            // 
            this.itemPanel1.AutoScroll = true;
            // 
            // 
            // 
            this.itemPanel1.BackgroundStyle.Class = "ItemPanel";
            this.itemPanel1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.itemPanel1.ContainerControlProcessDialogKey = true;
            this.itemPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.itemPanel1.DragDropSupport = true;
            this.itemPanel1.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
            this.itemPanel1.Location = new System.Drawing.Point(0, 0);
            this.itemPanel1.Name = "itemPanel1";
            this.itemPanel1.Size = new System.Drawing.Size(207, 252);
            this.itemPanel1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.itemPanel1.TabIndex = 8;
            this.itemPanel1.Text = "itemPanel1";
            // 
            // dockContainerItem1
            // 
            this.dockContainerItem1.Control = this.panelDockContainer1;
            this.dockContainerItem1.Name = "dockContainerItem1";
            this.dockContainerItem1.Text = "パネル";
            // 
            // bar2
            // 
            this.bar2.AccessibleDescription = "DotNetBar Bar (bar2)";
            this.bar2.AccessibleName = "DotNetBar Bar";
            this.bar2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.bar2.AutoSyncBarCaption = true;
            this.bar2.CloseSingleTab = true;
            this.bar2.Controls.Add(this.panelDockContainer2);
            this.bar2.Font = new System.Drawing.Font("MS Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.bar2.GrabHandleStyle = DevComponents.DotNetBar.eGrabHandleStyle.Caption;
            this.bar2.IsMaximized = false;
            this.bar2.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.dockContainerItem2});
            this.bar2.LayoutType = DevComponents.DotNetBar.eLayoutType.DockContainer;
            this.bar2.Location = new System.Drawing.Point(3, 281);
            this.bar2.Name = "bar2";
            this.bar2.Size = new System.Drawing.Size(213, 231);
            this.bar2.Stretch = true;
            this.bar2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bar2.TabIndex = 1;
            this.bar2.TabStop = false;
            this.bar2.Text = "行動";
            // 
            // panelDockContainer2
            // 
            this.panelDockContainer2.AutoScroll = true;
            this.panelDockContainer2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelDockContainer2.Controls.Add(this.cmbEar);
            this.panelDockContainer2.Controls.Add(this.cmbWeaponIdx);
            this.panelDockContainer2.Controls.Add(this.cmbWeaponType);
            this.panelDockContainer2.Controls.Add(this.labelX4);
            this.panelDockContainer2.Controls.Add(this.chkTamingPlay);
            this.panelDockContainer2.Controls.Add(this.chkEmotionPlay);
            this.panelDockContainer2.Controls.Add(this.chkBodyPlay);
            this.panelDockContainer2.Controls.Add(this.cmbTamingFrame);
            this.panelDockContainer2.Controls.Add(this.cmbEmotionFrame);
            this.panelDockContainer2.Controls.Add(this.cmbBodyFrame);
            this.panelDockContainer2.Controls.Add(this.cmbActionTaming);
            this.panelDockContainer2.Controls.Add(this.cmbEmotion);
            this.panelDockContainer2.Controls.Add(this.labelX3);
            this.panelDockContainer2.Controls.Add(this.labelX2);
            this.panelDockContainer2.Controls.Add(this.labelX1);
            this.panelDockContainer2.Controls.Add(this.cmbActionBody);
            this.panelDockContainer2.Controls.Add(this.chkHairShade);
            this.panelDockContainer2.Controls.Add(this.chkHairCover);
            this.panelDockContainer2.Controls.Add(this.chkApplyBRM);
            this.panelDockContainer2.Controls.Add(this.labelX5);
            this.panelDockContainer2.Controls.Add(this.labelX6);
            this.panelDockContainer2.Controls.Add(this.cmbGroupChair);
            this.panelDockContainer2.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelDockContainer2.Location = new System.Drawing.Point(3, 23);
            this.panelDockContainer2.Name = "panelDockContainer2";
            this.panelDockContainer2.Size = new System.Drawing.Size(207, 205);
            this.panelDockContainer2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelDockContainer2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.panelDockContainer2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.panelDockContainer2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.panelDockContainer2.Style.GradientAngle = 90;
            this.panelDockContainer2.TabIndex = 0;
            this.panelDockContainer2.Visible = true;
            // 
            // cmbEar
            // 
            this.cmbEar.DisplayMember = "Text";
            this.cmbEar.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbEar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbEar.FormattingEnabled = true;
            this.cmbEar.ItemHeight = 15;
            this.cmbEar.Location = new System.Drawing.Point(163, 103);
            this.cmbEar.Name = "cmbEar";
            this.cmbEar.Size = new System.Drawing.Size(39, 21);
            this.cmbEar.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbEar.TabIndex = 15;
            this.cmbEar.SelectedIndexChanged += new System.EventHandler(this.cmbEar_SelectedIndexChanged);
            // 
            // cmbWeaponIdx
            // 
            this.cmbWeaponIdx.DisplayMember = "Text";
            this.cmbWeaponIdx.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbWeaponIdx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWeaponIdx.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbWeaponIdx.FormattingEnabled = true;
            this.cmbWeaponIdx.ItemHeight = 15;
            this.cmbWeaponIdx.Location = new System.Drawing.Point(89, 84);
            this.cmbWeaponIdx.Name = "cmbWeaponIdx";
            this.cmbWeaponIdx.Size = new System.Drawing.Size(50, 21);
            this.cmbWeaponIdx.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbWeaponIdx.TabIndex = 13;
            this.cmbWeaponIdx.SelectedIndexChanged += new System.EventHandler(this.cmbWeaponIdx_SelectedIndexChanged);
            // 
            // cmbWeaponType
            // 
            this.cmbWeaponType.DisplayMember = "Text";
            this.cmbWeaponType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbWeaponType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWeaponType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbWeaponType.FormattingEnabled = true;
            this.cmbWeaponType.ItemHeight = 15;
            this.cmbWeaponType.Location = new System.Drawing.Point(35, 84);
            this.cmbWeaponType.Name = "cmbWeaponType";
            this.cmbWeaponType.Size = new System.Drawing.Size(50, 21);
            this.cmbWeaponType.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbWeaponType.TabIndex = 12;
            this.cmbWeaponType.SelectedIndexChanged += new System.EventHandler(this.cmbWeaponType_SelectedIndexChanged);
            // 
            // labelX4
            // 
            this.labelX4.AutoSize = true;
            this.labelX4.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(3, 87);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(31, 18);
            this.labelX4.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.labelX4.TabIndex = 13;
            this.labelX4.Text = "武器";
            // 
            // chkTamingPlay
            // 
            this.chkTamingPlay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkTamingPlay.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.chkTamingPlay.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkTamingPlay.Location = new System.Drawing.Point(184, 57);
            this.chkTamingPlay.Name = "chkTamingPlay";
            this.chkTamingPlay.Size = new System.Drawing.Size(15, 21);
            this.chkTamingPlay.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkTamingPlay.TabIndex = 8;
            this.chkTamingPlay.TextVisible = false;
            this.chkTamingPlay.CheckedChanged += new System.EventHandler(this.chkTamingPlay_CheckedChanged);
            // 
            // chkEmotionPlay
            // 
            this.chkEmotionPlay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkEmotionPlay.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.chkEmotionPlay.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkEmotionPlay.Location = new System.Drawing.Point(184, 30);
            this.chkEmotionPlay.Name = "chkEmotionPlay";
            this.chkEmotionPlay.Size = new System.Drawing.Size(15, 21);
            this.chkEmotionPlay.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkEmotionPlay.TabIndex = 7;
            this.chkEmotionPlay.TextVisible = false;
            this.chkEmotionPlay.CheckedChanged += new System.EventHandler(this.chkEmotionPlay_CheckedChanged);
            // 
            // chkBodyPlay
            // 
            this.chkBodyPlay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkBodyPlay.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.chkBodyPlay.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkBodyPlay.Location = new System.Drawing.Point(184, 3);
            this.chkBodyPlay.Name = "chkBodyPlay";
            this.chkBodyPlay.Size = new System.Drawing.Size(15, 21);
            this.chkBodyPlay.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkBodyPlay.TabIndex = 6;
            this.chkBodyPlay.TextVisible = false;
            this.chkBodyPlay.CheckedChanged += new System.EventHandler(this.chkBodyPlay_CheckedChanged);
            // 
            // cmbTamingFrame
            // 
            this.cmbTamingFrame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbTamingFrame.DisplayMember = "Text";
            this.cmbTamingFrame.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbTamingFrame.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTamingFrame.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbTamingFrame.FormattingEnabled = true;
            this.cmbTamingFrame.ItemHeight = 15;
            this.cmbTamingFrame.Location = new System.Drawing.Point(128, 57);
            this.cmbTamingFrame.Name = "cmbTamingFrame";
            this.cmbTamingFrame.Size = new System.Drawing.Size(50, 21);
            this.cmbTamingFrame.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbTamingFrame.TabIndex = 5;
            this.cmbTamingFrame.SelectedIndexChanged += new System.EventHandler(this.cmbTamingFrame_SelectedIndexChanged);
            // 
            // cmbEmotionFrame
            // 
            this.cmbEmotionFrame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbEmotionFrame.DisplayMember = "Text";
            this.cmbEmotionFrame.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbEmotionFrame.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEmotionFrame.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbEmotionFrame.FormattingEnabled = true;
            this.cmbEmotionFrame.ItemHeight = 15;
            this.cmbEmotionFrame.Location = new System.Drawing.Point(128, 30);
            this.cmbEmotionFrame.Name = "cmbEmotionFrame";
            this.cmbEmotionFrame.Size = new System.Drawing.Size(50, 21);
            this.cmbEmotionFrame.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbEmotionFrame.TabIndex = 4;
            this.cmbEmotionFrame.SelectedIndexChanged += new System.EventHandler(this.cmbEmotionFrame_SelectedIndexChanged);
            // 
            // cmbBodyFrame
            // 
            this.cmbBodyFrame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbBodyFrame.DisplayMember = "Text";
            this.cmbBodyFrame.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbBodyFrame.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBodyFrame.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbBodyFrame.FormattingEnabled = true;
            this.cmbBodyFrame.ItemHeight = 15;
            this.cmbBodyFrame.Location = new System.Drawing.Point(128, 3);
            this.cmbBodyFrame.Name = "cmbBodyFrame";
            this.cmbBodyFrame.Size = new System.Drawing.Size(50, 21);
            this.cmbBodyFrame.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbBodyFrame.TabIndex = 3;
            this.cmbBodyFrame.SelectedIndexChanged += new System.EventHandler(this.cmbBodyFrame_SelectedIndexChanged);
            // 
            // cmbActionTaming
            // 
            this.cmbActionTaming.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbActionTaming.DisplayMember = "Text";
            this.cmbActionTaming.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbActionTaming.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbActionTaming.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbActionTaming.FormattingEnabled = true;
            this.cmbActionTaming.ItemHeight = 15;
            this.cmbActionTaming.Location = new System.Drawing.Point(35, 57);
            this.cmbActionTaming.Name = "cmbActionTaming";
            this.cmbActionTaming.Size = new System.Drawing.Size(87, 21);
            this.cmbActionTaming.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbActionTaming.TabIndex = 2;
            this.cmbActionTaming.SelectedIndexChanged += new System.EventHandler(this.cmbActionTaming_SelectedIndexChanged);
            // 
            // cmbEmotion
            // 
            this.cmbEmotion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbEmotion.DisplayMember = "Text";
            this.cmbEmotion.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbEmotion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEmotion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbEmotion.FormattingEnabled = true;
            this.cmbEmotion.ItemHeight = 15;
            this.cmbEmotion.Location = new System.Drawing.Point(35, 30);
            this.cmbEmotion.Name = "cmbEmotion";
            this.cmbEmotion.Size = new System.Drawing.Size(87, 21);
            this.cmbEmotion.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbEmotion.TabIndex = 1;
            this.cmbEmotion.SelectedIndexChanged += new System.EventHandler(this.cmbEmotion_SelectedIndexChanged);
            // 
            // labelX3
            // 
            this.labelX3.AutoSize = true;
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(3, 60);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(31, 18);
            this.labelX3.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.labelX3.TabIndex = 3;
            this.labelX3.Text = "乗る";
            // 
            // labelX2
            // 
            this.labelX2.AutoSize = true;
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(3, 33);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(31, 18);
            this.labelX2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.labelX2.TabIndex = 2;
            this.labelX2.Text = "表情";
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(3, 6);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(31, 18);
            this.labelX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.labelX1.TabIndex = 1;
            this.labelX1.Text = "行動";
            // 
            // cmbActionBody
            // 
            this.cmbActionBody.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbActionBody.DisplayMember = "Text";
            this.cmbActionBody.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbActionBody.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbActionBody.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbActionBody.FormattingEnabled = true;
            this.cmbActionBody.ItemHeight = 15;
            this.cmbActionBody.Location = new System.Drawing.Point(35, 3);
            this.cmbActionBody.Name = "cmbActionBody";
            this.cmbActionBody.Size = new System.Drawing.Size(87, 21);
            this.cmbActionBody.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbActionBody.TabIndex = 0;
            this.cmbActionBody.SelectedIndexChanged += new System.EventHandler(this.cmbActionBody_SelectedIndexChanged);
            // 
            // chkHairShade
            // 
            this.chkHairShade.AutoSize = true;
            this.chkHairShade.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.chkHairShade.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkHairShade.Location = new System.Drawing.Point(82, 128);
            this.chkHairShade.Name = "chkHairShade";
            this.chkHairShade.Size = new System.Drawing.Size(88, 20);
            this.chkHairShade.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkHairShade.TabIndex = 10;
            this.chkHairShade.Text = "髪の影";
            this.chkHairShade.Enabled = false;
            this.chkHairShade.CheckedChanged += new System.EventHandler(this.chkHairShade_CheckedChanged);
            // 
            // chkHairCover
            // 
            this.chkHairCover.AutoSize = true;
            this.chkHairCover.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.chkHairCover.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkHairCover.Location = new System.Drawing.Point(5, 108);
            this.chkHairCover.Name = "chkHairCover";
            this.chkHairCover.Size = new System.Drawing.Size(76, 20);
            this.chkHairCover.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkHairCover.TabIndex = 9;
            this.chkHairCover.Text = "髪を押す";
            this.chkHairCover.CheckedChanged += new System.EventHandler(this.chkHairCover_CheckedChanged);
            // 
            // chkApplyBRM
            // 
            this.chkApplyBRM.AutoSize = true;
            this.chkApplyBRM.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.chkApplyBRM.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkApplyBRM.Location = new System.Drawing.Point(5, 155);
            this.chkApplyBRM.Name = "chkApplyBRM";
            this.chkApplyBRM.Size = new System.Drawing.Size(172, 18);
            this.chkApplyBRM.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkApplyBRM.TabIndex = 16;
            this.chkApplyBRM.Text = "プレーヤーの位置調整（椅子）";
            this.chkApplyBRM.CheckedChanged += new System.EventHandler(this.chkApplyBRM_CheckedChanged);
            // 
            // cmbGroupChair
            // 
            this.cmbGroupChair.DisplayMember = "Text";
            this.cmbGroupChair.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbGroupChair.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGroupChair.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbGroupChair.FormattingEnabled = true;
            this.cmbGroupChair.ItemHeight = 15;
            this.cmbGroupChair.Location = new System.Drawing.Point(70, 129);
            this.cmbGroupChair.Name = "cmbGroupChair";
            this.cmbGroupChair.Size = new System.Drawing.Size(50, 21);
            this.cmbGroupChair.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbGroupChair.TabIndex = 18;
            this.cmbGroupChair.Enabled = false;
            this.cmbGroupChair.SelectedIndexChanged += new System.EventHandler(this.cmbGroupChair_SelectedIndexChanged);
            // 
            // dockContainerItem2
            // 
            this.dockContainerItem2.Control = this.panelDockContainer2;
            this.dockContainerItem2.Name = "dockContainerItem2";
            this.dockContainerItem2.Text = "行動";
            // 
            // dockSite8
            // 
            this.dockSite8.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.dockSite8.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dockSite8.Location = new System.Drawing.Point(0, 539);
            this.dockSite8.Name = "dockSite8";
            this.dockSite8.Size = new System.Drawing.Size(775, 0);
            this.dockSite8.TabIndex = 7;
            this.dockSite8.TabStop = false;
            // 
            // dockSite5
            // 
            this.dockSite5.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.dockSite5.Dock = System.Windows.Forms.DockStyle.Left;
            this.dockSite5.Location = new System.Drawing.Point(0, 27);
            this.dockSite5.Name = "dockSite5";
            this.dockSite5.Size = new System.Drawing.Size(0, 512);
            this.dockSite5.TabIndex = 4;
            this.dockSite5.TabStop = false;
            // 
            // dockSite6
            // 
            this.dockSite6.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.dockSite6.Dock = System.Windows.Forms.DockStyle.Right;
            this.dockSite6.Location = new System.Drawing.Point(775, 27);
            this.dockSite6.Name = "dockSite6";
            this.dockSite6.Size = new System.Drawing.Size(0, 512);
            this.dockSite6.TabIndex = 5;
            this.dockSite6.TabStop = false;
            // 
            // dockSite7
            // 
            this.dockSite7.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.dockSite7.Controls.Add(this.bar3);
            this.dockSite7.Dock = System.Windows.Forms.DockStyle.Top;
            this.dockSite7.Location = new System.Drawing.Point(0, 0);
            this.dockSite7.Name = "dockSite7";
            this.dockSite7.Size = new System.Drawing.Size(775, 27);
            this.dockSite7.TabIndex = 6;
            this.dockSite7.TabStop = false;
            // 
            // bar3
            // 
            this.bar3.AccessibleDescription = "DotNetBar Bar (bar3)";
            this.bar3.AccessibleName = "DotNetBar Bar";
            this.bar3.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolBar;
            this.bar3.DockSide = DevComponents.DotNetBar.eDockSide.Top;
            this.bar3.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.bar3.GrabHandleStyle = DevComponents.DotNetBar.eGrabHandleStyle.Office2003;
            this.bar3.IsMaximized = false;
            this.bar3.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnCode,
            this.btnCharac,
            this.btnReset,
            this.btnLock,
            this.btnSaveAsGif,
            this.btnSaveOptions,
            this.btnExport});
            this.bar3.Location = new System.Drawing.Point(0, 0);
            this.bar3.Name = "bar3";
            this.bar3.Size = new System.Drawing.Size(168, 27);
            this.bar3.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bar3.TabIndex = 0;
            this.bar3.TabStop = false;
            this.bar3.Text = "ツール";
            // 
            // btnCode
            // 
            this.btnCode.Image = global::WzComparerR2.Avatar.Properties.Resources.script_code;
            this.btnCode.Name = "btnCode";
            this.btnCode.Tooltip = "コード";
            this.btnCode.Click += new System.EventHandler(this.btnCode_Click);
            //
            // Separator1
            //
            this.Separator1.SeparatorOrientation = DevComponents.DotNetBar.eDesignMarkerOrientation.Vertical;
            //
            // Separator2
            //
            this.Separator2.SeparatorOrientation = DevComponents.DotNetBar.eDesignMarkerOrientation.Vertical;
            //
            // Separator3
            //
            this.Separator3.SeparatorOrientation = DevComponents.DotNetBar.eDesignMarkerOrientation.Vertical;
            //
            // Separator4
            //
            this.Separator4.SeparatorOrientation = DevComponents.DotNetBar.eDesignMarkerOrientation.Vertical;
            // 
            // btnCharac
            // 
            this.btnCharac.AutoExpandOnClick = true;
            this.btnCharac.Image = global::WzComparerR2.Avatar.Properties.Resources.user;
            this.btnCharac.Name = "btnCharac";
            this.btnCharac.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnCustomPreset,
            this.btnAPI,
            this.Separator1,
            this.btnMale,
            this.btnFemale,
            this.Separator2,
            this.btnLaraTheSheep,
            this.Separator3,
            this.btnAngelicBuster,
            this.btnOldBokugen,
            this.btnNewBokugen,
            this.btnKanna,
            this.btnZero,
            this.btnPathfinder,
            this.btnHayato,
            this.btnBeastTamer,
            this.btnLara,
            this.btnLynn,
            this.Separator4,
            this.btnPopuko,
            this.btnPipimi,
            this.btnMegumin,
            this.btnAqua,
            this.btnDarkness,
            this.btnTanjiroKamado,
            this.btnNezukoKamado,
            this.btnZenitsuAgatsuma,
            this.btnInosukeHashibira});
            this.btnCharac.Tooltip = "初期化";
            // 
            // 
            // btnCustomPreset
            // 
            this.btnCustomPreset.Name = "btnCustomPreset";
            this.btnCustomPreset.Text = "カスタムプリセット";
            this.btnCustomPreset.Click += new System.EventHandler(this.btnCustomPreset_Click);
            // 
            // btnMale
            // 
            this.btnMale.Name = "btnMale";
            this.btnMale.Text = "男性キャラ";
            this.btnMale.Click += new System.EventHandler(this.btnMale_Click);
            // 
            // btnFemale
            // 
            this.btnFemale.Name = "btnFemale";
            this.btnFemale.Text = "女性キャラ";
            this.btnFemale.Click += new System.EventHandler(this.btnFemale_Click);
            // 
            // 
            // btnLaraTheSheep
            // 
            this.btnLaraTheSheep.Name = "btnLaraTheSheep";
            this.btnLaraTheSheep.Text = "光卡的拉羊羊";
            this.btnLaraTheSheep.Click += new System.EventHandler(this.btnLaraTheSheep_Click);
            // 
            // 
            // btnHayato
            // 
            this.btnHayato.Name = "btnHayato";
            this.btnHayato.Text = "ハヤト";
            this.btnHayato.Click += new System.EventHandler(this.btnHayato_Click);
            // 
            // 
            // btnKanna
            // 
            this.btnKanna.Name = "btnKanna";
            this.btnKanna.Text = "カンナ";
            this.btnKanna.Click += new System.EventHandler(this.btnKanna_Click);
            // 
            // 
            // btnZero
            // 
            this.btnZero.Name = "btnZero";
            this.btnZero.Text = "ゼロ";
            this.btnZero.Click += new System.EventHandler(this.btnZero_Click);
            // 
            // 
            // btnBeastTamer
            // 
            this.btnBeastTamer.Name = "btnBeastTamer";
            this.btnBeastTamer.Text = "ビーストテーマー";
            this.btnBeastTamer.Click += new System.EventHandler(this.btnBeastTamer_Click);
            // 
            // 
            // btnPathfinder
            // 
            this.btnPathfinder.Name = "btnPathfinder";
            this.btnPathfinder.Text = "パスファインダー";
            this.btnPathfinder.Click += new System.EventHandler(this.btnPathfinder_Click);
            // 
            // 
            // btnLara
            // 
            this.btnLara.Name = "btnLara";
            this.btnLara.Text = "ララ";
            this.btnLara.Click += new System.EventHandler(this.btnLara_Click);
            // 
            // 
            // btnLynn
            // 
            this.btnLynn.Name = "btnLynn";
            this.btnLynn.Text = "リン";
            this.btnLynn.Click += new System.EventHandler(this.btnLynn_Click);
            // 
            // 
            // btnAngelicBuster
            // 
            this.btnAngelicBuster.Name = "btnAngelicBuster";
            this.btnAngelicBuster.Text = "エンジェリックバスター";
            this.btnAngelicBuster.Click += new System.EventHandler(this.btnAngelicBuster_Click);
            // 
            // 
            // btnOldBokugen
            // 
            this.btnOldBokugen.Name = "btnOldBokugen";
            this.btnOldBokugen.Text = "墨玄(古い)";
            this.btnOldBokugen.Click += new System.EventHandler(this.btnOldBokugen_Click);
            // 
            // 
            // btnNewBokugen
            // 
            this.btnNewBokugen.Name = "btnNewBokugen";
            this.btnNewBokugen.Text = "墨玄(新しい)";
            this.btnNewBokugen.Click += new System.EventHandler(this.btnNewBokugen_Click);
            // 
            // 
            // btnPopuko
            // 
            this.btnPopuko.Name = "btnPopuko";
            this.btnPopuko.Text = "ポプ子";
            this.btnPopuko.Click += new System.EventHandler(this.btnPopuko_Click);
            // 
            // 
            // btnPipimi
            // 
            this.btnPipimi.Name = "btnPipimi";
            this.btnPipimi.Text = "ピピ美";
            this.btnPipimi.Click += new System.EventHandler(this.btnPipimi_Click);
            // 
            // 
            // btnMegumin
            // 
            this.btnMegumin.Name = "btnMegumin";
            this.btnMegumin.Text = "めぐみん";
            this.btnMegumin.Click += new System.EventHandler(this.btnMegumin_Click);
            // 
            // 
            // btnAqua
            // 
            this.btnAqua.Name = "btnAqua";
            this.btnAqua.Text = "アクア";
            this.btnAqua.Click += new System.EventHandler(this.btnAqua_Click);
            // 
            // 
            // btnDarkness
            // 
            this.btnDarkness.Name = "btnDarkness";
            this.btnDarkness.Text = "ダクネス";
            this.btnDarkness.Click += new System.EventHandler(this.btnDarkness_Click);
            // 
            // 
            // btnTanjiroKamado
            // 
            this.btnTanjiroKamado.Name = "btnTanjiroKamado";
            this.btnTanjiroKamado.Text = "竈門炭治郎";
            this.btnTanjiroKamado.Click += new System.EventHandler(this.btnTanjiroKamado_Click);
            // 
            // 
            // btnNezukoKamado
            // 
            this.btnNezukoKamado.Name = "btnNezukoKamado";
            this.btnNezukoKamado.Text = "竈門禰豆子";
            this.btnNezukoKamado.Click += new System.EventHandler(this.btnNezukoKamado_Click);
            // 
            // 
            // btnZenitsuAgatsuma
            // 
            this.btnZenitsuAgatsuma.Name = "btnZenitsuAgatsuma";
            this.btnZenitsuAgatsuma.Text = "我妻善逸";
            this.btnZenitsuAgatsuma.Click += new System.EventHandler(this.btnZenitsuAgatsuma_Click);
            // 
            // 
            // btnInosukeHashibira
            // 
            this.btnInosukeHashibira.Name = "btnInosukeHashibira";
            this.btnInosukeHashibira.Text = "嘴平伊之助";
            this.btnInosukeHashibira.Click += new System.EventHandler(this.btnInosukeHashibira_Click);
            // btnAPI
            // 
            this.btnAPI.Name = "btnAPI";
            this.btnAPI.Text = "実際のキャラクター";
            this.btnAPI.Click += new System.EventHandler(this.btnAPI_Click);
            // 
            // btnReset
            // 
            this.btnReset.Image = global::WzComparerR2.Avatar.Properties.Resources.arrow_in;
            this.btnReset.Name = "btnReset";
            this.btnReset.Tooltip = "最初の位置に";
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnLock
            // 
            this.btnLock.AutoCheckOnClick = true;
            this.btnLock.Image = global::WzComparerR2.Avatar.Properties.Resources._lock;
            this.btnLock.Name = "btnLock";
            this.btnLock.Tooltip = "ロック";
            // 
            // btnSaveAsGif
            // 
            this.btnSaveAsGif.Image = global::WzComparerR2.Avatar.Properties.Resources.disk;
            this.btnSaveAsGif.Name = "btnSaveAsGif";
            this.btnSaveAsGif.Tooltip = "保存";
            this.btnSaveAsGif.Click += new System.EventHandler(this.btnSaveAsGif_Click);
            // 
            // btnSaveOptions
            // 
            this.btnSaveOptions.AutoExpandOnClick = true;
            this.btnSaveOptions.Image = global::WzComparerR2.Avatar.Properties.Resources.autosave;
            this.btnSaveOptions.Name = "btnSaveOptions";
            this.btnSaveOptions.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnEnableAutosave,
            this.btnSpecifySavePath});
            this.btnSaveOptions.Tooltip = "自動保存オプション";
            // 
            // btnEnableAutosave
            // 
            this.btnEnableAutosave.AutoCheckOnClick = true;
            this.btnEnableAutosave.Name = "btnEnableAutosave";
            this.btnEnableAutosave.Text = "自動保存の有効化";
            this.btnEnableAutosave.Click += new System.EventHandler(this.btnEnableAutosave_Click);
            // 
            // btnSpecifySavePath
            // 
            this.btnSpecifySavePath.Name = "btnSpecifySavePath";
            this.btnSpecifySavePath.Text = "保存フォルダーを指定...";
            this.btnSpecifySavePath.Enabled = this.btnEnableAutosave.Checked;
            this.btnSpecifySavePath.Click += new System.EventHandler(this.btnSpecifySavePath_Click);
            // dockSite3
            // 
            this.dockSite3.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.dockSite3.Dock = System.Windows.Forms.DockStyle.Top;
            this.dockSite3.DocumentDockContainer = new DevComponents.DotNetBar.DocumentDockContainer();
            this.dockSite3.Location = new System.Drawing.Point(0, 27);
            this.dockSite3.Name = "dockSite3";
            this.dockSite3.Size = new System.Drawing.Size(775, 0);
            this.dockSite3.TabIndex = 2;
            this.dockSite3.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // avatarContainer1
            // 
            this.avatarContainer1.BackColor = System.Drawing.Color.White;
            this.avatarContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.avatarContainer1.Location = new System.Drawing.Point(0, 27);
            this.avatarContainer1.Name = "avatarContainer1";
            this.avatarContainer1.Origin = new System.Drawing.Point(0, 0);
            this.avatarContainer1.Size = new System.Drawing.Size(559, 512);
            this.avatarContainer1.TabIndex = 8;
            this.avatarContainer1.Text = "avatarContainer1";
            // 
            // labelX5
            // 
            this.labelX5.AutoSize = true;
            this.labelX5.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Location = new System.Drawing.Point(139, 87);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(25, 16);
            this.labelX5.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.labelX5.TabIndex = 14;
            this.labelX5.Text = "耳";
            // 
            // labelX6
            // 
            this.labelX6.AutoSize = true;
            this.labelX6.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX6.Location = new System.Drawing.Point(3, 131);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(60, 18);
            this.labelX6.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.labelX6.TabIndex = 17;
            this.labelX6.Text = "座る人数";
            // 
            // cmbEar
            // 
            this.cmbEar.DisplayMember = "Text";
            this.cmbEar.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbEar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEar.FormattingEnabled = true;
            this.cmbEar.ItemHeight = 15;
            this.cmbEar.Location = new System.Drawing.Point(163, 84);
            this.cmbEar.Name = "cmbEar";
            this.cmbEar.Size = new System.Drawing.Size(39, 21);
            this.cmbEar.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbEar.TabIndex = 15;
            this.cmbEar.SelectedIndexChanged += new System.EventHandler(this.cmbEar_SelectedIndexChanged);
            //
            // btnExport
            //
            this.btnExport.Name = "btnExport";
            this.btnExport.Image = global::WzComparerR2.Avatar.Properties.Resources.export;
            this.btnExport.Tooltip = "アクションのエクスポート";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // AvatarForm
            // 
            this.ClientSize = new System.Drawing.Size(775, 539);
            this.Controls.Add(this.avatarContainer1);
            this.Controls.Add(this.dockSite2);
            this.Controls.Add(this.dockSite1);
            this.Controls.Add(this.dockSite3);
            this.Controls.Add(this.dockSite4);
            this.Controls.Add(this.dockSite5);
            this.Controls.Add(this.dockSite6);
            this.Controls.Add(this.dockSite7);
            this.Controls.Add(this.dockSite8);
            this.DoubleBuffered = true;
            this.Name = "AvatarForm";
            this.Text = "アバター";
            this.dockSite2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).EndInit();
            this.bar1.ResumeLayout(false);
            this.panelDockContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bar2)).EndInit();
            this.bar2.ResumeLayout(false);
            this.panelDockContainer2.ResumeLayout(false);
            this.panelDockContainer2.PerformLayout();
            this.dockSite7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bar3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.DotNetBarManager dotNetBarManager1;
        private DevComponents.DotNetBar.DockSite dockSite4;
        private DevComponents.DotNetBar.DockSite dockSite1;
        private DevComponents.DotNetBar.DockSite dockSite2;
        private DevComponents.DotNetBar.Bar bar1;
        private DevComponents.DotNetBar.PanelDockContainer panelDockContainer1;
        private DevComponents.DotNetBar.DockContainerItem dockContainerItem1;
        private DevComponents.DotNetBar.Bar bar2;
        private DevComponents.DotNetBar.PanelDockContainer panelDockContainer2;
        private DevComponents.DotNetBar.DockContainerItem dockContainerItem2;
        private DevComponents.DotNetBar.DockSite dockSite3;
        private DevComponents.DotNetBar.DockSite dockSite5;
        private DevComponents.DotNetBar.DockSite dockSite6;
        private DevComponents.DotNetBar.DockSite dockSite7;
        private DevComponents.DotNetBar.DockSite dockSite8;
        private DevComponents.DotNetBar.ItemPanel itemPanel1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbActionTaming;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbEmotion;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbActionBody;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkTamingPlay;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkEmotionPlay;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkBodyPlay;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbTamingFrame;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbEmotionFrame;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbBodyFrame;
        private System.Windows.Forms.Timer timer1;
        private AvatarContainer avatarContainer1;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkHairCover;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkApplyBRM;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbWeaponIdx;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbWeaponType;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.Bar bar3;
        private DevComponents.DotNetBar.ButtonItem btnCode;
        private DevComponents.DotNetBar.ButtonItem btnCharac;
        private DevComponents.DotNetBar.ButtonItem btnReset;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkHairShade;
        private DevComponents.DotNetBar.ButtonItem btnLock;
        private DevComponents.DotNetBar.ButtonItem btnMale;
        private DevComponents.DotNetBar.ButtonItem btnFemale;
        private DevComponents.DotNetBar.ButtonItem btnHayato;
        private DevComponents.DotNetBar.ButtonItem btnKanna;
        private DevComponents.DotNetBar.ButtonItem btnZero;
        private DevComponents.DotNetBar.ButtonItem btnBeastTamer;
        private DevComponents.DotNetBar.ButtonItem btnPathfinder;
        private DevComponents.DotNetBar.ButtonItem btnLara;
        private DevComponents.DotNetBar.ButtonItem btnLynn;
        private DevComponents.DotNetBar.ButtonItem btnAngelicBuster;
        private DevComponents.DotNetBar.ButtonItem btnOldBokugen;
        private DevComponents.DotNetBar.ButtonItem btnNewBokugen;
        private DevComponents.DotNetBar.ButtonItem btnPopuko;
        private DevComponents.DotNetBar.ButtonItem btnPipimi;
        private DevComponents.DotNetBar.ButtonItem btnMegumin;
        private DevComponents.DotNetBar.ButtonItem btnAqua;
        private DevComponents.DotNetBar.ButtonItem btnDarkness;
        private DevComponents.DotNetBar.ButtonItem btnTanjiroKamado;
        private DevComponents.DotNetBar.ButtonItem btnNezukoKamado;
        private DevComponents.DotNetBar.ButtonItem btnZenitsuAgatsuma;
        private DevComponents.DotNetBar.ButtonItem btnInosukeHashibira;
        private DevComponents.DotNetBar.ButtonItem btnLaraTheSheep;
        private DevComponents.DotNetBar.ButtonItem btnCustomPreset;
        private DevComponents.DotNetBar.ButtonItem btnAPI;
        private DevComponents.DotNetBar.ButtonItem btnSaveAsGif;
        private DevComponents.DotNetBar.ButtonItem btnSaveOptions;
        private DevComponents.DotNetBar.ButtonItem btnEnableAutosave;
        private DevComponents.DotNetBar.ButtonItem btnSpecifySavePath;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbEar;
        private DevComponents.DotNetBar.Separator Separator1;
        private DevComponents.DotNetBar.Separator Separator2;
        private DevComponents.DotNetBar.Separator Separator3;
        private DevComponents.DotNetBar.Separator Separator4;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.ButtonItem btnExport;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbGroupChair;
    }
}

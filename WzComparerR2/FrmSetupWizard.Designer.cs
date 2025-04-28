using WzComparerR2.Properties;

namespace WzComparerR2
{
    partial class FrmSetupWizard
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
            this.wizard = new DevComponents.DotNetBar.Wizard();
            this.wizardPage1 = new DevComponents.DotNetBar.WizardPage();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.previewCommonWizardPage = new DevComponents.DotNetBar.WizardPage();
            this.translatorWizardPage = new DevComponents.DotNetBar.WizardPage();
            this.behaviorGroupPanel = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.modelSizeNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.modelSizeLabelX = new DevComponents.DotNetBar.LabelX();
            this.animationOutputPage = new DevComponents.DotNetBar.WizardPage();
            this.finishedWizardPage = new DevComponents.DotNetBar.WizardPage();
            this.chkEnable22AniStyle = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.wizard.SuspendLayout();
            this.wizardPage1.SuspendLayout();
            this.previewCommonWizardPage.SuspendLayout();
            this.translatorWizardPage.SuspendLayout();
            this.behaviorGroupPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.modelSizeNumericUpDown)).BeginInit();
            this.animationOutputPage.SuspendLayout();
            this.finishedWizardPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // wizard
            // 
            this.wizard.BackColor = System.Drawing.SystemColors.Control;
            this.wizard.BackButtonText = "< 戻る(&B)";
            this.wizard.NextButtonText = "次へ(&N) >";
            this.wizard.CancelButtonText = "キャンセル(&C)";
            this.wizard.CancelButtonWidth = 80;
            this.wizard.FinishButtonText = "完了(&F)";
            this.wizard.Cursor = System.Windows.Forms.Cursors.Default;
            this.wizard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizard.FinishButtonTabIndex = 3;
            this.wizard.FooterHeight = 42;
            // 
            // 
            // 
            this.wizard.FooterStyle.BackColor = System.Drawing.SystemColors.Control;
            this.wizard.FooterStyle.BackColorGradientAngle = 90;
            this.wizard.FooterStyle.BorderBottomWidth = 1;
            this.wizard.FooterStyle.BorderColor = System.Drawing.SystemColors.Control;
            this.wizard.FooterStyle.BorderLeftWidth = 1;
            this.wizard.FooterStyle.BorderRightWidth = 1;
            this.wizard.FooterStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Etched;
            this.wizard.FooterStyle.BorderTopColor = System.Drawing.SystemColors.Control;
            this.wizard.FooterStyle.BorderTopWidth = 1;
            this.wizard.FooterStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.wizard.FooterStyle.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.wizard.FooterStyle.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.wizard.ForeColor = System.Drawing.SystemColors.ControlText;
            this.wizard.HeaderCaptionFont = new System.Drawing.Font("MS PGothic", 12F, System.Drawing.FontStyle.Bold);
            this.wizard.HeaderDescriptionFont = new System.Drawing.Font("MS PGothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.wizard.HeaderDescriptionIndent = 16;
            this.wizard.HeaderHeight = 60;
            this.wizard.HeaderImage = null;
            this.wizard.HeaderImageSize = new System.Drawing.Size(0, 0);
            // 
            // 
            // 
            this.wizard.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.wizard.HeaderStyle.BackColorGradientAngle = 90;
            this.wizard.HeaderStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Etched;
            this.wizard.HeaderStyle.BorderBottomWidth = 1;
            this.wizard.HeaderStyle.BorderColor = System.Drawing.SystemColors.Control;
            this.wizard.HeaderStyle.BorderLeftWidth = 1;
            this.wizard.HeaderStyle.BorderRightWidth = 1;
            this.wizard.HeaderStyle.BorderTopWidth = 1;
            this.wizard.HeaderStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.wizard.HeaderStyle.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.wizard.HeaderStyle.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.wizard.HelpButtonVisible = false;
            this.wizard.Location = new System.Drawing.Point(0, 0);
            this.wizard.Name = "wizard";
            this.wizard.Size = new System.Drawing.Size(879, 538);
            this.wizard.TabIndex = 0;
            this.wizard.WizardPages.AddRange(new DevComponents.DotNetBar.WizardPage[] {
            this.wizardPage1,
            this.previewCommonWizardPage,
            this.translatorWizardPage,
            this.animationOutputPage,
            this.finishedWizardPage});
            this.wizard.FinishButtonClick += new System.ComponentModel.CancelEventHandler(this.wizard_FinishButtonClick);
            this.wizard.WizardPageChanging += new DevComponents.DotNetBar.WizardCancelPageChangeEventHandler(this.wizard_WizardPageChanging);
            // 
            // wizardPage1
            // 
            this.wizardPage1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.wizardPage1.BackColor = System.Drawing.Color.White;
            this.wizardPage1.Controls.Add(this.label1);
            this.wizardPage1.Controls.Add(this.label2);
            this.wizardPage1.Controls.Add(this.label3);
            this.wizardPage1.InteriorPage = false;
            this.wizardPage1.Location = new System.Drawing.Point(0, 0);
            this.wizardPage1.Name = "wizardPage1";
            this.wizardPage1.Size = new System.Drawing.Size(879, 496);
            // 
            // 
            // 
            this.wizardPage1.Style.BackColor = System.Drawing.Color.White;
            this.wizardPage1.Style.BackgroundImagePosition = DevComponents.DotNetBar.eStyleBackgroundImage.TopLeft;
            this.wizardPage1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.wizardPage1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.wizardPage1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.wizardPage1.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("MS PGothic", 16F);
            this.label1.Location = new System.Drawing.Point(210, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(655, 61);
            this.label1.TabIndex = 0;
            this.label1.Text = "WzComparerR2-JMSへようこそ";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(210, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(654, 374);
            this.label2.TabIndex = 1;
            this.label2.Text = "このウィザードは、WzComparerR2をより便利に使用できるように設定する方法を説明します。";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(210, 474);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(600, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "続行するには、「次へ(&N)」をクリックします。";
            // 
            // previewCommonWizardPage
            // 
            this.previewCommonWizardPage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.previewCommonWizardPage.AntiAlias = false;
            this.previewCommonWizardPage.Controls.Add(this.chkEnable22AniStyle);
            this.previewCommonWizardPage.Location = new System.Drawing.Point(7, 66);
            this.previewCommonWizardPage.Name = "previewCommonWizardPage";
            this.previewCommonWizardPage.PageTitle = "プレビューオプション";
            this.previewCommonWizardPage.PageDescription = "これらのオプションはツールチップのレイアウトに影響します。";
            this.previewCommonWizardPage.Size = new System.Drawing.Size(865, 418);
            // 
            // 
            // 
            this.previewCommonWizardPage.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.previewCommonWizardPage.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.previewCommonWizardPage.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.previewCommonWizardPage.TabIndex = 12;
            // 
            // chkEnable22AniStyle
            // 
            this.chkEnable22AniStyle.AutoSize = true;
            this.chkEnable22AniStyle.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.chkEnable22AniStyle.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkEnable22AniStyle.Location = new System.Drawing.Point(13, 12);
            this.chkEnable22AniStyle.Name = "chkEnable22AniStyle";
            this.chkEnable22AniStyle.Size = new System.Drawing.Size(145, 16);
            this.chkEnable22AniStyle.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkEnable22AniStyle.TabIndex = 4;
            this.chkEnable22AniStyle.Text = "22周年記念テーマを有効にする";
            // 
            // translatorWizardPage
            // 
            this.translatorWizardPage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.translatorWizardPage.AntiAlias = false;
            this.translatorWizardPage.Controls.Add(this.behaviorGroupPanel);
            this.translatorWizardPage.Location = new System.Drawing.Point(7, 66);
            this.translatorWizardPage.Name = "translatorWizardPage";
            this.translatorWizardPage.PageTitle = "翻訳機能";
            this.translatorWizardPage.PageDescription = "翻訳機能を使用すると、外国語で新しいコンテンツを理解できるようになります。";
            this.translatorWizardPage.Size = new System.Drawing.Size(865, 418);
            // 
            // 
            // 
            this.translatorWizardPage.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.translatorWizardPage.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.translatorWizardPage.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.translatorWizardPage.TabIndex = 18;
            // 
            // behaviorGroupPanel
            // 
            this.behaviorGroupPanel.CanvasColor = System.Drawing.SystemColors.Control;
            this.behaviorGroupPanel.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.behaviorGroupPanel.Controls.Add(this.modelSizeNumericUpDown);
            this.behaviorGroupPanel.Controls.Add(this.modelSizeLabelX);
            this.behaviorGroupPanel.DisabledBackColor = System.Drawing.Color.Empty;
            this.behaviorGroupPanel.Location = new System.Drawing.Point(193, 112);
            this.behaviorGroupPanel.Name = "behaviorGroupPanel";
            this.behaviorGroupPanel.Size = new System.Drawing.Size(456, 164);
            // 
            // 
            // 
            this.behaviorGroupPanel.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.behaviorGroupPanel.Style.BackColorGradientAngle = 90;
            this.behaviorGroupPanel.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.behaviorGroupPanel.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.behaviorGroupPanel.Style.BorderBottomWidth = 1;
            this.behaviorGroupPanel.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.behaviorGroupPanel.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.behaviorGroupPanel.Style.BorderLeftWidth = 1;
            this.behaviorGroupPanel.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.behaviorGroupPanel.Style.BorderRightWidth = 1;
            this.behaviorGroupPanel.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.behaviorGroupPanel.Style.BorderTopWidth = 1;
            this.behaviorGroupPanel.Style.CornerDiameter = 4;
            this.behaviorGroupPanel.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.behaviorGroupPanel.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.behaviorGroupPanel.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.behaviorGroupPanel.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.behaviorGroupPanel.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.behaviorGroupPanel.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.behaviorGroupPanel.TabIndex = 0;
            this.behaviorGroupPanel.Text = "Behavior";
            // 
            // modelSizeNumericUpDown
            // 
            this.modelSizeNumericUpDown.DecimalPlaces = 1;
            this.modelSizeNumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.modelSizeNumericUpDown.Location = new System.Drawing.Point(213, 63);
            this.modelSizeNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.modelSizeNumericUpDown.Name = "modelSizeNumericUpDown";
            this.modelSizeNumericUpDown.Size = new System.Drawing.Size(66, 19);
            this.modelSizeNumericUpDown.TabIndex = 8;
            this.modelSizeNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // modelSizeLabelX
            // 
            this.modelSizeLabelX.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.modelSizeLabelX.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.modelSizeLabelX.Font = new System.Drawing.Font("MS PGothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.modelSizeLabelX.Location = new System.Drawing.Point(137, 61);
            this.modelSizeLabelX.Name = "modelSizeLabelX";
            this.modelSizeLabelX.Size = new System.Drawing.Size(69, 21);
            this.modelSizeLabelX.TabIndex = 7;
            this.modelSizeLabelX.Text = "Model Size";
            // 
            // animationOutputPage
            // 
            this.animationOutputPage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.animationOutputPage.AntiAlias = false;
            this.animationOutputPage.Location = new System.Drawing.Point(7, 66);
            this.animationOutputPage.Name = "animationOutputPage";
            this.animationOutputPage.PageTitle = "アニメーション出力";
            this.animationOutputPage.PageDescription = "アニメーションをエクスポートする方法を設定できます。";
            this.animationOutputPage.Size = new System.Drawing.Size(865, 418);
            // 
            // 
            // 
            this.animationOutputPage.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.animationOutputPage.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.animationOutputPage.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.animationOutputPage.TabIndex = 19;
            // 
            // finishedWizardPage
            // 
            this.finishedWizardPage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.finishedWizardPage.BackColor = System.Drawing.Color.White;
            this.finishedWizardPage.InteriorPage = false;
            this.finishedWizardPage.AntiAlias = false;
            this.finishedWizardPage.Controls.Add(this.label4);
            this.finishedWizardPage.Controls.Add(this.label5);
            this.finishedWizardPage.Controls.Add(this.label6);
            this.finishedWizardPage.Location = new System.Drawing.Point(0, 0);
            this.finishedWizardPage.Name = "finishedWizardPage";
            this.finishedWizardPage.Size = new System.Drawing.Size(879, 496);
            // 
            // 
            // 
            this.finishedWizardPage.Style.BackColor = System.Drawing.Color.White;
            this.finishedWizardPage.Style.BackgroundImagePosition = DevComponents.DotNetBar.eStyleBackgroundImage.TopLeft;
            this.finishedWizardPage.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.finishedWizardPage.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.finishedWizardPage.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.finishedWizardPage.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("MS PGothic", 16F);
            this.label4.Location = new System.Drawing.Point(210, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(655, 61);
            this.label4.TabIndex = 0;
            this.label4.Text = "設定完了";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(210, 92);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(654, 374);
            this.label5.TabIndex = 1;
            this.label5.Text = "初回実行ウィザードが終了しました。";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(210, 474);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(600, 12);
            this.label6.TabIndex = 2;
            this.label6.Text = "設定を保存するには、「完了(&F)」をクリックします。";
            // 
            // FrmSetupWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(879, 538);
            this.Controls.Add(this.wizard);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.Font = new System.Drawing.Font("MS PGothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSetupWizard";
            this.Text = "初回実行ウィザード";
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.TopMost = true;
            this.wizard.ResumeLayout(false);
            this.wizardPage1.ResumeLayout(false);
            this.previewCommonWizardPage.ResumeLayout(false);
            this.translatorWizardPage.ResumeLayout(false);
            this.behaviorGroupPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.modelSizeNumericUpDown)).EndInit();
            this.animationOutputPage.ResumeLayout(false);
            this.finishedWizardPage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Wizard wizard;
        private DevComponents.DotNetBar.WizardPage wizardPage1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private DevComponents.DotNetBar.WizardPage finishedWizardPage;
        private DevComponents.DotNetBar.WizardPage previewCommonWizardPage;
        private DevComponents.DotNetBar.WizardPage translatorWizardPage;
        private DevComponents.DotNetBar.Controls.GroupPanel behaviorGroupPanel;
        private System.Windows.Forms.NumericUpDown modelSizeNumericUpDown;
        private DevComponents.DotNetBar.LabelX modelSizeLabelX;
        private DevComponents.DotNetBar.WizardPage animationOutputPage;

        private DevComponents.DotNetBar.Controls.CheckBoxX chkEnable22AniStyle;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkEnableAutoPreview;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkShowID;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkShowPrice;

        private DevComponents.DotNetBar.Controls.CheckBoxX chkEnableTranslate;
        private DevComponents.DotNetBar.LabelX lblPreferredLayout;
        private DevComponents.DotNetBar.LabelX lblPreferredDesiredLangauge;
        private DevComponents.DotNetBar.LabelX lblPreferredTranslateEngine;
        private DevComponents.DotNetBar.LabelX lblMozhiBackend;
        private DevComponents.DotNetBar.LabelX lblLanguageModel;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbPreferredLayout;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbPreferredDesiredLanguage;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbPreferredTranslateEngine;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbMozhiBackend;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbLanguageModel;
    }
}
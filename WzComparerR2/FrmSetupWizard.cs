using DevComponents.DotNetBar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using WzComparerR2.Config;

namespace WzComparerR2
{
    public partial class FrmSetupWizard : DevComponents.DotNetBar.Office2007Form
    {
        public FrmSetupWizard()
        {
#if NET6_0_OR_GREATER
            // https://learn.microsoft.com/en-us/dotnet/core/compatibility/fx-core#controldefaultfont-changed-to-segoe-ui-9pt
            this.Font = new Font(new FontFamily("MS PGothic"), 9f);
#endif
            InitializeComponent();
            this.wizard.HeaderImage = null;
            }

        public bool isFirstRun { get; set; }

        public bool Enable22AniStyle
        {
            get { return chkEnable22AniStyle.Checked; }
            set { chkEnable22AniStyle.Checked = value; }
        }

        private void FrmSetupWizard_Load(object sender, EventArgs e)
        {
            if (isFirstRun) this.label2.Text += "\r\n\r\n初回実行ウィザードを実行するのは今回が初めてです。「キャンセル」をクリックすると、次回からはウィザードが表示されなくなります。このウィザードには、後で「ヘルプ」タブからアクセスできます。";

            //Set Default Values
        }

        private void wizard_WizardPageChanging(object sender, WizardCancelPageChangeEventArgs e)
        {
            if (e.OldPage == translatorWizardPage && e.PageChangeSource == eWizardPageChangeSource.NextButton)
            {
                // if (NameTextBoxX.Text == String.Empty || entryIDIntegerInput.Value == 0 || displayIDIntegerInput.Value == 0)
                if (false)
                {
                    e.Cancel = true;
                }
            }
        }

        private void wizard_FinishButtonClick(object sender, CancelEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public void Load(WcR2Config mainconfig, CharaSimConfig characonfig)
        {
/*            this.NxOpenAPIKey = mainconfig.NxOpenAPIKey;
            this.NxSecretKey = mainconfig.NxSecretKey;
            this.MozhiBackend = mainconfig.MozhiBackend;
            this.LanguageModel = mainconfig.LanguageModel;
            this.OpenAIBackend = mainconfig.OpenAIBackend;
            this.OpenAIExtraOption = mainconfig.OpenAIExtraOption;
            this.OpenAISystemMessage = mainconfig.OpenAISystemMessage;
            this.LMTemperature = mainconfig.LMTemperature;
            this.MaximumToken = mainconfig.MaximumToken;
            this.PreferredTranslateEngine = mainconfig.PreferredTranslateEngine;
            this.DesiredLanguage = mainconfig.DesiredLanguage;
            this.PreferredLayout = mainconfig.PreferredLayout;
            this.DetectCurrency = mainconfig.DetectCurrency;
            this.DesiredCurrency = mainconfig.DesiredCurrency;
            this.PreferredStringCopyMethod = characonfig.PreferredStringCopyMethod;
            this.CopyParsedSkillString = characonfig.CopyParsedSkillString;*/
            this.Enable22AniStyle = characonfig.Enable22AniStyle;
/*            this.ShowSoldPrice = characonfig.Gear.ShowSoldPrice || characonfig.Item.ShowSoldPrice;
            this.ShowPurchasePrice = characonfig.Gear.ShowCashPurchasePrice || characonfig.Item.ShowCashPurchasePrice;
            this.ShowObjectID = characonfig.Gear.ShowID || characonfig.Item.ShowID || characonfig.Skill.ShowID;*/
        }

        public void Save(WcR2Config mainconfig, CharaSimConfig characonfig)
        {
/*            mainconfig.NxOpenAPIKey = this.NxOpenAPIKey;
            mainconfig.NxSecretKey = this.NxSecretKey;
            mainconfig.MozhiBackend = this.MozhiBackend;
            if (this.LanguageModel != "none") config.LanguageModel = this.LanguageModel;
            mainconfig.OpenAIBackend = this.OpenAIBackend;
            mainconfig.OpenAIExtraOption = this.OpenAIExtraOption;
            mainconfig.OpenAISystemMessage = this.OpenAISystemMessage;
            mainconfig.LMTemperature = this.LMTemperature;
            mainconfig.MaximumToken = this.MaximumToken;
            mainconfig.PreferredTranslateEngine = this.PreferredTranslateEngine;
            mainconfig.DesiredLanguage = this.DesiredLanguage;
            mainconfig.PreferredLayout = this.PreferredLayout;
            mainconfig.DetectCurrency = this.DetectCurrency;
            mainconfig.DesiredCurrency = this.DesiredCurrency;
            characonfig.PreferredStringCopyMethod = this.PreferredStringCopyMethod;
            characonfig.CopyParsedSkillString = this.CopyParsedSkillString;*/
            characonfig.Enable22AniStyle = this.Enable22AniStyle;/*
            characonfig.Gear.ShowSoldPrice = this.ShowSoldPrice;
            characonfig.Item.ShowSoldPrice = this.ShowSoldPrice;
            characonfig.Gear.ShowCashPurchasePrice = this.ShowPurchasePrice;
            characonfig.Item.ShowCashPurchasePrice = this.ShowPurchasePrice;
            characonfig.Gear.ShowID = this.ShowObjectID;
            characonfig.Item.ShowID = this.ShowObjectID;
            characonfig.Skill.ShowID = this.ShowObjectID;*/
        }
    }
}
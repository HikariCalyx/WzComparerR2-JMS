using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Drawing;
using WzComparerR2.Patcher;

namespace WzComparerR2.Config
{
    [SectionName("WcR2")]
    public sealed class WcR2Config : ConfigSectionBase<WcR2Config>
    {
        public WcR2Config()
        {
            this.MainStyle = DevComponents.DotNetBar.eStyle.Office2016;
            this.MainStyleColor = Color.DimGray;
            this.SortWzOnOpened = true;
            this.AutoDetectExtFiles = true;
            this.NoPatcherPrompt = false;
            this.WzVersionVerifyMode = WzLib.WzVersionVerifyMode.Fast;
            this.EnableTranslate = false;
            this.EnableCloudTranslateAPI = false;
            this.DesiredLanguage = "ja";
        }

        /// <summary>
        /// 获取最近打开的文档列表。
        /// </summary>
        [ConfigurationProperty("recentDocuments")]
        [ConfigurationCollection(typeof(ConfigArrayList<string>.ItemElement))]
        public ConfigArrayList<string> RecentDocuments
        {
            get { return (ConfigArrayList<string>)this["recentDocuments"]; }
        }

        /// <summary>
        /// 获取或设置主窗体界面样式。
        /// </summary>
        [ConfigurationProperty("mainStyle")]
        public ConfigItem<DevComponents.DotNetBar.eStyle> MainStyle
        {
            get { return (ConfigItem<DevComponents.DotNetBar.eStyle>)this["mainStyle"]; }
            set { this["mainStyle"] = value; }
        }

        /// <summary>
        /// 获取或设置主窗体界面主题色。
        /// </summary>
        [ConfigurationProperty("mainStyleColor")]
        public ConfigItem<Color> MainStyleColor
        {
            get { return (ConfigItem<Color>)this["mainStyleColor"]; }
            set { this["mainStyleColor"] = value; }
        }

        /// <summary>
        /// NXOpenAPI Configuration
        /// </summary>
        [ConfigurationProperty("nxOpenAPIKey")]
        [ConfigurationCollection(typeof(ConfigArrayList<string>.ItemElement))]
        public ConfigItem<string> NxOpenAPIKey
        {
            get { return (ConfigItem<string>)this["nxOpenAPIKey"]; }
            set { this["nxOpenAPIKey"] = value; }
        }

        /// <summary>
        /// GCloudAPI Configuration
        /// </summary>
        [ConfigurationProperty("GCloudAPIKey")]
        [ConfigurationCollection(typeof(ConfigArrayList<string>.ItemElement))]
        public ConfigItem<string> GCloudAPIKey
        {
            get { return (ConfigItem<string>)this["GCloudAPIKey"]; }
            set { this["GCloudAPIKey"] = value; }
        }

        /// <summary>
        /// Desired Language Configuration
        /// </summary>
        [ConfigurationProperty("DesiredLanguage")]
        [ConfigurationCollection(typeof(ConfigArrayList<string>.ItemElement))]
        public ConfigItem<string> DesiredLanguage
        {
            get { return (ConfigItem<string>)this["DesiredLanguage"]; }
            set { this["DesiredLanguage"] = value; }
        }

        /// <summary>
        /// EnableTranslate Configuration
        /// </summary>
        [ConfigurationProperty("EnableTranslate")]
        public ConfigItem<bool> EnableTranslate
        {
            get { return (ConfigItem<bool>)this["EnableTranslate"]; }
            set { this["EnableTranslate"] = value; }
        }

        /// <summary>
        /// EnableCloudTranslateAPI Configuration
        /// </summary>
        [ConfigurationProperty("EnableCloudTranslateAPI")]
        public ConfigItem<bool> EnableCloudTranslateAPI
        {
            get { return (ConfigItem<bool>)this["EnableCloudTranslateAPI"]; }
            set { this["EnableCloudTranslateAPI"] = value; }
        }

        /// <summary>
        /// NXSecretKey Configuration
        /// </summary>
        [ConfigurationProperty("nxSecretKey")]
        [ConfigurationCollection(typeof(ConfigArrayList<string>.ItemElement))]
        public ConfigItem<string> NxSecretKey
        {
            get { return (ConfigItem<string>)this["nxSecretKey"]; }
            set { this["nxSecretKey"] = value; }
        }

        /// <summary>
        /// 获取或设置Wz对比报告默认输出文件夹。
        /// </summary>
        [ConfigurationProperty("comparerOutputFolder")]
        public ConfigItem<string> ComparerOutputFolder
        {
            get { return (ConfigItem<string>)this["comparerOutputFolder"]; }
            set { this["comparerOutputFolder"] = value; }
        }

        /// <summary>
        /// 获取或设置一个值，指示Wz文件加载后是否自动排序。
        /// </summary>
        [ConfigurationProperty("sortWzOnOpened")]
        public ConfigItem<bool> SortWzOnOpened
        {
            get { return (ConfigItem<bool>)this["sortWzOnOpened"]; }
            set { this["sortWzOnOpened"] = value; }
        }

        /// <summary>
        /// 获取或设置一个值，指示Wz文件加载后是否自动排序。
        /// </summary>
        [ConfigurationProperty("sortWzByImgID")]
        public ConfigItem<bool> SortWzByImgID
        {
            get { return (ConfigItem<bool>)this["sortWzByImgID"]; }
            set { this["sortWzByImgID"] = value; }
        }

        /// <summary>
        /// 获取或设置一个值，指示Wz加载中对于ansi字符串的编码。
        /// </summary>
        [ConfigurationProperty("wzEncoding")]
        public ConfigItem<int> WzEncoding
        {
            get { return (ConfigItem<int>)this["wzEncoding"]; }
            set { this["wzEncoding"] = value; }
        }

        /// <summary>
        /// 获取或设置一个值，指示加载Base.wz时是否自动检测扩展wz文件（如Map2、Mob2）。
        /// </summary>
        [ConfigurationProperty("autoDetectExtFiles")]
        public ConfigItem<bool> AutoDetectExtFiles
        {
            get { return (ConfigItem<bool>)this["autoDetectExtFiles"]; }
            set { this["autoDetectExtFiles"] = value; }
        }

        /// <summary>
        /// 获取或设置一个值，指示是否不再提示游戏更新器通知。
        /// </summary>
        [ConfigurationProperty("noPatcherPrompt")]
        public ConfigItem<bool> NoPatcherPrompt
        {
            get { return (ConfigItem<bool>)this["noPatcherPrompt"]; }
            set { this["noPatcherPrompt"] = value; }
        }
        /// <summary>
        /// 获取或设置一个值，指示读取wz是否跳过img检测。
        /// </summary>
        [ConfigurationProperty("imgCheckDisabled")]
        public ConfigItem<bool> ImgCheckDisabled
        {
            get { return (ConfigItem<bool>)this["imgCheckDisabled"]; }
            set { this["imgCheckDisabled"] = value; }
        }

        /// <summary>
        /// 获取或设置一个值，指示读取wz是否跳过img检测。
        /// </summary>
        [ConfigurationProperty("wzVersionVerifyMode")]
        public ConfigItem<WzLib.WzVersionVerifyMode> WzVersionVerifyMode
        {
            get { return (ConfigItem<WzLib.WzVersionVerifyMode>)this["wzVersionVerifyMode"]; }
            set { this["wzVersionVerifyMode"] = value; }
        }

        [ConfigurationProperty("patcherSettings")]
        [ConfigurationCollection(typeof(PatcherSetting), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
        public PatcherSettingCollection PatcherSettings
        {
            get { return (PatcherSettingCollection)this["patcherSettings"]; }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WzComparerR2.Config;

namespace WzComparerR2.CmsdlGui.Config
{
    [SectionName("WcR2.CmsdlGui")]
    public class CmsdlGuiConfig : ConfigSectionBase<CmsdlGuiConfig>
    {
        public CmsdlGuiConfig()
        {
        }

        /// <summary>
        /// Previously selected region
        /// </summary>
        [ConfigurationProperty("selectedRegion")]
        public ConfigItem<int> SelectedRegion
        {
            get { return (ConfigItem<int>)this["selectedRegion"]; }
            set { this["selectedRegion"] = value; }
        }

        /// <summary>
        /// CMS client path
        /// </summary>
        [ConfigurationProperty("cmsPath")]
        public ConfigItem<string> CmsPath
        {
            get { return (ConfigItem<string>)this["cmsPath"]; }
            set { this["cmsPath"] = value; }
        }


        /// <summary>
        /// TMS client path
        /// </summary>
        [ConfigurationProperty("tmsPath")]
        public ConfigItem<string> TmsPath
        {
            get { return (ConfigItem<string>)this["tmsPath"]; }
            set { this["tmsPath"] = value; }
        }
    }

}

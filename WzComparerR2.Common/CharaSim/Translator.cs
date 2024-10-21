using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using WzComparerR2.Config;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
namespace WzComparerR2.CharaSim
{
    public class Translator
    {
        public Translator()
        {
            this.DesiredLanguage = Translator.DefaultDesiredLanguage;
            this.GCloudAPIKey = Translator.DefaultGCloudAPIKey;
        }

        public string DesiredLanguage { get; set; }
        public string GCloudAPIKey { get; set; }

        public static string GCloudAPIBaseURL = "https://translation.googleapis.com/language/translate/v2";
        
        public static bool IsDesiredLanguage(string orgText)
        {
            var request = (HttpWebRequest)WebRequest.Create(GCloudAPIBaseURL + "/detect");
            request.ContentType = "application/json";
            return false;
        }

        public static string TranslateString(string orgText)
        {
            return "¥Õ¥¡¥Õ¥Ë©`¥ë¥Þ¥Ê¥¯¥é¥É¥ë";
        }

        #region Global Settings
        public static string DefaultDesiredLanguage { get; set; }

        public static string DefaultGCloudAPIKey { get; set; }

        public static bool IsTranslateEnabled { get; set; }
        #endregion
    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using WzComparerR2.Config;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO;
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

        public static string GTranslateBaseURL = "https://translate.googleapis.com/translate_a/t";

        public static JArray Translate(string text, string desiredLanguage)
        {
            var request = (HttpWebRequest)WebRequest.Create(GTranslateBaseURL + "?client=gtx&format=text&sl=auto&tl=" + desiredLanguage);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            var postData = "q=" + Uri.EscapeDataString(text);
            var byteArray = System.Text.Encoding.UTF8.GetBytes(postData);
            request.ContentLength = byteArray.Length;
            Stream newStream = request.GetRequestStream();
            newStream.Write(byteArray, 0, byteArray.Length);
            newStream.Close();
            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                return JArray.Parse(responseString);
            }
            catch
            {
                return JArray.Parse($"[[\"{text}\",\"{desiredLanguage}\"]]");
            }
            
        }

        public static string TranslateString(string orgText)
        {
            JArray response = Translate(orgText, Translator.DefaultDesiredLanguage);
            return response[0][0].ToString();
        }

        public static bool IsDesiredLanguage(string orgText)
        {
            JArray response = Translate(orgText, Translator.DefaultDesiredLanguage);
            return (response[0][1].ToString() == DefaultDesiredLanguage);
        }

        #region Global Settings
        public static string DefaultDesiredLanguage { get; set; }

        public static string DefaultGCloudAPIKey { get; set; }

        public static bool IsTranslateEnabled { get; set; }
        #endregion
    }

}
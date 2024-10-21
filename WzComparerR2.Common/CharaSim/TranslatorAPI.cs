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
    public class TranslatorAPI
    {
        public TranslatorAPI()
        {
            this.DesiredLanguage = TranslatorAPI.DefaultDesiredLanguage;
            this.GCloudAPIKey = TranslatorAPI.DefaultGCloudAPIKey;
        }

        public string DesiredLanguage { get; set; }
        public string GCloudAPIKey { get; set; }

        public static string GCloudAPIBaseURL = "https://translation.googleapis.com/language/translate/v2";

        public static JObject PostJson(string url, Dictionary<string, object> param)
        {
            string paramStr = JsonConvert.SerializeObject(param);
            byte[] data = System.Text.Encoding.UTF8.GetBytes(paramStr);
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";

            Stream newStream = request.GetRequestStream();
            newStream.Write(data, 0, data.Length);
            newStream.Close();

            request.ServicePoint.Expect100Continue = false;
            request.ProtocolVersion = HttpVersion.Version11;

            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JObject.Parse(responseString);
            }

            return null;
        }

        public static bool IsDesiredLanguage(string orgText)
        {
            if (orgText == "(null)" || string.IsNullOrEmpty(orgText)) return true;
            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
            keyValuePairs.Add("q", orgText);
            try
            {
                JObject checkResult = PostJson(GCloudAPIBaseURL + "/detect?key=" + DefaultGCloudAPIKey, keyValuePairs);
                return (DefaultDesiredLanguage == checkResult.SelectToken("data.['detections'][0]['language']").ToString());
            }
            catch
            {
                return true;
            }
        }

        public static string TranslateString(string orgText)
        {
            if (orgText == "(null)" || string.IsNullOrEmpty(orgText)) return orgText;
            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
            keyValuePairs.Add("q", orgText);
            keyValuePairs.Add("target", DefaultDesiredLanguage);
            keyValuePairs.Add("format", "text");
            try
            {
                JObject checkResult = PostJson(GCloudAPIBaseURL + "?key=" + DefaultGCloudAPIKey, keyValuePairs);
                return checkResult.SelectToken("data.['translations'][0]['translatedText']").ToString();
            }
            catch
            {
                return orgText;
            }
        }

        #region Global Settings
        public static string DefaultDesiredLanguage { get; set; }

        public static string DefaultGCloudAPIKey { get; set; }

        public static bool IsTranslateEnabled { get; set; }
        #endregion
    }

}
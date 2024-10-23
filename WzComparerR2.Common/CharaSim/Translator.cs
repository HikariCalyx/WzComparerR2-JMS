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
using static System.Net.Mime.MediaTypeNames;
using DevComponents.DotNetBar;
namespace WzComparerR2.CharaSim
{
    public class Translator
    {
        public static string GTranslateBaseURL = "https://translate.googleapis.com/translate_a/t";
        public static JArray GTranslate(string text, string desiredLanguage)
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

        public static JObject MTranslate(string text, string engine, string sourceLanguage, string desiredLanguage)
        {
            var request = (HttpWebRequest)WebRequest.Create(DefaultMozhiBackend + "/api/translate?engine=" + engine + "&from=" + sourceLanguage + "&to=" + desiredLanguage + "&text=" + Uri.EscapeDataString(text));
            request.Accept = "application/json";
            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                return JObject.Parse(responseString);
            }
            catch
            {
                return JObject.Parse("{\"translated-text\": \"" + text + "\"}");
            }
        }

        public static string TranslateString(string orgText)
        {
            if (string.IsNullOrEmpty(orgText)) return orgText;
            bool isMozhiUsed = false;
            string mozhiEngine = "";
            string translatedText = "";
            string sourceLanguage = "auto";
            string targetLanguage = DefaultDesiredLanguage;
            switch (DefaultPreferredTranslateEngine)
            {
                //0: Google (Mozhi)
                case 0:
                    isMozhiUsed = true;
                    mozhiEngine = "google";
                    break;
                //1: DeepL (Mozhi)
                case 1:
                    isMozhiUsed = true;
                    sourceLanguage = "en";
                    if (targetLanguage.Contains("zh")) targetLanguage = "zh";
                    if (targetLanguage == "yue") targetLanguage = "zh";
                    mozhiEngine = "deepl";
                    break;
                //2: DuckDuckGo / Bing (Mozhi)
                case 2:
                    isMozhiUsed = true;
                    if (targetLanguage == "zh-CN") targetLanguage = "zh";
                    mozhiEngine = "duckduckgo";
                    break;
                //3: MyMemory (Mozhi)
                case 3:
                    isMozhiUsed = true;
                    sourceLanguage = "Autodetect";
                    if (targetLanguage.Contains("zh") || targetLanguage == "yue") targetLanguage = "zh";
                    mozhiEngine = "mymemory";
                    break;
                //4: Yandex (Mozhi)
                case 4:
                    isMozhiUsed = true;
                    if (targetLanguage.Contains("zh")) targetLanguage = "zh";
                    if (targetLanguage == "yue") targetLanguage = "zh";
                    mozhiEngine = "yandex";
                    break;
                //5: Naver Papago (Non-Mozhi)
                case 5:
                    isMozhiUsed = true;
                    if (targetLanguage == "yue") targetLanguage = "zh-TW";
                    mozhiEngine = "naver";
                    break;
                //6: Google (Non-Mozhi)
                case 6:
                default:
                    JArray response = GTranslate(orgText.Replace("\\n", "\r\n"), Translator.DefaultDesiredLanguage);
                    translatedText = response[0][0].ToString().Replace("\r\n", "\\n").Replace("££", "#");
                    break;
            }
            if (isMozhiUsed)
            {
                translatedText = MTranslate(orgText.Replace("\\n", "\r\n"), mozhiEngine, sourceLanguage, targetLanguage).SelectToken("translated-text").ToString().Replace("\r\n", "\\n").Replace("££", "#");
            }
            return translatedText;
        }

        public static bool IsDesiredLanguage(string orgText)
        {
            if (string.IsNullOrEmpty(orgText)) return true;
            JArray response = GTranslate(orgText, Translator.DefaultDesiredLanguage);
            return (response[0][1].ToString() == DefaultDesiredLanguage);
        }

        #region Global Settings
        public static string DefaultDesiredLanguage { get; set; }
        public static string DefaultMozhiBackend { get; set; }
        public static int DefaultPreferredTranslateEngine { get; set; }
        public static bool IsTranslateEnabled { get; set; }
        #endregion
    }

}
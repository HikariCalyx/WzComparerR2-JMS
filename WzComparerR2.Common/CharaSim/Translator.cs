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
using System.Globalization;
using System.Threading;
namespace WzComparerR2.CharaSim
{
    public class Translator
    {
        private static string GTranslateBaseURL = "https://translate.googleapis.com/translate_a/t";
        private static string NTranslateBaseURL = "https://naveropenapi.apigw.ntruss.com";

        private static JArray GTranslate(string text, string desiredLanguage)
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

        public static bool IsKoreanStringPresent(string checkString)
        {
            if (checkString == null) return false;
            return checkString.Any(c => (c >= '\uAC00' && c <= '\uD7A3'));
        }

        private static string GetKeyValue(string jsonDictKey)
        {
            try
            {
                return JObject.Parse(DefaultTranslateAPIKey).SelectToken(jsonDictKey).ToString();
            }
            catch
            {
                return "";
            }
            
        }
        private static JObject MTranslate(string text, string engine, string sourceLanguage, string desiredLanguage)
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

        private static JObject NTranslate(string text, string desiredLanguage)
        {
            var request = (HttpWebRequest)WebRequest.Create(NTranslateBaseURL + "/nmt/v1");
            request.Method = "POST";
            request.Accept = "application/json";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Headers["X-NCP-APIGW-API-KEY-ID"] = GetKeyValue("X-NCP-APIGW-API-KEY-ID");
            request.Headers["X-NCP-APIGW-API-KEY"] = GetKeyValue("X-NCP-APIGW-API-KEY-ID");
            var postData = "source=auto&target=" + desiredLanguage + "text=" + Uri.EscapeDataString(text);
            var byteArray = System.Text.Encoding.UTF8.GetBytes(postData);
            request.ContentLength = byteArray.Length;
            Stream newStream = request.GetRequestStream();
            newStream.Write(byteArray, 0, byteArray.Length);
            newStream.Close();
            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                return JObject.Parse(responseString);
            }
            catch
            {
                return JObject.Parse("{\"message\": {\"result\": {\"translatedText\": \"Ÿo„¿¤ÊNaver API¥­©`\"}}}");
            }
        }

        public static string MergeString(string text1, string text2, int newLineCounts=0, bool oneLineSeparatorRequired=false, bool bracketRequiredForText2=false)
        {
            if (text1 == text2)
            {
                return text1;
            }
            else if (!string.IsNullOrEmpty(text1) && !string.IsNullOrEmpty(text2))
            {
                string resultStr;
                switch (DefaultPreferredLayout)
                {
                    case 1:
                        resultStr = text2;
                        if (newLineCounts == 0 && oneLineSeparatorRequired) resultStr += " / ";
                        if (newLineCounts == 0 && bracketRequiredForText2) resultStr += " ";
                        while (newLineCounts > 0)
                        {
                            resultStr += Environment.NewLine;
                            newLineCounts--;
                        }
                        if (bracketRequiredForText2) resultStr += "(";
                        resultStr += text1;
                        if (bracketRequiredForText2) resultStr += ")";
                        break;
                    case 2:
                        resultStr = text1;
                        if (newLineCounts == 0 && oneLineSeparatorRequired) resultStr += " / ";
                        if (newLineCounts == 0 && bracketRequiredForText2) resultStr += " ";
                        while (newLineCounts > 0)
                        {
                            resultStr += Environment.NewLine;
                            newLineCounts--;
                        }
                        if (bracketRequiredForText2) resultStr += "(";
                        resultStr += text2;
                        if (bracketRequiredForText2) resultStr += ")";
                        break;
                    case 3:
                        resultStr = text2;
                        break;
                    default:
                        resultStr = text1;
                        break;
                }
                return resultStr;
            }
            else
            {
                return text1;
            }
        }

        public static string TranslateString(string orgText, bool titleCase=false)
        {
            if (string.IsNullOrEmpty(orgText) || orgText == "(null)") return orgText;
            bool isMozhiUsed = false;
            string mozhiEngine = "";
            string translatedText = "";
            string sourceLanguage = "auto";
            string targetLanguage = DefaultDesiredLanguage;
            switch (DefaultPreferredTranslateEngine)
            {
                //0: Google (Non-Mozhi)
                default:
                case 0:
                    JArray responseArr = GTranslate(orgText.Replace("\\n", "\r\n"), Translator.DefaultDesiredLanguage);
                    translatedText = responseArr[0][0].ToString().Replace("\r\n", "\\n").Replace("££", "#");
                    break;
                //1: Google (Mozhi)
                case 1:
                    isMozhiUsed = true;
                    mozhiEngine = "google";
                    break;
                //2: DeepL (Mozhi)
                case 2:
                    isMozhiUsed = true;
                    sourceLanguage = "en";
                    if (targetLanguage.Contains("zh") || targetLanguage == "yue") targetLanguage = "zh";
                    mozhiEngine = "deepl";
                    break;
                //3: DuckDuckGo / Bing (Mozhi)
                case 3:
                    isMozhiUsed = true;
                    if (targetLanguage == "zh-CN") targetLanguage = "zh";
                    mozhiEngine = "duckduckgo";
                    break;
                //4: MyMemory (Mozhi)
                case 4:
                    isMozhiUsed = true;
                    sourceLanguage = "Autodetect";
                    if (targetLanguage.Contains("zh") || targetLanguage == "yue") targetLanguage = "zh";
                    mozhiEngine = "mymemory";
                    break;
                //5: Yandex (Mozhi)
                case 5:
                    isMozhiUsed = true;
                    if (targetLanguage.Contains("zh") || targetLanguage == "yue") targetLanguage = "zh";
                    mozhiEngine = "yandex";
                    break;
                //6: Naver Papago (Non-Mozhi)
                case 6:
                    if (targetLanguage == "yue") targetLanguage = "zh-TW";
                    JObject responseObj = NTranslate(orgText.Replace("\\n", "\r\n"), Translator.DefaultDesiredLanguage);
                    translatedText = responseObj.SelectToken("message.result.translatedText").ToString();
                    break;
                //7: iFlyTek (Non-Mozhi)
            }
            if (isMozhiUsed)
            {
                translatedText = MTranslate(orgText.Replace("\\n", "\r\n"), mozhiEngine, sourceLanguage, targetLanguage).SelectToken("translated-text").ToString().Replace("\r\n", "\\n").Replace("££", "#");
            }
            if (titleCase && targetLanguage == "en")
            {
                CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
                TextInfo textInfo = cultureInfo.TextInfo;
                translatedText = textInfo.ToTitleCase(translatedText);
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
        public static string DefaultTranslateAPIKey { get; set; }
        public static int DefaultPreferredLayout { get; set; }
        public static int DefaultPreferredTranslateEngine { get; set; }
        public static bool IsTranslateEnabled { get; set; }
        #endregion
    }

}
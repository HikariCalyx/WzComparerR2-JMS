using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using WzComparerR2.Config;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO;
using System.Globalization;
using System.Threading;
using static System.Net.Mime.MediaTypeNames;
namespace WzComparerR2.CharaSim
{
    public class Translator
    {
        // L2C stands for Language to Currency
        private static Dictionary<string, string> dictL2C = new Dictionary<string, string>()
        {
            { "ja", "jpy" },
            { "ko", "krw" },
            { "zh-CN", "cny" },
            { "en", "usd" },
            { "zh-TW", "twd" }
        };

        private static Dictionary<string, string> dictC2L = new Dictionary<string, string>()
        {
            { "jpy", "ja" },
            { "krw", "ko" },
            { "cny", "zh-CN" },
            { "usd", "en" },
            { "twd", "zh-TW" },
            { "sgd", "en" }
        };

        // Language Model Expression
        private static Dictionary<string, string> dictL2LM = new Dictionary<string, string>()
        {
            { "ja", "Japanese" },
            { "ko", "Korean" },
            { "zh-CN", "Simplified Chinese" },
            { "en", "English" },
            { "zh-TW", "Traditional Chinese" },
            { "yue", "Cantonese" }
        };

        private static Dictionary<string, string> dictCurrencyName = new Dictionary<string, string>()
        {
            { "jpy", "円" },
            { "krw", "ウォン" },
            { "cny", "元" },
            { "usd", "ドル" },
            { "twd", "台湾ドル" },
            { "hkd", "香港ドル" },
            { "mop", "マカオパタカ" },
            { "sgd", "シンガポールドル" },
            { "eur", "ユーロ" },
            { "cad", "カナダドル" },
            { "aud", "オーストラリアドル" },
            { "myr", "マレーシアリンギット" },
        };

        private static string GTranslateBaseURL = "https://translate.googleapis.com/translate_a/t";
        private static string NTranslateBaseURL = "https://naveropenapi.apigw.ntruss.com";
        public static string OAITranslateBaseURL { get; set; }

        private static List<string> CurrencyBaseURL = new List<string>()
        {
            "https://cdn.jsdelivr.net/npm/@fawazahmed0/currency-api@latest/v1/currencies/",
            "https://latest.currency-api.pages.dev/v1/currencies/",
            "https://registry.npmmirror.com/@fawazahmed0/currency-api/latest/files/v1/currencies/"
        };

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

        private static string OAITranslate(string text, string desiredLanguage, bool singleLine = false)
        {
            if (string.IsNullOrEmpty(DefaultOpenAISystemMessage)) DefaultOpenAISystemMessage = "You are an automated translator for a community game engine, and I only need translated result in output.";
            if (string.IsNullOrEmpty(OAITranslateBaseURL)) OAITranslateBaseURL = "https://api.openai.com/v1";
            var request = (HttpWebRequest)WebRequest.Create(OAITranslateBaseURL + "/chat/completions");
            request.Method = "POST";
            request.ContentType = "application/json";
            if (!string.IsNullOrEmpty(DefaultTranslateAPIKey))
            {
                JObject reqHeaders = JObject.Parse(DefaultTranslateAPIKey);
                foreach (var property in reqHeaders.Properties()) request.Headers.Add(property.Name, property.Value.ToString());
            }
            var postData = new JObject(
                new JProperty("model", DefaultLanguageModel),
                new JProperty("messages", new JArray(
                    new JObject(
                        new JProperty("role", "system"),
                        new JProperty("content", DefaultOpenAISystemMessage)
                    ),
                    new JObject(
                        new JProperty("role", "user"),
                        new JProperty("content", "Please translate following in-game content into " + dictL2LM[desiredLanguage] + ": " + text)
                    )
                )),
                new JProperty("stream", false)
            );
            if (IsExtraParamEnabled)
            {
                postData.Add(new JProperty("temperature", DefaultLMTemperature));
                postData.Add(new JProperty("max_tokens", DefaultMaximumToken));
            }
            var byteArray = System.Text.Encoding.UTF8.GetBytes(postData.ToString());
            request.ContentLength = byteArray.Length;
            Stream newStream = request.GetRequestStream();
            newStream.Write(byteArray, 0, byteArray.Length);
            newStream.Close();
            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                JObject jrResponse = JObject.Parse(responseString);
                string responseResult = jrResponse.SelectToken("choices[0].message.content").ToString();
                if (responseResult.Contains("</think>"))
                {
                    responseResult = responseResult.Split(new String[] { "</think>\n\n" }, StringSplitOptions.None)[1];
                }
                if (responseResult.Contains("**\""))
                {
                    responseResult = responseResult.Split(new String[] { "**" }, StringSplitOptions.None)[1];
                }
                if (responseResult.Contains(text))
                {
                    responseResult = responseResult.Split(new String[] { "**" }, StringSplitOptions.None)[1];
                }
                if (responseResult.Contains("：\n\n") || responseResult.Contains(":\n\n") || responseResult.Contains(": \n\n"))
                {
                    // TO DO: Put extra output to NetworkLogger.
                    responseResult = responseResult.Split(new String[] { "\n\n" }, StringSplitOptions.None)[1];
                    if (responseResult.Contains("\r")) responseResult = responseResult.Split(new String[] { "\r" }, StringSplitOptions.None)[0];
                    if (responseResult.Contains("\n")) responseResult = responseResult.Split(new String[] { "\n" }, StringSplitOptions.None)[0];
                }
                if (singleLine)
                {
                    return responseResult.Replace("\r\n", " ").Replace("\n", "").Replace("  ", " ").Replace("\"", "");
                }
                else
                {
                    return responseResult;
                }
                
            }
            catch (Exception e)
            {
                return text;
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
                return JObject.Parse("{\"message\": {\"result\": {\"translatedText\": \"無効なNaver APIキー\"}}}");
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
            string translatedText = TryFetchCachedTranslationResult(orgText);
            string sourceLanguage = "auto";
            string targetLanguage = DefaultDesiredLanguage;
            if (!String.IsNullOrEmpty(translatedText))
            {
                if (titleCase && targetLanguage == "en")
                {
                    CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
                    TextInfo textInfo = cultureInfo.TextInfo;
                    translatedText = textInfo.ToTitleCase(translatedText);
                }
                return translatedText;
            }
            switch (DefaultPreferredTranslateEngine)
            {
                //0: Google (Non-Mozhi)
                default:
                case 0:
                    JArray responseArr = GTranslate(ConvHashTagToHTMLTag(orgText), Translator.DefaultDesiredLanguage);
                    translatedText = responseArr[0][0].ToString().Replace("＃", "#");
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
                    JObject responseObj = NTranslate(ConvHashTagToHTMLTag(orgText), Translator.DefaultDesiredLanguage);
                    translatedText = responseObj.SelectToken("message.result.translatedText").ToString();
                    break;
                //9: OpenAI Compatible
                case 9:
                    if (titleCase && targetLanguage == "en")
                    {
                        translatedText = OAITranslate(ConvHashTagToHTMLTag(orgText), Translator.DefaultDesiredLanguage, true);
                    }
                    else
                    {
                        translatedText = OAITranslate(ConvHashTagToHTMLTag(orgText), Translator.DefaultDesiredLanguage);
                    }
                    break;
            }
            if (isMozhiUsed)
            {
                translatedText = MTranslate(ConvHashTagToHTMLTag(orgText), mozhiEngine, sourceLanguage, targetLanguage).SelectToken("translated-text").ToString().Replace("＃", "#");
            }
            if (titleCase && targetLanguage == "en")
            {
                CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
                TextInfo textInfo = cultureInfo.TextInfo;
                translatedText = textInfo.ToTitleCase(translatedText);
            }
            translatedText = ConvHTMLTagToHashTag(translatedText);
            CacheTranslationResult(orgText, translatedText);
            return translatedText;
        }

        public static string ConvHashTagToHTMLTag(string orgText)
        {
            if (!string.IsNullOrEmpty(orgText))
            {
                return orgText.Replace("#c", "<CHL>").Replace("#", "</CHL>").Replace("\\r\\n", "<BR/>").Replace("\\n", "<BR/>");
            }
            else
            {
                return orgText;
            }
        }

        public static string ConvHTMLTagToHashTag(string orgText)
        {
            if (!string.IsNullOrEmpty(orgText))
            {
                return Regex.Replace(
                    Regex.Replace(
                        Regex.Replace(
                            Regex.Replace(
                                orgText.Replace("< ", "<").Replace(" >", ">"), 
                                "<CHL>", "#c", RegexOptions.IgnoreCase),
                            "</CHL>", "#", RegexOptions.IgnoreCase),
                        "<BR/>", "\r\n", RegexOptions.IgnoreCase),
                    "CHL>", "#c", RegexOptions.IgnoreCase);
            }
            else
            {
                return orgText;
            }
        }

        public static string GetLanguage(string orgText)
        {
            if (string.IsNullOrEmpty(orgText) || orgText == "(null)") return "ja";
            bool isMozhiUsed = false;
            string mozhiEngine = "";
            string orgLanguage = "";
            string sourceLanguage = "auto";
            string targetLanguage = DefaultDesiredLanguage;
            switch (DefaultPreferredTranslateEngine)
            {
                //0: Google (Non-Mozhi)
                default:
                case 0:
                    JArray responseArr = GTranslate(orgText, Translator.DefaultDesiredLanguage);
                    orgLanguage = responseArr[0][1].ToString();
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
                    JObject responseObj = NTranslate(orgText, Translator.DefaultDesiredLanguage);
                    orgLanguage = responseObj.SelectToken("message.result.srcLangType").ToString();
                    break;
            }
            if (isMozhiUsed)
            {
                orgLanguage = MTranslate(orgText, mozhiEngine, sourceLanguage, targetLanguage).SelectToken("detected").ToString();
            }
            return orgLanguage;
        }

        public static string GetConvertedCurrency(int pointValue, string sourceLanguage)
        {
            if (DefaultDesiredCurrency == "none")
            {
                return null;
            }
            UpdateExchangeTable();
            if (String.IsNullOrEmpty(ExchangeTable))
            {
                return null;
            }
            double irlPrice;
            string sourceCurrency;
            switch (sourceLanguage)
            {
                case "zh-CN":
                    irlPrice = pointValue / 100.00; break; // CMSでは100ポイントあたり1元
                case "en":
                    irlPrice = pointValue / 1000.00; break; // GMSでは1000ポイントあたり1ドル
                default:
                    irlPrice = pointValue; break;
            }
            JObject exTable = JObject.Parse(ExchangeTable);
            if (DefaultDetectCurrency == "auto")
            {
                sourceCurrency = dictL2C[sourceLanguage];
            }
            else
            {
                sourceCurrency = DefaultDetectCurrency;
            }
            double exchangeMultipler = 1;
            double.TryParse(exTable.SelectToken(DefaultDesiredCurrency + "." + sourceCurrency).ToString(), out exchangeMultipler);
            double convertedPrice = irlPrice / exchangeMultipler;
            switch (DefaultDesiredCurrency)
            {
                case "jpy":
                case "krw":
                    return "約" + Math.Round(convertedPrice).ToString() + dictCurrencyName[DefaultDesiredCurrency];
                default:
                    return "約" + convertedPrice.ToString("0.##") + dictCurrencyName[DefaultDesiredCurrency];
            }
        }

        public static string ConvertCurrencyToLang(string currency)
        {
            try
            {
                return dictC2L[currency];
            }
            catch
            {
                return null;
            }
        }

        public static bool IsDesiredLanguage(string orgText)
        {
            if (string.IsNullOrEmpty(orgText)) return true;
            JArray response = GTranslate(orgText, Translator.DefaultDesiredLanguage);
            return (response[0][1].ToString() == DefaultDesiredLanguage);
        }

        public static void UpdateExchangeTable()
        {
            if (String.IsNullOrEmpty(ExchangeTable))
            {
                foreach (string bURL in CurrencyBaseURL)
                {
                    string fetchURL = bURL + DefaultDesiredCurrency + ".min.json";
                    var request = (HttpWebRequest)WebRequest.Create(fetchURL);
                    request.Accept = "application/json";
                    try
                    {
                        var response = (HttpWebResponse)request.GetResponse();
                        var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                        ExchangeTable = responseString;
                        break;
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
        }

        public static void InitializeCache()
        {
            string[] pathList = new string[]
            {
                "google",
                "mozhi-google",
                "mozhi-deepl",
                "mozhi-duckduckgobing",
                "mozhi-mymemory",
                "mozhi-yandex",
                "naver",
                "openai"
            };
            foreach (string targetPath in pathList)
            {
                string createPath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "TranslationCache", targetPath);
                if (!File.Exists(createPath))
                {
                    System.IO.Directory.CreateDirectory(createPath);
                }
            }
        }

        private static string TryFetchCachedTranslationResult(string orgText)
        {
            string cachePath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "TranslationCache");
            switch (DefaultPreferredTranslateEngine)
            {
                default:
                case 0:
                    cachePath = Path.Combine(cachePath, "google", DefaultDesiredLanguage + ".json");
                    break;
                case 1:
                    cachePath = Path.Combine(cachePath, "mozhi-google", DefaultDesiredLanguage + ".json");
                    break;
                case 2:
                    cachePath = Path.Combine(cachePath, "mozhi-deepl", DefaultDesiredLanguage + ".json");
                    break;
                case 3:
                    cachePath = Path.Combine(cachePath, "mozhi-duckduckgobing", DefaultDesiredLanguage + ".json");
                    break;
                case 4:
                    cachePath = Path.Combine(cachePath, "mozhi-mymemory", DefaultDesiredLanguage + ".json");
                    break;
                case 5:
                    cachePath = Path.Combine(cachePath, "mozhi-yandex", DefaultDesiredLanguage + ".json");
                    break;
                case 6:
                    cachePath = Path.Combine(cachePath, "naver", DefaultDesiredLanguage + ".json");
                    break;
                case 9:
                    cachePath = Path.Combine(cachePath, "openai", DefaultLanguageModel + "_" + DefaultDesiredLanguage + ".json");
                    break;
            }
            if (File.Exists(cachePath))
            {
                try
                {
                    JObject currentTranslationCache = JObject.Parse(File.ReadAllText(cachePath));
                    string translatedResult = currentTranslationCache.SelectToken(String.Format("['{0}']", orgText)).ToString();
                    return translatedResult;
                }
                catch
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }

        private static void CacheTranslationResult(string orgText, string translatedText)
        {
            JObject currentTranslationCache = new JObject();
            string cachePath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "TranslationCache");
            switch (DefaultPreferredTranslateEngine)
            {
                default:
                case 0:
                    cachePath = Path.Combine(cachePath, "google", DefaultDesiredLanguage + ".json");
                    break;
                case 1:
                    cachePath = Path.Combine(cachePath, "mozhi-google", DefaultDesiredLanguage + ".json");
                    break;
                case 2:
                    cachePath = Path.Combine(cachePath, "mozhi-deepl", DefaultDesiredLanguage + ".json");
                    break;
                case 3:
                    cachePath = Path.Combine(cachePath, "mozhi-duckduckgobing", DefaultDesiredLanguage + ".json");
                    break;
                case 4:
                    cachePath = Path.Combine(cachePath, "mozhi-mymemory", DefaultDesiredLanguage + ".json");
                    break;
                case 5:
                    cachePath = Path.Combine(cachePath, "mozhi-yandex", DefaultDesiredLanguage + ".json");
                    break;
                case 6:
                    cachePath = Path.Combine(cachePath, "naver", DefaultDesiredLanguage + ".json");
                    break;
                case 9:
                    cachePath = Path.Combine(cachePath, "openai", DefaultLanguageModel + "_" + DefaultDesiredLanguage + ".json");
                    break;
            }
            if (File.Exists(cachePath)) currentTranslationCache = JObject.Parse(File.ReadAllText(cachePath));
            currentTranslationCache.Add(new JProperty(orgText, translatedText));
            File.WriteAllText(cachePath, currentTranslationCache.ToString());
        }

        #region Global Settings
        public static string ExchangeTable { get; set; }
        public static string DefaultDesiredLanguage { get; set; }
        public static string DefaultMozhiBackend { get; set; }
        public static string DefaultLanguageModel { get; set; }
        public static string DefaultTranslateAPIKey { get; set; }
        public static string DefaultOpenAISystemMessage { get; set; }
        public static int DefaultPreferredLayout { get; set; }
        public static int DefaultPreferredTranslateEngine { get; set; }
        public static bool IsTranslateEnabled { get; set; }
        public static string DefaultDetectCurrency { get; set; }
        public static string DefaultDesiredCurrency { get; set; }
        public static double DefaultLMTemperature { get; set; }
        public static int DefaultMaximumToken { get; set; }
        public static bool IsExtraParamEnabled { get; set; }
        #endregion
    }

}
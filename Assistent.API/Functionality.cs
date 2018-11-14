using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VAssistent.Assistent.API.Models;

namespace VAssistent.Assistent.API
{
    public static class Functionality
    {
        public static string Process(this string phrase)
        {
            /*Array.Resize(ref preWords, preWords.Length + 1);
            preWords[preWords.Length - 1] = "new string";*/
            string response = null;
            string[] preWords = phrase.Split(" ".ToCharArray());

            if (preWords.Length > 1)
                if (preWords[0].IsQuestion())
                    if (preWords[1].IsVerb()) response = QuestionKind(phrase);
                    else response = "The phrase starts with a question but it doesn't make any sense.";
                else return PhraseKind(phrase);
            return response;
        }

        public static string QuestionKind(this string phrase)
        {
            if (phrase.Contains("weather") && phrase.Contains("today")) { return "Today is windy"; }
            if (phrase.Contains("weather") && phrase.Contains("tomorrow")) { return "Tomorrow will be sunny"; }
            if (phrase.Contains("time")) { return "Current time is " + DateTime.Now.ToString("HH:mm:ss"); }
            return null;
        }

        public static string PhraseKind(this string phrase)
        {
            string lowerCasePhrase = phrase.ToLower();
            if (lowerCasePhrase.Contains("open chrome")) { /*System.Diagnostics.Process.Start(@"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe");*/ return "Application Started"; }
            return null;
        }

        public static string GetWeather(string day)
        {

            return "";
        }

        public static void Calculator(string sentence)
        {
            string rr = null;
            Regex regex = new Regex(@"([0-9]|[\-+#])+");
            List<string> matchedStrings = new List<string>();
            foreach (Match match in regex.Matches(@sentence))
            {
                rr += match.Value;
            }
            double result = Convert.ToDouble(new DataTable().Compute(rr, null));
            Console.WriteLine(result.ToString());
        }

        public static bool IsQuestion(this string word)
        {
            string[] questionWords = { "What", "How", "Why", "When", "Where", "Who" };
            foreach (string question in questionWords)
            {
                if (word.ToLower() == question.ToLower()) return true;
            }
            return false;
        }

        public static bool IsVerb(this string word)
        {
            string[] verbWords = { "is", "are", "will", "were", "shall", "should", "am" };
            foreach (string verb in verbWords)
            {
                if (word == verb) return true;
            }
            return false;
        }

        public static string SpeechToText(string apiKey, string language)
        {
            HttpWebRequest request = null;
            request = (HttpWebRequest)HttpWebRequest.Create($"https://westus.stt.speech.microsoft.com/speech/recognition/conversation/cognitiveservices/v1?language={language}");
            request.SendChunked = true;
            request.Accept = @"application/json;text/xml";
            request.Method = "POST";
            request.ProtocolVersion = HttpVersion.Version11;
            request.ContentType = @"audio/wav; codec=audio/pcm; samplerate=16000";
            request.Headers["Ocp-Apim-Subscription-Key"] = apiKey;
            string responseString = null;

            using (FileStream fs = new FileStream("AudioFiles\\done2.wav", FileMode.Open, FileAccess.Read))
            {
                byte[] buffer = null;
                int bytesRead = 0;
                using (Stream requestStream = request.GetRequestStream())
                {
                    buffer = new Byte[checked((uint)Math.Min(1024, (int)fs.Length))];

                    while ((bytesRead = fs.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        requestStream.Write(buffer, 0, bytesRead);
                    }

                    requestStream.Flush();
                }
            }

            using (WebResponse response = request.GetResponse())
            {
                //Console.WriteLine(((HttpWebResponse)response).StatusCode);
                using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                {
                    responseString = sr.ReadToEnd();
                }
                return responseString;
            }
        }

        public static string BingSearch(string question, string ApiKey, string language)
        {
            HttpWebRequest request = null;
            request = (HttpWebRequest)HttpWebRequest.Create($"https://api.cognitive.microsoft.com/bing/v7.0/search?q={question}&count=5&offset=0&mkt={language}&safesearch=Moderate");
            request.SendChunked = true;
            request.Accept = @"application/json;text/xml";
            request.Method = "GET";
            request.ProtocolVersion = HttpVersion.Version11;
            request.ContentType = @"application/json";
            request.Headers["Ocp-Apim-Subscription-Key"] = ApiKey;
            string responseString = null;


            using (WebResponse response = request.GetResponse())
            {
                //Console.WriteLine(((HttpWebResponse)response).StatusCode);
                using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                {
                    responseString = sr.ReadToEnd();
                }
                return responseString;
            }
        }
    }
}

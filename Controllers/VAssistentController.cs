using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using VAssistent.Assistent.API;
using VAssistent.Assistent.API.Models;
using VAssistent.Models;

namespace VAssistent.Controllers
{
    public class VAssistentController : Controller
    {
        private readonly string bingSearchKey;
        private readonly string speechToTextKey;
        private readonly string language;

        public VAssistentController(IConfiguration iconfiguration)
        {
            speechToTextKey = iconfiguration.GetValue<string>("ApiKeys:SpeechToText");
            bingSearchKey = iconfiguration.GetValue<string>("ApiKeys:BingSearch");
            language = iconfiguration.GetValue<string>("ApiKeys:Language");
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string RecogniseSpeech(string text)
        {
            string response = null;

            if (text == "speechToText")
            {
                response = Functionality.SpeechToText(speechToTextKey, language);
                if(response != null)
                {
                    return response;
                }
            }

            response = text.Process();
            if (response == null)
            {
                response = Functionality.BingSearch(text, bingSearchKey, language);
                if(response !=null)
                {
                    var res = JsonConvert.DeserializeObject<Demo>(response);
                    WebResponse web = new WebResponse();
                    web.Type = "BingSearch";
                    web.Value = res.webPages.value;
                    var webSeatch = JsonConvert.SerializeObject(web);
                    return webSeatch;
                }
            }
            else
            {
                LocalResponse local = new LocalResponse();
                local.Type = "Local";
                local.Message = response;
                return JsonConvert.SerializeObject(local);
            }
            return response;
        }
    }
}
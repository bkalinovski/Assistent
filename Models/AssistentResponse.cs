using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VAssistent.Assistent.API.Models;

namespace VAssistent.Models
{
    public class AssistentResponse
    {
        public string Type { get; set; }
    }

    public class LocalResponse : AssistentResponse
    {
        public string Message { get; set; }
    }

    public class WebResponse : AssistentResponse
    {
        public List<Value> Value { get; set; }
    }
}

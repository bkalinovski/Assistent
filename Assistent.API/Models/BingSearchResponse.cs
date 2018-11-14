using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VAssistent.Assistent.API.Models
{
    public class BingSearchResponse
    {
        public Demo Demo { get; set; }
    }

    public class Demo
    {
        public WebPages webPages { get; set; }
    }

    public class WebPages
    {
        public string webSearchUrl { get; set; }
        public int totalEstimatedMatches { get; set; }
        public List<Value> value { get; set; }
    }

    public class Value
    {
        public string name { get; set; }
        public string url { get; set; }
        public string displayUrl { get; set; }
        public string snippet { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace BBCparser
{
    class JsonGenerator
    {
        private readonly List<Content> _items;

        public JsonGenerator(List<Content> items)
        {
            _items = items;
        }
        public string PrepareJson()
        {
            
            JObject json = new JObject(
                new JProperty("title","BBC News - Home"),
                new JProperty("link",@"http://www.bbc.co.uk/news/#sa-ns_mchannel=NewsFeed&amp;ns_source=PublicRSS20-sa"),
                new JProperty("description","The latest stories from the Home section of the BBC News web site."),
                new JProperty("items",
                    new JArray(
                        from i in _items
                        select new JObject(
                            new JProperty("title", i.Title),
                            new JProperty("description", i.Description),
                            new JProperty("link", i.Link),
                            new JProperty("pubDate", i.PubDate)
                            ))));
           // Console.WriteLine(json.ToString());
            return json.ToString();
        }

        public void SaveJson()
        {
            var json = PrepareJson();
            var fileName = @"\"+CurrentDate() +" " + DateTime.Now.Hour + ".json";
            string path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName,"feed",CurrentDate());
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            
            File.WriteAllText(path+fileName, json);
            
        }

        public static string CurrentDate()
        {
            var date = DateTime.Now.Date.ToString("yyyy-MM-dd");
            return date;

        }
    }
}

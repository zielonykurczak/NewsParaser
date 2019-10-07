using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace BBCparser
{
    class NewsCache
    {
        private readonly Dictionary<string, DateTime> _newsCache;

        public NewsCache()
        {
            _newsCache = new Dictionary<string, DateTime>();
            LoadPreviousJson();
        }

        public void UpdateCache(string title, DateTime date)
        {
            _newsCache.Add(title, date);
        }

        public bool IsAlreadySaved(string title, DateTime date)
        {
            if (_newsCache.TryGetValue(title, out var dateFromCache))
                return dateFromCache.Equals(date);
            return false;
        }

        public void LoadPreviousJson()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "feed", JsonGenerator.CurrentDateTime());
            if (Directory.Exists(path) && Directory.EnumerateFiles(path).Any())
                foreach (var file in Directory.EnumerateFiles(path))
                {
                    var json = File.ReadAllText(file);
                    var jsonObject = JObject.Parse(json);
                    foreach (var item in jsonObject["items"])
                    {
                        var date = item["pubDate"];
                        var formattedDate = DateFormatter.ChangePubdateFormat(date.ToString());
                        if (!_newsCache.ContainsKey(item["title"].ToString()))

                            try
                            {
                                UpdateCache(item["title"].ToString(), formattedDate.Date);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                                throw;
                            }
                    }
                }
        }
    }
}
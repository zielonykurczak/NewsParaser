using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Newtonsoft.Json.Linq;

namespace BBCparser
{
    public class NewsCache
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
            {
                return dateFromCache.Equals(date);
                
            }
            return false;
        }

        public void LoadPreviousJson()
        {
            foreach (string file in Directory.EnumerateFiles(Path.Combine(Directory.GetCurrentDirectory(),"feed",JsonGenerator.CurrentDateTime())))
            {
                var json = File.ReadAllText(file);
                JObject jsonObject = JObject.Parse(json);
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

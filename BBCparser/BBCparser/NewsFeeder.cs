using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BBCparser
{
    class NewsFeeder
    {
        private static List<Content> _items = new List<Content>();
        private static readonly HttpClient Client = new HttpClient();
        private readonly string _url = @"http://feeds.bbci.co.uk/news/uk/rss.xml";
        private string _content;

        public async Task<string> Feeder()
        {
            try
            {
                _content = await Client.GetStringAsync(_url);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return _content;
        }

        public List<Content> ListOfNewsGenerator()
        {
            Rss channel;
            NewsCache cache = new NewsCache();
            var serializer = new XmlSerializer(typeof(Rss));
            using (var reader = new StringReader(Feeder().Result))
            {
                channel = (Rss) serializer.Deserialize(reader);
                foreach (var item in channel.Channel.Items)
                {
                    var formattedDate = DateFormatter.ChangePubdateFormat(item.PubDate);
                    if (!cache.IsAlreadySaved(item.Title, formattedDate.Date))
                    {
                        _items.Add(item);
                        cache.UpdateCache(item.Title, formattedDate.Date);
                    }
                    
                }
            }
            return _items;
        }
    }
}
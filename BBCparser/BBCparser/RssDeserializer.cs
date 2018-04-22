using System.Collections.Generic;
using System.Xml.Serialization;

namespace BBCparser
{
    [XmlRoot(ElementName = "rss")]
    public class Rss
    {
        [XmlElement("channel")]
        public Channel Channel { get; set; }
    }


    [XmlRoot(ElementName = "channel")]
    public class Channel
    {
        [XmlElement("item")]
        public List<Content> Items { get; set; }
    }

    [XmlRoot(ElementName = "item")]
    public class Content
    {
        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("link")]
        public string Link { get; set; }

        [XmlElement("pubDate")]
        public string PubDate { get; set; }
    }
}
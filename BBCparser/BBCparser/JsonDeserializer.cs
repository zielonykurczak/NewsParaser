
public class Rootobject
{
    public string title { get; set; }
    public string link { get; set; }
    public string description { get; set; }
    public Item[] items { get; set; }
}

public class Item
{
    public string title { get; set; }
    public string description { get; set; }
    public string link { get; set; }
    public string pubDate { get; set; }
}

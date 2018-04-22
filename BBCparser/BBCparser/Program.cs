namespace BBCparser
{
    class Program
    {
        static void Main(string[] args)
        {
            
            NewsFeeder request = new NewsFeeder();
            var listOfNews = request.ListOfNewsGenerator();
            var generator = new JsonGenerator(listOfNews);
            generator.SaveJson();
        }
    }
}

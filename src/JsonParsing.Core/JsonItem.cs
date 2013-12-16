namespace JsonParsing.Core
{
    public class JsonItem : IJsonItem
    {
        public JsonItem(string title)
        {
            Title = title;
        }

        public string Title { get; set; }
    }
}
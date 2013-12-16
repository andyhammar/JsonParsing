using System.Linq;

namespace JsonParsing.Core
{
    public class JsonJsonObjectParser : IJsonParser
    {
        public IJsonData Parse(string json)
        {
            var jsonObject = Windows.Data.Json.JsonObject.Parse(json);
            var episodes = jsonObject.GetObject().GetNamedArray("episodes");

            var result = new JsonData();
            var jsonItems = episodes.Select(e => new JsonItem(e.GetObject().GetNamedString(("title"))));
            result.Items = jsonItems.Cast<IJsonItem>().ToArray();

            return result;
        }
    }
}
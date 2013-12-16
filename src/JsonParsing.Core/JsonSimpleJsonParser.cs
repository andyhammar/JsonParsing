using System.Collections.Generic;
using System.Linq;

namespace JsonParsing.Core
{
    public class JsonSimpleJsonParser : IJsonParser
    {
        public IJsonData ParseDynamic(string json)
        {
            dynamic jsonObject = SimpleJson.DeserializeObject(json);
            var episodes = jsonObject.episodes;
            
            var result = new JsonData();
            //var jsonItems = episodes.Select(e => new JsonItem(e.GetObject().GetNamedString(("title"))));
            //result.Items = jsonItems.Cast<IJsonItem>().ToArray();

            return result;
        }

        public IJsonData ParseDictionary(string json)
        {
            var jsonObject = (IDictionary<string, object>)SimpleJson.DeserializeObject(json);
            var episodes = ((List<object>)jsonObject["episodes"]).Cast<IDictionary<string, object>>();
            var items = episodes.Select(e => new JsonItem((string)e["title"]));
            var result = new JsonData();
            result.Items = items.Cast<IJsonItem>().ToArray();

            return result;
        }
    }
}
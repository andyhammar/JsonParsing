using System.Collections.Generic;
using System.Linq;

namespace JsonParsing.Core
{
    public class JsonSimpleJsonParser : IJsonParser
    {
        public IJsonData ParseDynamic(string json)
        {
            dynamic jsonObject = SimpleJson.DeserializeObject(json);
            List<object> episodes = jsonObject.episodes;
            var items = episodes.Select(e => new JsonItem(((dynamic)e).title));

            var result = new JsonData
            {
                Items = items.Cast<IJsonItem>().ToArray()
            };

            return result;
        }

        public IJsonData ParseDictionary(string json)
        {
            var jsonObject = (IDictionary<string, object>)SimpleJson.DeserializeObject(json);
            var episodes = ((List<object>)jsonObject["episodes"]).Cast<IDictionary<string, object>>();
            var items = episodes.Select(e => new JsonItem((string)e["title"]));
            var result = new JsonData
            {
                Items = items.Cast<IJsonItem>().ToArray()
            };

            return result;
        }
    }
}
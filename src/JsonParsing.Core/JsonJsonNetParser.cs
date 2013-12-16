using System.Linq;
using Newtonsoft.Json;

namespace JsonParsing.Core
{
    public class JsonJsonNetParser : IJsonParser
    {
        public IJsonData Parse(string json)
        {
            //using http://json2csharp.com/

            var obj = JsonConvert.DeserializeObject<RootObject>(json);

            if (obj == null)
                return null;

            var items = obj.episodes.Select(e => new JsonItem(e.title));
            var jsonItems = items.Cast<IJsonItem>().ToArray();
            var result = new JsonData
            {
                Items = jsonItems
            };

            return result;
        }
    }
}
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

            var result = new JsonData();
            result.Items = obj.episodes.Select(e => new JsonItem(e.title)).ToArray();

            return result;
        }
    }
}
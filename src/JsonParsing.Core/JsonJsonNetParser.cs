using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonParsing.Core
{
    public class JsonJsonNetParser : IJsonParser
    {
        private static string _episodesName = "episodes";
        private static string _titleName = "title";

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

        public IJsonData ParseWithJObjectParse(string json)
        {
            return ParseWithJObjectParse(json, "episodes", "title");
        }

        public IJsonData ParseWithJObjectParseFixedStrings(string json)
        {
            return ParseWithJObjectParse(json, _episodesName, _titleName);
        }

        public IJsonData ParseWithJObjectParse(string json, string episodesName, string titleName)
        {
            var obj = JObject.Parse(json);

            if (obj == null)
                return null;

            var items = obj.GetValue(episodesName).Select(e => new JsonItem(((JObject)e).GetValue(titleName).ToString()));

            var jsonItems = items.Cast<IJsonItem>().ToArray();
            var result = new JsonData
            {
                Items = jsonItems
            };

            return result;
        }

        public IJsonData ParseWithJObjectParseStringsInLinqQuery(string json)
        {
            var obj = JObject.Parse(json);

            if (obj == null)
                return null;

            var items = obj.GetValue("episodes").Select(e => new JsonItem(((JObject)e).GetValue("title").ToString()));


            var jsonItems = items.Cast<IJsonItem>().ToArray();
            var result = new JsonData
            {
                Items = jsonItems
            };

            return result;
        }
        }
}
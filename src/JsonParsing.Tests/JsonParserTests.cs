using System.Reflection;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using JsonParsing.Core;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace JsonParsing.Tests
{
    [TestClass]
    public class JsonParserTests
    {
        private string _text;

        [TestMethod]
        public async Task can_read_file()
        {
            _text = await ReadFile();
        }

        [TestMethod]
        public async Task can_parse_with_json_net()
        {
            _text = await ReadFile();

            var parser = new JsonJsonObjectParser();
            IJsonData data = parser.Parse(_text);
            Assert.AreEqual(113, data.Items.Length);
        }

        [TestMethod]
        public async Task can_parse_with_jsonobject()
        {
            _text = await ReadFile();

            var parser = new JsonJsonNetParser();
            IJsonData data = parser.Parse(_text);
            Assert.AreEqual(113, data.Items.Length);
        }
        [TestMethod]
        public async Task can_parse_with_simple_json_dynamic()
        {
            _text = await ReadFile();

            var parser = new JsonSimpleJsonParser();
            IJsonData data = parser.ParseDynamic(_text);
            Assert.AreEqual(113, data.Items.Length);
        }

        [TestMethod]
        public async Task can_parse_with_simple_json_dictionary()
        {
            _text = await ReadFile();

            var parser = new JsonSimpleJsonParser();
            IJsonData data = parser.ParseDictionary(_text);
            Assert.AreEqual(113, data.Items.Length);
        }

        private async Task<string> ReadFile()
        {
            var reader = new FileDataReader();
            var text = await reader.ReadAsync("data.json");
            Assert.IsNotNull(text);
            Assert.IsTrue(text.Length > 0);
            return text;
        }
    }
}

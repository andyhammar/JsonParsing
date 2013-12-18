using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using JsonParsing.Core;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace JsonParsing.Tests
{
    [TestClass]
    public class JsonParserTests
    {
        private static string _text;

        [ClassInitialize]
        public static void SetUp(TestContext context)
        {
            _text = ReadFile().GetAwaiter().GetResult();
        }

        [TestMethod]
        public async Task can_parse_with_json_net()
        {
            var parser = new JsonJsonObjectParser();
            IJsonData data = parser.Parse(_text);
            Assert.AreEqual(113, data.Items.Length);
        }

        [TestMethod]
        public async Task can_parse_with_jsonobject()
        {
            var parser = new JsonJsonNetParser();
            IJsonData data = parser.Parse(_text);
            Assert.AreEqual(113, data.Items.Length);
        }
        [TestMethod]
        [Ignore]
        public async Task can_parse_with_simple_json_dynamic()
        {
            var parser = new JsonSimpleJsonParser();
            IJsonData data = parser.ParseDynamic(_text);
            Assert.AreEqual(113, data.Items.Length);
        }

        [TestMethod]
        public async Task can_parse_with_simple_json_dictionary()
        {
            var parser = new JsonSimpleJsonParser();
            IJsonData data = parser.ParseDictionary(_text);
            Assert.AreEqual(113, data.Items.Length);
        }

        [TestMethod]
        public async Task speed_measurements()
        {
            
        }

        private async static Task<string> ReadFile()
        {
            var reader = new FileDataReader();
            var text = await reader.ReadAsync("data.json");
            Assert.IsNotNull(text);
            Assert.IsTrue(text.Length > 0);
            return text;
        }
    }
}

using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
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
        public void can_parse_with_json_object()
        {
            var parser = new JsonJsonObjectParser();
            IJsonData data = parser.Parse(_text);
            Assert.AreEqual(113, data.Items.Length);
        }

        [TestMethod]
        public void can_parse_with_json_net()
        {
            var parser = new JsonJsonNetParser();
            IJsonData data = parser.Parse(_text);
            Assert.AreEqual(113, data.Items.Length);
        }
        [TestMethod]
        [Ignore]
        public void can_parse_with_simple_json_dynamic()
        {
            var parser = new JsonSimpleJsonParser();
            IJsonData data = parser.ParseDynamic(_text);
            Assert.AreEqual(113, data.Items.Length);
        }

        [TestMethod]
        public void can_parse_with_simple_json_dictionary()
        {
            var parser = new JsonSimpleJsonParser();
            IJsonData data = parser.ParseDictionary(_text);
            Assert.AreEqual(113, data.Items.Length);
        }

        [TestMethod]
        public void speed_measurements()
        {
            RunMany(can_parse_with_json_net);
            RunMany(can_parse_with_json_object);
            RunMany(can_parse_with_simple_json_dictionary);
        }

        public void RunMany(Action action, int nbrIterations = 100, [CallerMemberName]string caller = "caller")
        {
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < nbrIterations; i++)
            {
                action();
            }
            var elapsed = sw.Elapsed;
            Debug.WriteLine("{0,-40}{1} ms/parse", action.GetMethodInfo().Name, elapsed.TotalMilliseconds / (double)100);
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

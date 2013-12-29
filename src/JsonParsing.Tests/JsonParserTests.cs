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
        private const int NBR_TIME_MEASUREMENT_ITERATIONS = 1000;
        private static string _json;

        [ClassInitialize]
        public static void SetUp(TestContext context)
        {
            _json = ReadFile().GetAwaiter().GetResult();
        }

        [TestMethod]
        public void can_parse_with_msft_json_object()
        {
            var parser = new JsonJsonObjectParser();
            IJsonData data = parser.Parse(_json);
            Assert.AreEqual(113, data.Items.Length);
        }

        [TestMethod]
        public void can_parse_with_json_net_deserialize_object()
        {
            var parser = new JsonJsonNetParser();
            IJsonData data = parser.Parse(_json);
            Assert.AreEqual(113, data.Items.Length);
        }

        [TestMethod]
        public void can_parse_with_json_net_jobject_parse_strings_as_parameters()
        {
            var parser = new JsonJsonNetParser();
            IJsonData data = parser.ParseWithJObjectParse(_json);
            Assert.AreEqual(113, data.Items.Length);
        }

        [TestMethod]
        public void can_parse_with_json_net_jobject_parse_static_strings()
        {
            var parser = new JsonJsonNetParser();
            IJsonData data = parser.ParseWithJObjectParse(_json);
            Assert.AreEqual(113, data.Items.Length);
        }

        [TestMethod]
        public void can_parse_with_json_net_jobject_parse_strings_in_linq_query()
        {
            var parser = new JsonJsonNetParser();
            IJsonData data = parser.ParseWithJObjectParseStringsInLinqQuery(_json);
            Assert.AreEqual(113, data.Items.Length);
        }

        [TestMethod]
        [Ignore]
        public void can_parse_with_simple_json_dynamic()
        {
            var parser = new JsonSimpleJsonParser();
            IJsonData data = parser.Parse(_json);
            Assert.AreEqual(113, data.Items.Length);
        }

        [TestMethod]
        public void can_parse_with_simple_json_dictionary()
        {
            var parser = new JsonSimpleJsonParser();
            IJsonData data = parser.ParseDictionary(_json);
            Assert.AreEqual(113, data.Items.Length);
        }

        [TestMethod]
        public void speed_measurements()
        {
            Debug.WriteLine("Running {0} iterations...", NBR_TIME_MEASUREMENT_ITERATIONS);
            var totalStopWatch = new Stopwatch();
            totalStopWatch.Start();
            RunMany(can_parse_with_json_net_deserialize_object);
            RunMany(can_parse_with_json_net_jobject_parse_static_strings);
            RunMany(can_parse_with_json_net_jobject_parse_strings_as_parameters);
            RunMany(can_parse_with_json_net_jobject_parse_strings_in_linq_query);
            RunMany(can_parse_with_msft_json_object);
            RunMany(can_parse_with_simple_json_dictionary);
            totalStopWatch.Stop();
            Debug.WriteLine("Finished {0} iterations in {1}", NBR_TIME_MEASUREMENT_ITERATIONS, totalStopWatch.Elapsed);
        }

        public void RunMany(Action action, int nbrIterations = NBR_TIME_MEASUREMENT_ITERATIONS, [CallerMemberName]string caller = "caller")
        {
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < nbrIterations; i++)
            {
                action();
            }
            var elapsed = sw.Elapsed;
            Debug.WriteLine("{0,-60}{1} ms/parse round", action.GetMethodInfo().Name, elapsed.TotalMilliseconds / (double)NBR_TIME_MEASUREMENT_ITERATIONS);
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

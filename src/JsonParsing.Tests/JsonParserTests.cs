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
        
        [TestMethod]
        public async Task TestMethod1()
        {
            await SetUp();
        }

        private async Task SetUp()
        {
            var reader = new JsonReader();
            var text = await reader.ReadAsync("data.json");
            Assert.IsNotNull(text);
            Assert.IsTrue(text.Length > 0);
        }
    }
}

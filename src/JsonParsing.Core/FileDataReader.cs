using System;
using System.Threading.Tasks;
using Windows.Storage;

namespace JsonParsing.Core
{
    public class FileDataReader
    {
        public async Task<string> ReadAsync(string fileRelativePath)
        {
            var file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///" + fileRelativePath));
            var text = await FileIO.ReadTextAsync(file);
            return text;
        }
    }
}

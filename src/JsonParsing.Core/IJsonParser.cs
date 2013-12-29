namespace JsonParsing.Core
{
    public interface IJsonParser
    {
        IJsonData Parse(string json);
    }
}
using System;

namespace JsonParsing.Core
{
    public interface IJsonData
    {
        IJsonItem[] Items { get; set; }
    }

    public interface IJsonItem
    {
        string Title { get; set; }
    }
}
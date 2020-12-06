using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class JsonLoader<T>
{
    public readonly T Items;

    public JsonLoader(string filename)
    {
        var data = ReadJsonFromFile(filename);

        Items = JsonConvert.DeserializeObject<T>(data);
    }

    private string ReadJsonFromFile(string _filePath)
    {
        return Resources.Load(_filePath).ToString();
    }
}

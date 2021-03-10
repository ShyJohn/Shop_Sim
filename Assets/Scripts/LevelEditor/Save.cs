using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save
{
    // public List<MapLayoutElement> mapLayout = new List<MapLayoutElement>();
    public Dictionary<string, MapLayoutElement> mapLayout = new Dictionary<string, MapLayoutElement>();
}

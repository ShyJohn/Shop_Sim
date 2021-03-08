using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapLayoutElement
{
    public string prefabPath;
    public SerializableVector2 position = new SerializableVector2();
    public List<SerializableVector2> gridSpaces = new List<SerializableVector2>();
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapLayoutElement
{
    public string prefabPath;
    public SerializableVector2Int position = new SerializableVector2Int();
    public List<SerializableVector2Int> gridSpaces = new List<SerializableVector2Int>();
}

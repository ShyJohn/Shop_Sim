using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;

public class LevelEditorManager : MonoBehaviour
{
    GameObject cursor;
    [SerializeField] Material greenCursorMaterial;
    [SerializeField] Material redCursorMaterial;
    float distanceFromGround = 0.5f;
    bool snapToGrid = true;
    bool leftClicked = false;
    bool rightClicked = false;
    string itemPrefabString = "Prefabs/LevelItemPrefab";
    Save currentSave = new Save();
    bool rayCastCollision = false;

    Dictionary<string, GameObject> mapLayoutDictionary = new Dictionary<string, GameObject>();

    Vector2Int cursorGridPosition = new Vector2Int();

    bool cursorAvailable = true;

    // Start is called before the first frame update
    void Start()
    {
        cursor = GameObject.FindGameObjectWithTag("LevelEditor_Cursor");
        LoadLevel();
    }

    // Update is called once per frame
    void Update()
    {
        HoverItemAbovePlane();
        UpdateCursorMaterial();
        DetectMouseClick();
    }

    private void CreateItem(Vector2Int position)
    {
        if (!mapLayoutDictionary.ContainsKey(position.ToString()))
        {
            // Instantiate gameobject
            Vector3 positionVector3 = new Vector3();
            positionVector3.x = position.x;
            positionVector3.y = 0.5f;
            positionVector3.z = position.y;
            GameObject item = Instantiate(Resources.Load(itemPrefabString), positionVector3, Quaternion.identity) as GameObject;

            // Create maplayoutelement
            MapLayoutElement newElement = new MapLayoutElement();
            newElement.prefabPath = itemPrefabString;
            newElement.position.x = position.x;
            newElement.position.y = position.y;

            // Create gridspace list for new maplayoutelement
            SerializableVector2Int gridSpace = new SerializableVector2Int();
            gridSpace.x = position.x;
            gridSpace.y = position.y;
            newElement.gridSpaces.Add(gridSpace);

            // Add the new element to the current save & maplayoutdictionary
            currentSave.mapLayout.Add(position.ToString(), newElement);
            mapLayoutDictionary.Add(position.ToString(), item);
        }
    }

    private void DeleteItem(Vector2Int position)
    {
        if(mapLayoutDictionary.ContainsKey(position.ToString()))
        {
            GameObject.Destroy(mapLayoutDictionary[position.ToString()]);
            mapLayoutDictionary.Remove(position.ToString());
            currentSave.mapLayout.Remove(position.ToString());
        }
    }

    private void DetectMouseClick()
    {
        if (Input.GetMouseButtonDown(0) && !leftClicked)
		{
            leftClicked = true;
            if(rayCastCollision)
            {
                CreateItem(cursorGridPosition);
            }
		}

		if (Input.GetMouseButtonUp(0) && leftClicked)
		{
			leftClicked = false;
		}

        if (Input.GetMouseButtonDown(1) && !rightClicked)
		{
            rightClicked = true;
            if(rayCastCollision)
            {
                DeleteItem(cursorGridPosition);
            }
		}

		if (Input.GetMouseButtonUp(1) && rightClicked)
		{
			rightClicked = false;
		}
    }

    private void UpdateCursorMaterial()
    {
        if(mapLayoutDictionary.ContainsKey(cursorGridPosition.ToString()) && cursorAvailable)
        {
            cursorAvailable = false;
            cursor.GetComponent<MeshRenderer>().material = redCursorMaterial;
        }
        else if(!mapLayoutDictionary.ContainsKey(cursorGridPosition.ToString()) && !cursorAvailable)
        {
            cursorAvailable = true;
            cursor.GetComponent<MeshRenderer>().material = greenCursorMaterial;
        }
    }
    public void SetItemCube()
    {
        itemPrefabString = "Prefabs/LevelItemPrefab";
    }
    public void SetItemCylinder()
    {
        itemPrefabString = "Prefabs/LevelItemCylinder";
    }
    private void HoverItemAbovePlane()
	{
		Vector3 pos = new Vector3();

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("Plane")))
		{
			float distance = Vector3.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), hit.point);

			pos = ray.GetPoint(distance);
            pos.y = pos.y + distanceFromGround;
            rayCastCollision = true;
		}
        else
        {
            rayCastCollision = false;
        }

        cursor.transform.position = pos;
        
        if (snapToGrid)
        {
            Vector3 tempPosition = cursor.transform.position;
            tempPosition.x = Mathf.Round(tempPosition.x);
            tempPosition.y = 0.5f;
            tempPosition.z = Mathf.Round(tempPosition.z);
            cursor.transform.position = tempPosition;
            cursorGridPosition.x = (int)tempPosition.x;
            cursorGridPosition.y = (int)tempPosition.z;
        }
	}

    private Save CreateSaveGameObject()
    {
        return currentSave;
    }

    public void SaveLevel()
    {
        Save save = CreateSaveGameObject();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, save);
        file.Close();
    }

    public void LoadLevel()
    {
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            Save save = (Save)bf.Deserialize(file);
            file.Close();

            foreach(KeyValuePair<string, MapLayoutElement> element in save.mapLayout)
            {
                // Instantiate gameobject
                Vector3 position = new Vector3();
                position.x = element.Value.position.x;
                position.y = 0.5f;
                position.z = element.Value.position.y;
                GameObject item = Instantiate(Resources.Load(element.Value.prefabPath), position, Quaternion.identity) as GameObject;

                // Create grid position and add to map layout dictionary
                Vector2Int gridPosition = new Vector2Int();
                gridPosition.x = element.Value.position.x;
                gridPosition.y = element.Value.position.y;
                mapLayoutDictionary.Add(gridPosition.ToString(), item);
            }
            currentSave = save;
        }   
    }
}

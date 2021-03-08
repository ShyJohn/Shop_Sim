using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;

public class LevelEditorManager : MonoBehaviour
{
    GameObject item;
    float distanceFromGround = 0.5f;
    bool snapToGrid = true;
    bool clicked = false;
    string itemPrefabString = "Prefabs/LevelItemPrefab";

    Save currentSave = new Save();
    bool rayCastCollision = false;
    // Start is called before the first frame update
    void Start()
    {
        item = GameObject.FindGameObjectWithTag("Item");
    }

    // Update is called once per frame
    void Update()
    {
        HoverItemAbovePlane();

        DetectMouseClick();
    }

    private void CreateItem(Vector3 position)
    {
        GameObject item = Instantiate(Resources.Load(itemPrefabString), position, Quaternion.identity) as GameObject;
        MapLayoutElement newElement = new MapLayoutElement();
        newElement.prefabPath = itemPrefabString;
        newElement.position.x = position.x;
        newElement.position.y = position.z;
        SerializableVector2 gridSpace = new SerializableVector2();
        gridSpace.x = position.x;
        gridSpace.y = position.y;
        newElement.gridSpaces.Add(gridSpace);
        currentSave.mapLayout.Add(newElement);
    }

    private void DetectMouseClick()
    {
        if (Input.GetMouseButtonDown(0) && !clicked)
		{
            clicked = true;
            if(rayCastCollision)
            {
                CreateItem(item.transform.position);
            }
		}

		if (Input.GetMouseButtonUp(0) && clicked)
		{
			clicked = false;
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

        item.transform.position = pos;
        
        if (snapToGrid)
        {
            Vector3 tempPosition = item.transform.position;
            tempPosition.x = Mathf.Round(tempPosition.x);
            tempPosition.y = 0.5f;
            tempPosition.z = Mathf.Round(tempPosition.z);
            item.transform.position = tempPosition;
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

            foreach(MapLayoutElement element in save.mapLayout)
            {
                Vector3 position = new Vector3();
                position.x = element.position.x;
                position.y = 0.5f;
                position.z = element.position.y;
                GameObject item = Instantiate(Resources.Load(element.prefabPath), position, Quaternion.identity) as GameObject;
            }
        }   
    }
}

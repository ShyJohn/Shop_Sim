using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class Utility : MonoBehaviour
{
	private static Utility singleton;

	public static List<ShopItem> ShopItems { get; set; }
	private const string SHOP_ITEM_FILENAME = "shopitems";
	private JsonLoader<List<ShopItem>> shopItemLoader;

	//public GameObject[] Aisles { get; set; }

	private void Awake()
	{
		if(singleton != null && singleton != this)
		{
			Destroy(gameObject);
			return;
		}
		else
		{
			singleton = this;
			DontDestroyOnLoad(gameObject);
		}

		LoadShopItems();

	}

	private void Start()
	{
		
	}

	public static Utility Instance
	{
		get
		{
			if(singleton == null)
			{
				Debug.LogError("[Utility]: Instance does not exist");
				return null;
			}
			return singleton;
		}
	}

	void LoadShopItems()
	{
		shopItemLoader = new JsonLoader<List<ShopItem>>(SHOP_ITEM_FILENAME);
		ShopItems = shopItemLoader.Items;

		//Debug.Log(JsonConvert.SerializeObject(ShopItems));
		//Debug.Log(JsonConvert.SerializeObject(ShopItems["Bread"]));
	}

	public List<ShopItem> GenerateShoppingList(int size)
	{
		List<ShopItem> list = new List<ShopItem>();

		int i;
		for(i = 0; i < size; i++)
		{
			int randomElement = Random.Range(0, ShopItems.Count - 1);
			ShopItem randomItem = ShopItems[randomElement];
			list.Add(randomItem);
			Debug.Log(this.GetType().ToString() + ": GenerateShoppingList(): Randomly generated item: " + randomItem.Name + " : £" + randomItem.Price);
		}

		return list;

	}

	public Vector3 GetMouseRayCastOnPlane()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		// Cast rays which collide ONLY with layer: Plane (8)
		if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("Plane")))
		{
			// Raycast hitting plane
		}

		return hit.point;
	}

	//public float GetAngleBetweenTwoPoints2D(Vector2 firstPoint, Vector2 secondPoint)
	//{
		
	//}
}

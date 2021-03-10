using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckoutScanner : MonoBehaviour {

	int itemCount = 0;

	int numberOfItemsScanned = 0;

	[SerializeField] Material green;
	[SerializeField] Material yellow;
	[SerializeField] Material red;
	[SerializeField] Material white;

	[SerializeField] TextMesh scanText;
	[SerializeField] TextMesh itemListText;
	[SerializeField] TextMesh totalText;

	public List<TextMesh> itemsAdded;
	List<GameObject> itemList = new List<GameObject>();

	// Scanning
	[SerializeField] float scanInterval = 1.0f;
	float timeToBeScanned = 0.0f;
	GameObject itemBeingScanned;
	[System.NonSerialized] public int numberOfItemsLeftToScan = 0;

	float totalCostThisTransaction = 0.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (itemCount == 1 && itemBeingScanned != null)
		{
			if (!itemBeingScanned.GetComponent<ItemInfo>().scanned)
			{
				GetComponent<MeshRenderer>().material = yellow;
				scanText.text = "Scanning";
				scanText.color = Color.yellow;
			}
			else
			{
				GetComponent<MeshRenderer>().material = green;
				scanText.text = "Scanned";
				scanText.color = Color.green;
			}
		}
		else if (itemCount > 1)
		{
			GetComponent<MeshRenderer>().material = red;
			scanText.text = "Too many items";
			scanText.color = Color.red;
		}
		else
		{ 
			GetComponent<MeshRenderer>().material = white;
			scanText.text = "Scan an item...";
			scanText.color = Color.white;
		}

		if(Time.time > timeToBeScanned && itemBeingScanned != null)
		{
			if(itemBeingScanned.GetComponent<ItemInfo>().scanned != true)
			{
				itemBeingScanned.GetComponent<ItemInfo>().scanned = true;
				totalCostThisTransaction += itemBeingScanned.GetComponent<ItemInfo>().price;
				totalText.text = "Total: £" + totalCostThisTransaction.ToString("F2");
				numberOfItemsScanned++;
				itemList.Add(itemBeingScanned);
				UpdateScreen();
				numberOfItemsLeftToScan--;
			}
		}
	}

	void UpdateScreen()
	{
		if(numberOfItemsScanned < 10)
		{
			itemsAdded[numberOfItemsScanned - 1].text = itemList[numberOfItemsScanned - 1].GetComponent<ItemInfo>().nameOfItem + ": £" + itemList[numberOfItemsScanned - 1].GetComponent<ItemInfo>().price.ToString("F2");
			Debug.Log(GetType().ToString() + ": UpdateScreen(): Item scanned: " + itemList[numberOfItemsScanned - 1].GetComponent<ItemInfo>().nameOfItem + " which cost: £" + itemList[numberOfItemsScanned - 1].GetComponent<ItemInfo>().price.ToString("F2"));
		}
		else
		{
			for(int i = 0; i < 10; i++)
			{
				itemsAdded[9 - i].text = itemList[numberOfItemsScanned - i - 1].GetComponent<ItemInfo>().nameOfItem + ": £" + itemList[numberOfItemsScanned - i - 1].GetComponent<ItemInfo>().price.ToString("F2");
				Debug.Log(GetType().ToString() + ": UpdateScreen(): Item scanned: " + itemList[numberOfItemsScanned - i - 1].GetComponent<ItemInfo>().nameOfItem);
			}
		}
	}

	public void ResetCheckout()
	{
		totalCostThisTransaction = 0.0f;
		itemCount = 0;
		numberOfItemsScanned = 0;
		itemBeingScanned = null;
		itemList.Clear();
		for (int i = 0; i < 10; i++)
		{
			itemsAdded[i].text = "";
		}
		totalText.text = "Total: £0.00";
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Item")
		{
			itemCount++;
			if (itemBeingScanned == null)
			{
				itemBeingScanned = other.gameObject;
				timeToBeScanned = Time.time + scanInterval;
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		
		if(other.tag == "Item")
		{
			itemCount--;
			itemBeingScanned = null;
		}
	}
}

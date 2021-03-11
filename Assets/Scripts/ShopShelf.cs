using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopShelf : MonoBehaviour
{
	[SerializeField] private string stockName;
	[SerializeField] private int maxStock;

	private Vector3 spawnPosition;

	private int currentStock;

	private Stack<GameObject> stock = 
	private bool spawningItems = false;
	private float timeToNextTick = 0.0f;
	private float tickInterval = 0.2f;
	public string StockName
	{
		get => stockName;
		set => stockName = value;
	}
	public int MaxStock
	{
		get => maxStock;
		set => maxStock = value;
	}
	public int CurrentStock
	{
		get => currentStock;
		set => currentStock = value;
	}

	private void Start()
	{
		currentStock = maxStock;
		if (transform.GetChild(0))
		{
			spawnPosition = transform.GetChild(0).transform.position;
			for (int itemCount = 0; itemCount < maxStock; itemCount++)
			{
				
			}
		}
	}

	private void Update()
	{
		if(spawningItems)
		{
			timeToNextTick -= Time.deltaTime;

			if(timeToNextTick <= 0.0f)
			{
				timeToNextTick = tickInterval;
				GameObject item = Instantiate(Resources.Load("Prefabs/Banana"), spawnPosition, Quaternion.identity) as GameObject;
			}
		}
	}

	public bool CheckIfInStock(int amount)
	{
		return currentStock > amount ? true : false;
	}
	public void RemoveStock(int amount)
	{
		if (currentStock >= amount)
		{
			currentStock -= amount;
		}
		//else
		//{
		//	int returnAmount = currentStock;
		//	currentStock = 0;
		//}
	}
}

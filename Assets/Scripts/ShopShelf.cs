using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopShelf : MonoBehaviour
{
	[SerializeField] private string stockName;
	[SerializeField] private int maxStock;

	private int currentStock;
	
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

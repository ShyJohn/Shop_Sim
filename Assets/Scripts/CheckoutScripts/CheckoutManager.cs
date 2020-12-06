using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckoutManager : MonoBehaviour
{
	private static CheckoutManager singleton;

	private List<Checkout> checkoutList = new List<Checkout>();

	[System.NonSerialized] public Checkout priorityCheckout;

	private void Awake()
	{
		if (singleton != null && singleton != this)
		{
			Destroy(gameObject);
			return;
		}
		else
		{
			singleton = this;
			DontDestroyOnLoad(gameObject);
		}

	}

	public static CheckoutManager Instance
	{
		get
		{
			if (singleton == null)
			{
				Debug.LogError("[CheckoutManager]: Instance does not exist");
				return null;
			}
			return singleton;
		}

	}

	private void Start()
	{
		GetCheckouts();
	}

	private void Update()
	{
		CheckPriorityCheckout();
	}

	private void GetCheckouts()
	{
		int i;
		GameObject[] tempCheckoutList = GameObject.FindGameObjectsWithTag("Checkout");
		for (i = 0; i < tempCheckoutList.Length; i++)
		{
			checkoutList.Add(tempCheckoutList[i].GetComponent<Checkout>());
		}
	}

	private void CheckPriorityCheckout()
	{
		int smallestQueue = int.MaxValue;

		foreach(Checkout g in checkoutList)
		{
			if (g.queueSize < smallestQueue)
			{
				priorityCheckout = g;
			}
		}
	}
}

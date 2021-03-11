using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum CustomerState
{ AISLE_SEARCH, GO_TO_CHECKOUT, PUT_ITEMS_ON_CHECKOUT, WAIT_FOR_ITEMS_TO_BE_SCANNED, PAY, LEAVE_SHOP }

public class CustomerAI : MonoBehaviour
{
	private NavMeshAgent agent;
	private List<ShopItem> shoppingList;
	private Stack<ShopItem> inventory = new Stack<ShopItem>();
	private Stack<GameObject> checkoutInventory = new Stack<GameObject>();

	public CustomerState state;

	private int targetAisle;
	private int targetShelfNumber;
	private ShopShelf targetShelf;

	private Vector3 targetPosition;
	private GameObject[] Aisles;

	private bool targetPositionFound;

	private Checkout targetCheckout;
	private Vector3 targetCheckoutQueuePosition = new Vector3();

	public int currentQueuePosition;

	private const int shoppingListSize = 3;

	private Vector3 customerDestroyerPosition = new Vector3();

	// Start is called before the first frame update
	private void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		shoppingList = Utility.Instance.GenerateShoppingList(shoppingListSize);
		customerDestroyerPosition = GameObject.FindGameObjectWithTag("CustomerDestroyer").GetComponent<Transform>().position;
	}

	// Update is called once per frame
	private void Update()
	{
		switch (state)
		{
			case CustomerState.AISLE_SEARCH:
				if (shoppingList.Count <= 0)
				{
					state = CustomerState.GO_TO_CHECKOUT;
					targetCheckout = CheckoutManager.Instance.priorityCheckout;
					targetCheckout.queue.Enqueue(gameObject);
					currentQueuePosition = targetCheckout.queue.Count - 1;
					targetCheckoutQueuePosition = targetCheckout.queueSlots[currentQueuePosition].position;
					break;
				}
				if (!targetPositionFound)
				{
					FindTargetPosition();
				}
				GoToShelf();
				TakeItemFromShelf();
				break;

			case CustomerState.GO_TO_CHECKOUT:
				WaitInQueue();
				break;

			case CustomerState.PUT_ITEMS_ON_CHECKOUT:
				PutItemsOnCheckout();
				break;

			case CustomerState.WAIT_FOR_ITEMS_TO_BE_SCANNED:
				CheckIfItemScanningFinished();
				break;
			case CustomerState.PAY:
				PayCashier();
				break;
			case CustomerState.LEAVE_SHOP:
				LeaveShop();
				break;
		}
	}
	
	private void LeaveShop()
	{
		agent.destination = customerDestroyerPosition;
	}
	private void PayCashier()
	{
		targetCheckout.GetComponentInChildren<CheckoutScanner>().ResetCheckout();
		// Destory all shop items
		// Temporary, in future put items in shopping trolley
		int i;
		int totalElements = checkoutInventory.Count;
		for (i = 0; i < totalElements; i++)
		{
			Destroy(checkoutInventory.Peek().gameObject);
			checkoutInventory.Pop();
		}
		targetCheckout.RemoveCustomerFromQueue();
		state = CustomerState.LEAVE_SHOP;
	}

	private void CheckIfItemScanningFinished()
	{
		if (targetCheckout.GetComponentInChildren<CheckoutScanner>().numberOfItemsLeftToScan <= 0)
		{
			state = CustomerState.PAY;
		}
	}

	private void WaitInQueue()
	{
		targetCheckoutQueuePosition = targetCheckout.queueSlots[currentQueuePosition].position;
		agent.destination = targetCheckoutQueuePosition;
		if (targetCheckout.queue.Peek() == gameObject && Vector3.Distance(transform.position, targetCheckoutQueuePosition) < 1.0f)
		{
			state = CustomerState.PUT_ITEMS_ON_CHECKOUT;
			Debug.Log(this.GetType().ToString() + ": WaitInQueue(): Customer put items on checkout");
		}
	}

	private void GoToShelf()
	{
		agent.destination = targetPosition;
	}

	private void PutItemsOnCheckout()
	{
		Debug.Log(this.GetType().ToString() + ": PutItemsOnCheckout(): Customer Inventory count: " + inventory.Count);
		int numberOfItemsInInventory = inventory.Count;
		targetCheckout.GetComponentInChildren<CheckoutScanner>().numberOfItemsLeftToScan = numberOfItemsInInventory;

		for (int i = 0; i < numberOfItemsInInventory; i++)
		{
			Vector3 itemSlotPosition = targetCheckout.itemSlots.GetChild(i).position;
			GameObject item = Instantiate(Resources.Load("Prefabs/Banana"), itemSlotPosition, Quaternion.identity) as GameObject;
			item.GetComponent<ItemInfo>().nameOfItem = inventory.Peek().Name;
			item.GetComponent<ItemInfo>().price = inventory.Peek().Price;
			Debug.Log(this.GetType().ToString() + ": PutItemsOnCheckout(): Item name: " + item.GetComponent<ItemInfo>().nameOfItem + " Item price: £" + item.GetComponent<ItemInfo>().price);
			checkoutInventory.Push(item);
			inventory.Pop();
		}

		state = CustomerState.WAIT_FOR_ITEMS_TO_BE_SCANNED;
		Debug.Log(this.GetType().ToString() + ": PutItemsOnCheckout(): Customer Waiting for items to be scanned");
	}

	private void FindTargetPosition()
	{
		targetAisle = shoppingList[0].Aisle;
		targetShelfNumber = shoppingList[0].Shelf;

		Aisles = GameObject.FindGameObjectsWithTag("Aisle");

		targetShelf = Aisles[targetAisle].transform.GetChild(targetShelfNumber).GetComponent<ShopShelf>();

		targetPosition = targetShelf.transform.position;
	}

	private void TakeItemFromShelf()
	{
		if (Vector3.Distance(transform.position, targetPosition) < 1.5f)
		{
			if (targetShelf.CheckIfInStock(1))
			{
				// In stock, get item
				targetShelf.RemoveStock(1);
				inventory.Push(shoppingList[0]);
				Debug.Log(this.GetType().ToString() + ": TakeItemFromShelf(): Item name: " + inventory.Peek().Name + " Item price: £" + inventory.Peek().Price);
				shoppingList.RemoveAt(0);
				targetPositionFound = false;
				
			}
			else
			{
				// Not in stock
			}
		}
	}
}
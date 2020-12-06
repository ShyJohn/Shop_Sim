using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckoutLaser : MonoBehaviour {

	public bool broken;

	int itemCount = 0;

	// Use this for initialization
	void Start () {
			
	}
	
	// Update is called once per frame
	void Update () {
		
		if(itemCount > 0)
		{
			broken = true;
		}
		else
		{
			broken = false;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Item")
		{
			itemCount++;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if(other.tag == "Item")
		{
			itemCount--;
		}
	}

}

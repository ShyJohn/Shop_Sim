using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckoutConveyor : MonoBehaviour {

	[SerializeField] GameObject conveyorLaser;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnCollisionStay(Collision collision)
	{
		
		if (collision.gameObject.tag == "Item")
		{
			if (conveyorLaser.GetComponent<CheckoutLaser>().broken == false)
			{
				collision.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(1.0f, 0.0f, 0.0f);
			}
			else
			{
				collision.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, 0.0f);
			}
		}
	
	}
}

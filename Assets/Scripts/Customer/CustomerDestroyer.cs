using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerDestroyer : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Customer" && other.GetComponent<CustomerAI>().state == CustomerState.LEAVE_SHOP)
		{
			Debug.Log(this.GetType().ToString() + ": OnTriggerEnter(): Destroying Customer GameObject");
			Destroy(other.gameObject);
			Instantiate(Resources.Load("Prefabs/Customer"), transform.position, Quaternion.identity);
		}
	}
}

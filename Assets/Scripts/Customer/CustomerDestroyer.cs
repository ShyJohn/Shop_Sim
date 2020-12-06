using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerDestroyer : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Customer")
		{
			Debug.Log(this.GetType().ToString() + ": OnTriggerEnter(): Destroying Customer GameObject");
			Destroy(other.gameObject);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseSelectable : MonoBehaviour {

	public bool hovered;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		int layerMask = 1 << 2;
		layerMask = ~layerMask;

		if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
		{
			if (hit.collider.gameObject == gameObject)
			{
				hovered = true;
			}
			else
			{
				hovered = false;
			}
		}
	}
}

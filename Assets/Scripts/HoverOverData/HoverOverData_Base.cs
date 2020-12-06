using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class HoverOverData_Base : MonoBehaviour
{
	GameObject hoverOverDataRoot;
	Text hoverOverDataText;
	protected string textData;

	// Start is called before the first frame update
	protected void Start()
	{
		hoverOverDataRoot = GameObject.FindGameObjectWithTag("HoverOverDataRoot");
		hoverOverDataText = GameObject.FindGameObjectWithTag("HoverOverDataText").GetComponent<Text>();
		textData = "";
	}

	protected void OnMouseEnter()
	{
		//If your mouse hovers over the GameObject with the script attached, output this message
		Debug.Log(this.GetType().ToString() + ": OnMouseOver(): Mouse is over GameObject.");
		hoverOverDataText.text = textData;
		if (!hoverOverDataRoot.activeSelf)
		{
			hoverOverDataRoot.SetActive(true);
		}
	}

	protected void OnMouseOver()
	{
		//hoverOverDataRoot.transform.position = GenerateTextPosition();
		//hoverOverDataRoot.transform.LookAt(Camera.main.transform.position);
		Vector3 hoverOverDataPosition = Input.mousePosition;
		hoverOverDataPosition.x += 100;
		hoverOverDataPosition.y -= 50;
		hoverOverDataRoot.GetComponent<RectTransform>().position = hoverOverDataPosition;
	}

	protected void OnMouseExit()
	{
		//The mouse is no longer hovering over the GameObject so output this message each frame
		Debug.Log(this.GetType().ToString() + ": On(): Mouse is over GameObject.");
		if (hoverOverDataRoot.activeSelf)
		{
			hoverOverDataRoot.SetActive(false);
		}
	}

	//protected Vector3 GenerateTextPosition()
	//{
	//	Vector3 position = new Vector3();
	//	float distanceAwayFromCamera = 15;
	//	Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

	//	position = ray.GetPoint(distanceAwayFromCamera);
	//	return position;
	//}

}

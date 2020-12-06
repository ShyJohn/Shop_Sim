using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckoutCamera : MonoBehaviour {

	[SerializeField] float bounds = 1.0f;
	[SerializeField] GameObject pivot;
	[SerializeField] float rotationSpeed = 50.0f;
	[SerializeField] float movementSpeed = 3.0f;
	float xRotation;
	float yRotation;

	// Use this for initialization
	void Start () {
		xRotation = 70.0f;
		yRotation = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {

		Debug.Log(Input.mousePosition.y);

		if (Input.mousePosition.x > Screen.width - (Screen.width / 16))
		{
			// RIGHT SIDE			
			yRotation += (rotationSpeed * Time.deltaTime);
		}

		if (Input.mousePosition.x < Screen.width / 16)
		{
			// LEFT SIDE
			yRotation -= (rotationSpeed * Time.deltaTime);
		}

		if (Input.mousePosition.y < Screen.height / 16 && xRotation < 70)
		{
			// BOTTOM
			xRotation += (rotationSpeed * Time.deltaTime);
		}

		if (Input.mousePosition.y > Screen.height - (Screen.height / 16) && xRotation > 0)
		{
			// TOP
			xRotation -= (rotationSpeed * Time.deltaTime);
		}

		if (Input.GetKey(KeyCode.A))
		{
			//yRotation -= (rotationSpeed * Time.deltaTime);
			pivot.transform.position -= (pivot.transform.right * Time.deltaTime * movementSpeed);
		}
		if (Input.GetKey(KeyCode.D))
		{
			//yRotation += (rotationSpeed * Time.deltaTime);
			pivot.transform.position += (pivot.transform.right * Time.deltaTime * movementSpeed);
		}

		if (Input.GetKey(KeyCode.W))
		{
			//xRotation -= (rotationSpeed * Time.deltaTime);
			pivot.transform.position += (pivot.transform.forward * Time.deltaTime * movementSpeed);
		}
		if(Input.GetKey(KeyCode.S))
		{
			//xRotation += (rotationSpeed * Time.deltaTime);
			pivot.transform.position -= (pivot.transform.forward * Time.deltaTime * movementSpeed);
		}

		transform.localRotation = Quaternion.AngleAxis(xRotation, new Vector3(1.0f, 0.0f, 0.0f));
		pivot.transform.rotation = Quaternion.AngleAxis(yRotation, new Vector3(0.0f, 1.0f, 0.0f));
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrthographicCameraController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 10.0f;
    [SerializeField] private float zoomSpeed = 5.0f;
    [SerializeField] private float rotateSpeed = 25.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0f ) // forward
        {
            Camera.main.orthographicSize -= (Input.GetAxis("Mouse ScrollWheel") * zoomSpeed);
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
		{
			transform.position = transform.position + (-transform.right * movementSpeed * Time.deltaTime);
		}

		if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
		{
			transform.position = transform.position + (transform.right * movementSpeed * Time.deltaTime);
		}

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
		{
            // Vector3 newPosition = transform.position + (transform.forward * movementSpeed * Time.deltaTime * 2.0f);
            // newPosition.y = transform.position.y;
			transform.position = transform.position + (transform.forward * movementSpeed * Time.deltaTime * 2.0f);
		}

		if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
		{
			// Vector3 newPosition = transform.position + (-transform.forward * movementSpeed * Time.deltaTime * 2.0f);
            // newPosition.y = transform.position.y;
			transform.position = transform.position + (-transform.forward * movementSpeed * Time.deltaTime * 2.0f);
		}

        if (Input.GetKey(KeyCode.E))
		{
            transform.Rotate(new Vector3(0, -rotateSpeed * Time.deltaTime, 0), Space.Self);
		}

		if (Input.GetKey(KeyCode.Q))
		{
			transform.Rotate(new Vector3(0, rotateSpeed * Time.deltaTime, 0), Space.Self);
		}
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockCage : MonoBehaviour
{
	private bool hovered;
	private bool selected = false;
	private Rigidbody rigidBody;

	Outline outline;
	MouseSelectable mouseSelectable;

	// Start is called before the first frame update
	void Start()
    {
		hovered = GetComponent<MouseSelectable>().hovered;
		outline = GetComponent<Outline>();
		mouseSelectable = GetComponent<MouseSelectable>();
		rigidBody = GetComponent<Rigidbody>();
	}

    // Update is called once per frame
    void Update()
    {
		CheckIfSelected();

		EnableOutlineIfSelected();

		if (Input.GetMouseButtonDown(0))
		{
			// Left click
			if (hovered)
			{
				selected = true;
			}
		}

		if (Input.GetMouseButtonUp(0))
		{
			selected = false;
		}

		if (selected)
		{
			// Move cage towards mouse using physics
			RotateTowardsMousePosition();
			MoveTowardsMousePosition();
		}
	}

	private void MoveTowardsMousePosition()
	{
		Vector3 targetPosition = Utility.Instance.GetMouseRayCastOnPlane();
		float speed = 25.0f;

		if (rigidBody.velocity.magnitude < 5.0f)
		{
			rigidBody.AddForce((targetPosition - transform.position).normalized * speed);
		}
	}

	private void RotateTowardsMousePosition()
	{
		float rotationSpeed = 5.0f;
		Vector3 targetPosition = Utility.Instance.GetMouseRayCastOnPlane();

		Vector3 temporaryTargetPosition = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);

		Vector3 targetDirection = temporaryTargetPosition - transform.position;

		float singleStep = rotationSpeed * Time.deltaTime;

		Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
		Debug.DrawRay(transform.position, newDirection, Color.red);
		transform.rotation = Quaternion.LookRotation(newDirection);
	}

	private void CheckIfSelected()
	{
		hovered = mouseSelectable.hovered;
	}

	private void EnableOutlineIfSelected()
	{
		outline.enabled = selected ? true : false;
	}
}

using UnityEngine;

public class PickUpProduct : MonoBehaviour
{
	[SerializeField] private float distanceFromGround = 0.5f;

	private bool hovered;
	private bool grabbed;

	Outline outline;
	MouseSelectable mouseSelectable;

	// Use this for initialization
	private void Start()
	{
		hovered = GetComponent<MouseSelectable>().hovered;
		outline = GetComponent<Outline>();
		mouseSelectable = GetComponent<MouseSelectable>();
	}

	// Update is called once per frame
	private void Update()
	{
		CheckIfSelected();

		EnableOutlineIfSelected();

		if (Input.GetMouseButtonDown(0))
		{
			// Left click
			if (hovered)
			{
				grabbed = true;
			}
		}

		if (Input.GetMouseButtonUp(0))
		{
			grabbed = false;
		}

		if (grabbed)
		{
			HoverItemAbovePlane();
		}
	}

	private void CheckIfSelected()
	{
		hovered = mouseSelectable.hovered;
	}

	private void EnableOutlineIfSelected()
	{
		outline.enabled = hovered ? true : false;
	}

	private void HoverItemAbovePlane()
	{
		Vector3 pos = new Vector3();

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("Plane")))
		{
			float distance = Vector3.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), hit.point);

			pos = ray.GetPoint(distance - distanceFromGround);
		}

		transform.position = pos;
	}
}
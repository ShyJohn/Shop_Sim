using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aisle : MonoBehaviour
{
    [SerializeField] public int aisleNumber;
	Transform start;
	Transform end;

	private void Start()
	{
		start = transform.GetChild(0);
		end = transform.GetChild(1);
	}
}

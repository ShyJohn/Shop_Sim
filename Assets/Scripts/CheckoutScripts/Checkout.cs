using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkout : MonoBehaviour
{
	[SerializeField] public Transform[] queueSlots;
	[SerializeField] public Transform itemSlots;
	public int queueSize { get; set; }
	public int nextOpenQueueSlot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		//nextOpenQueueSlot = queueSize - 1;
    }
}

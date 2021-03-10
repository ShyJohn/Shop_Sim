using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkout : MonoBehaviour
{
	[SerializeField] public Transform[] queueSlots;
	[SerializeField] public Transform itemSlots;
  public Queue<GameObject> queue = new Queue<GameObject>();
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
      
      public void RemoveCustomerFromQueue()
      {
        // Remove customer from the queue
          queue.Dequeue();
          foreach(GameObject customer in queue)
          {
              // Update the queue position of all the remaining customers
              customer.GetComponent<CustomerAI>().currentQueuePosition -= 1;
          }
      }
}

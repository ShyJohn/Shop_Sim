using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverOverData_Shelf : HoverOverData_Base
{
	ShopShelf shelf;

    // Start is called before the first frame update
    new private void Start()
    {
		base.Start();
		shelf = GetComponent<ShopShelf>();
		textData = shelf.StockName + "\n" + "Current Stock: " + shelf.CurrentStock + "\n" + "Max Stock: " + shelf.MaxStock;
	}
}

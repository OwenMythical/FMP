using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionScript : MonoBehaviour
{
    public string Item;
    public int Amount;
    InventoryManager IM;

    void Start()
    {
        IM = (InventoryManager)GameObject.FindGameObjectWithTag("Canvas").GetComponent("InventoryManager");
    }

    public void Collect()
    {
        if (Item == "MetalScrap")
        {
            IM.MetalScrap += Amount;
            IM.Refresh();
        }
        else if (Item == "WoodScrap")
        {
            IM.WoodScrap += Amount;
            IM.Refresh();
        }
        else
        {
            IM.AddItem(Item);
        }
        Destroy(gameObject);
    }
}

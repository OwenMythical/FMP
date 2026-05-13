using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionScript : MonoBehaviour
{
    public string Item;
    public int Amount;
    InventoryManager IM;
    DialogueManager DM;

    void Start()
    {
        IM = (InventoryManager)GameObject.FindGameObjectWithTag("Canvas").GetComponent("InventoryManager");
        DM = (DialogueManager)GameObject.FindGameObjectWithTag("Canvas").GetComponent("DialogueManager");
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
        else if (Amount < 0)
        {
            IM.AddKey(Item);
            DM.DialogueStart(("Got " + Item));
        }
        else
        {
            IM.AddItem(Item);
        }
        Destroy(gameObject);
    }
}

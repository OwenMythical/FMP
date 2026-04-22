using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionScript : MonoBehaviour
{
    public string Material;
    public int Amount;
    InventoryManager IM;

    void Start()
    {
        IM = (InventoryManager)GameObject.FindGameObjectWithTag("Canvas").GetComponent("InventoryManager");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (Material == "MetalScrap")
            {
                IM.MetalScrap += Amount;
            }
            else if (Material == "WoodScrap")
            {
                IM.WoodScrap += Amount;
            }
            Destroy(gameObject);
        }
    }
}

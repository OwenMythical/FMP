using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InteractionScript : MonoBehaviour
{
    public bool CollectInteraction;
    public bool DialogueInteraction;
    public bool KeyCheck;
    public bool DoorOpen;
    public bool OpenShop;
    public bool Collected = false;
    public string Text;
    public string Item;
    public int Amount;
    DialogueManager DM;
    InventoryManager IM;
    GameObject Player;
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        GameObject Can = GameObject.FindGameObjectWithTag("Canvas");
        DM = (DialogueManager)Can.GetComponent("DialogueManager");
        IM = (InventoryManager)Can.GetComponent("InventoryManager");
    }

    public void Interact()
    {
        if (DialogueInteraction == true)
        {
            DM.DialogueStart(Text);
        }

        if (DoorOpen == true)
        {
            if (KeyCheck == true)
            {
                bool Check = IM.CheckKey(Item);
                if (Check == true)
                {
                    Destroy(gameObject);
                }
                else if (Text != "")
                {
                    DM.DialogueStart(Text);
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }

        if (CollectInteraction == true && Collected == false)
        {
            Collected = true;
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
        }

        if (OpenShop == true)
        {
            IM.OpenShop();
        }
    }
}

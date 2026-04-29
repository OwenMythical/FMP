using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryManager : MonoBehaviour
{
    public int MetalScrap = 0;
    public int WoodScrap = 0;
    public List<TextMeshProUGUI> Inventory = new List<TextMeshProUGUI>();

    public List<int> FindItem(string Item)
    {
        List<int> FoundPositions = new List<int>();
        int i = 1;
        foreach (TextMeshProUGUI InvText in Inventory)
        {
            if (InvText.text == Item)
            {
                FoundPositions.Add(i);
            }
            i += 1;
        }
        Debug.Log(FoundPositions);
        return (FoundPositions);
    }

    public void CraftItem(string Item)
    {
        bool CanCraft = false;
        int MS = 0;
        int WS = 0;
        switch(Item)
        {
            case "Pipe":
                MS = 2;
                break;
            default:
                MS = 999;
                WS = 999;
                break;
        }

        if (MetalScrap >= MS && WoodScrap >= WS)
        {
            MetalScrap -= MS;
            WoodScrap -= WS;
            CanCraft = true;
        }

        if (CanCraft == true)
        {
            foreach (TextMeshProUGUI InvText in Inventory)
            {
                if (InvText.text == "Nothing")
                {
                    InvText.text = Item;
                    break;
                }
            }
        }
    }

    public void AddItem(string Item)
    {
        foreach (TextMeshProUGUI InvText in Inventory)
        {
            if (InvText.text == "Nothing")
            {
                InvText.text = Item;
                break;
            }
        }
    }
}

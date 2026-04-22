using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryManager : MonoBehaviour
{
    public int MetalScrap = 0;
    public int WoodScrap = 0;
    public List<string> Inventory = new List<string>();

    public List<int> FindItem(string Item)
    {
        List<int> FoundPositions = new List<int>();
        int i = 1;
        foreach (string InvItem in Inventory)
        {
            if (InvItem == Item)
            {
                FoundPositions.Add(i);
            }
            i += 1;
        }
        Debug.Log(FoundPositions);
        return (FoundPositions);
    }
}

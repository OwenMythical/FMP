using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryManager : MonoBehaviour
{
    public int MetalScrap = 0;
    public int WoodScrap = 0;
    public List<TextMeshProUGUI> Inventory = new List<TextMeshProUGUI>();
    public GameObject ItemPickupPrefab;
    AssetFinder AF;

    void Start()
    {
        AF = (AssetFinder)GameObject.FindGameObjectWithTag("Canvas").GetComponent("AssetFinder");
    }

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
                MS = 3;
                break;
            case "Noise Maker":
                MS = 2;
                WS = 1;
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
        bool Found = false;
        foreach (TextMeshProUGUI InvText in Inventory)
        {
            if (InvText.text == "Nothing")
            {
                InvText.text = Item;
                Found = true;
                break;
            }
        }

        if (Found == false)
        {
            GameObject NewItemPickup = GameObject.Instantiate(ItemPickupPrefab);
            NewItemPickup.transform.position = gameObject.transform.position;
            CollectionScript ColScript = (CollectionScript)NewItemPickup.GetComponent("CollectionScript");
            ColScript.Item = Item;
            SpriteRenderer ISR = (SpriteRenderer)NewItemPickup.GetComponent("SpriteRenderer");
            string Path = AF.GetPath(Item);
            ISR.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(Path);
        }
    }
}

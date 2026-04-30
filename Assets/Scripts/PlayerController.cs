using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Progress;

public class PlayerController : MonoBehaviour
{
    public float MoveSpeed = 1;
    public GameObject ItemObject;
    public GameObject PR;
    AssetFinder AF;
    string CurrentEquipped = "Nothing";
    int EquippedSpace = 0;
    SpriteRenderer OSR;
    Rigidbody2D RB;
    CircleCollider2D Collider;
    InventoryManager IM;

    void Start()
    {
        RB = (Rigidbody2D)gameObject.GetComponent("Rigidbody2D");
        Collider = (CircleCollider2D)gameObject.GetComponent("CircleCollider2D");
        IM = (InventoryManager)GameObject.FindGameObjectWithTag("Canvas").GetComponent("InventoryManager");
        OSR = (SpriteRenderer)ItemObject.GetComponent("SpriteRenderer");
        AF = (AssetFinder)GameObject.FindGameObjectWithTag("Canvas").GetComponent("AssetFinder");
    }

    void Update()
    {
        float Horizontal = Input.GetAxisRaw("Horizontal") * MoveSpeed;
        float Vertical = Input.GetAxisRaw("Vertical") * MoveSpeed;

        RB.velocity = new Vector2(Horizontal, Vertical);

        //Object Interaction
        if (Input.GetKeyDown(KeyCode.E))
        {
            List<Collider2D> Colliders = new List<Collider2D>();
            ContactFilter2D Filter = new ContactFilter2D();
            Collider.OverlapCollider(Filter.NoFilter(),Colliders);
            foreach (Collider2D Coll in Colliders)
            {
                if (Coll.gameObject.tag == "InteractionObject")
                {
                    InteractionScript IntScript = (InteractionScript)Coll.gameObject.GetComponent("InteractionScript");
                    IntScript.Interact();
                }
                else if (Coll.gameObject.tag == "CollectionObject")
                {
                    CollectionScript ColScript = (CollectionScript)Coll.gameObject.GetComponent("CollectionScript");
                    ColScript.Collect();
                }
            }
        }

        //Item Dropping
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (CurrentEquipped != "Nothing")
            {
                GameObject NewItemPickup = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/ItemPickup.prefab"));
                NewItemPickup.transform.position = gameObject.transform.position;
                CollectionScript ColScript = (CollectionScript)NewItemPickup.GetComponent("CollectionScript");
                ColScript.Item = CurrentEquipped;
                SpriteRenderer ISR = (SpriteRenderer)NewItemPickup.GetComponent("SpriteRenderer");
                ISR.sprite = OSR.sprite;
                IM.Inventory[EquippedSpace].text = "Nothing";
                CurrentEquipped = "Nothing";
                OSR.enabled = false;
            }
        }

        //Item Using
        if (Input.GetButtonDown("Fire1"))
        {
            if (CurrentEquipped == "Noise Maker")
            {
                GameObject NoiseMaker = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Distractor.prefab"));
                NoiseMaker.transform.position = gameObject.transform.position;
                Rigidbody2D NMRB = (Rigidbody2D)NoiseMaker.GetComponent("Rigidbody2D");
                NMRB.AddForce(PR.transform.up * 750);
                IM.Inventory[EquippedSpace].text = "Nothing";
                CurrentEquipped = "Nothing";
                OSR.enabled = false;
            }
        }

        //Inventory Equipping
        string ItemToEquip = "Nothing";
        bool InvButtonPressed = false;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ItemToEquip = IM.Inventory[0].text;
            EquippedSpace = 0;
            InvButtonPressed = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ItemToEquip = IM.Inventory[1].text;
            EquippedSpace = 1;
            InvButtonPressed = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ItemToEquip = IM.Inventory[2].text;
            EquippedSpace = 2;
            InvButtonPressed = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ItemToEquip = IM.Inventory[3].text;
            EquippedSpace = 3;
            InvButtonPressed = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ItemToEquip = IM.Inventory[4].text;
            EquippedSpace = 4;
            InvButtonPressed = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            ItemToEquip = IM.Inventory[5].text;
            EquippedSpace = 5;
            InvButtonPressed = true;
        }

        if (InvButtonPressed == true)
        {
            if (ItemToEquip == "Nothing")
            {
                OSR.enabled = false;
                CurrentEquipped = "Nothing";
            }
            else
            {
                string Path = AF.GetPath(ItemToEquip);
                OSR.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(Path);
                CurrentEquipped = ItemToEquip;
                OSR.enabled = true;
            }
        }
    }
}

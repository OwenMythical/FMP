using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class ShadowGenerator : MonoBehaviour
{
    public GameObject ShadowParent;
    public GameObject ShadowCaster;
    public Tilemap tilemap;

    public void Start()
    {
        TileBase[] Tiles = tilemap.GetTilesBlock(tilemap.cellBounds);
        foreach (TileBase tile in Tiles)
        {
            if (tile != null)
            {
                GameObject NewCaster = Instantiate(ShadowCaster,ShadowParent.transform);
                
            }
        }
    }


}

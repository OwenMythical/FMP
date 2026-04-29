using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class AssetFinder : MonoBehaviour
{
    public string GetPath(string Item)
    {
        string Path = null;

        if (Item == "Pipe")
        {
            Path = "Assets/Sprites/Pipe.png";
        }

        return Path;
    }
}

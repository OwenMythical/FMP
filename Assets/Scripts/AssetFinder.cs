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
        if (Item == "Noise Maker")
        {
            Path = "Assets/Sprites/Light2.png";
        }
        if (Item == "Axe")
        {
            Path = "Assets/Sprites/Axe.png";
        }

        return Path;
    }
}

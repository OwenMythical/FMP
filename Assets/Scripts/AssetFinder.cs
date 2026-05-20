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
            Path = "Pipe";
        }
        if (Item == "Noise Maker")
        {
            Path = "Light2";
        }
        if (Item == "Axe")
        {
            Path = "Axe";
        }

        return Path;
    }
}

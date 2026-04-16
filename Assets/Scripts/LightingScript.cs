using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightingScript : MonoBehaviour
{
    public Light2D GlobalLighting;

    void Start()
    {
        GlobalLighting.color = new Color(0, 0, 0);
    }
}

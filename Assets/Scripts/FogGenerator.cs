using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FogGenerator : MonoBehaviour
{
    public float StartPosX = -20f;
    public float StartPosY = -20f;
    public float EndPosX = 20f;
    public float EndPosY = 20f;
    public GameObject FogObject;

    // Start is called before the first frame update
    void Start()
    {
        float PosX = StartPosX;
        while (PosX < EndPosX)
        {
            float PosY = StartPosY;
            while (PosY < EndPosY)
            {
                GameObject Fog = Instantiate(FogObject,gameObject.transform);
                Fog.transform.position = new Vector2(PosX, PosY);
                PosY += 2;
            }
            PosX += 2;
        }
    }
}

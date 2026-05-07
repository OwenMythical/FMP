using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class FlashlightScript : MonoBehaviour
{
    public float Battery = 1000;
    PolygonCollider2D FlashCollider;
    Light2D LightSource;
    void Start()
    {
        LightSource = (Light2D)gameObject.GetComponent("Light2D");
        FlashCollider = (PolygonCollider2D)gameObject.GetComponent("PolygonCollider2D");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" || collision.tag == "Dead")
        {
            EnemyTransparency ET = (EnemyTransparency)collision.gameObject.GetComponent("EnemyTransparency");
            Vector3 Direction = collision.transform.position - gameObject.transform.position;
            List<RaycastHit2D> Results = new List<RaycastHit2D>();
            if (Physics2D.Raycast(transform.position, Direction, new ContactFilter2D(), Results) > 0)
            {
                if (Results[1].collider.gameObject == collision.gameObject)
                {
                    ET.Illuminated = true;

                }
                else
                {
                    ET.Illuminated = false;
                }
            }
            else
            {
                ET.Illuminated = false;
            }
        }

        if (collision.tag == "Fog")
        {
            Tilemap Fog = collision.GetComponent<Tilemap>();
            Vector3 Position = collision.ClosestPoint(FlashCollider.transform.position);
            Vector3Int CellPosition = Fog.WorldToCell(Position + new Vector3(0.5f, 0.5f));
            Debug.Log(CellPosition);
            Fog.SetTile(CellPosition, null);
            //try getting every tile position and using trial and error instead? might be laggy
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" || collision.tag == "Dead")
        {
            EnemyTransparency ET = (EnemyTransparency)collision.gameObject.GetComponent("EnemyTransparency");
            ET.Illuminated = false;
        }
    }

    void Update()
    {
        if (LightSource.enabled == true)
        {
            Battery -= 0.01f;
            if (Battery <= 0)
            {
                Battery = 0;
                LightSource.enabled = false;
                FlashCollider.enabled = false;
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                LightSource.enabled = false;
                FlashCollider.enabled = false;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.F) && Battery > 0)
            {
                LightSource.enabled = true;
                FlashCollider.enabled = true;
            }
        }
    }
}

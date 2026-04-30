using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;

public class PointlightScript : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
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
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            EnemyTransparency ET = (EnemyTransparency)collision.gameObject.GetComponent("EnemyTransparency");
            ET.Illuminated = false;
        }
    }
}

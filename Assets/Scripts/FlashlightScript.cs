using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class FlashlightScript : MonoBehaviour
{
    ContactFilter2D ContactFilter;
    void Start()
    {
        ContactFilter = new ContactFilter2D();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            SpriteRenderer SR = (SpriteRenderer)collision.gameObject.GetComponent("SpriteRenderer");
            Vector3 Direction = collision.transform.position - gameObject.transform.position;
            List<RaycastHit2D> Results = new List<RaycastHit2D>();
            if (Physics2D.Raycast(transform.position, Direction, new ContactFilter2D(), Results) > 0)
            {
                if (Results[1].collider.gameObject == collision.gameObject)
                {
                    SR.enabled = true;
                }
                else
                {
                    SR.enabled = false;
                }
            }
            else
            {
                Debug.Log("No Colliders");
                SR.enabled = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            SpriteRenderer SR = (SpriteRenderer)collision.gameObject.GetComponent("SpriteRenderer");
            SR.enabled = false;
        }
    }
}

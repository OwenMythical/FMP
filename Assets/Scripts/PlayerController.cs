using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float MoveSpeed = 1;
    Rigidbody2D RB;
    CircleCollider2D Collider;
    // Start is called before the first frame update
    void Start()
    {
        RB = (Rigidbody2D)gameObject.GetComponent("Rigidbody2D");
        Collider = (CircleCollider2D)gameObject.GetComponent("CircleCollider2D");
    }

    // Update is called once per frame
    void Update()
    {
        float Horizontal = Input.GetAxisRaw("Horizontal") * MoveSpeed;
        float Vertical = Input.GetAxisRaw("Vertical") * MoveSpeed;

        RB.velocity = new Vector2(Horizontal, Vertical);

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
            }
        }
    }


}

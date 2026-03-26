using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed = 1;
    Rigidbody2D RB;
    // Start is called before the first frame update
    void Start()
    {
        RB = (Rigidbody2D)gameObject.GetComponent("Rigidbody2D");
    }

    // Update is called once per frame
    void Update()
    {
        float Horizontal = Input.GetAxisRaw("Horizontal") * MoveSpeed;
        float Vertical = Input.GetAxisRaw("Vertical") * MoveSpeed;

        RB.velocity = new Vector2(Horizontal, Vertical);
    }
}

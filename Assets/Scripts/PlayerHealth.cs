using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float Health = 100;

    public void TakeDamage(float Damage)
    {
        Health -= Damage;
        if (Health <= 0)
        {
            SpriteRenderer SR = (SpriteRenderer)gameObject.GetComponent("SpriteRenderer");
            SR.color = new Color(0.75f, 0.75f, 0.75f);
            Destroy(gameObject.GetComponent("PlayerController"));
        }
    }
}

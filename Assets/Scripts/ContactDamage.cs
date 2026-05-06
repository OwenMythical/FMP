using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactDamage : MonoBehaviour
{
    public float Cooldown;
    public float Damage;
    public Collider2D Hitbox;
    bool CanAttack = true;

    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (CanAttack == true && collision.tag == "Player")
        {
            CanAttack = false;

            yield return new WaitForSeconds(Cooldown);
            CanAttack = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactDamage : MonoBehaviour
{
    public float Cooldown;
    public float Damage;
    public bool CanAttack = true;
    public EnemyHealth EH;

    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (CanAttack == true && collision.tag == "Player" && EH.CanMove == true)
        {
            CanAttack = false;
            PlayerHealth PH = (PlayerHealth)collision.GetComponent("PlayerHealth");
            PH.TakeDamage(Damage);
            yield return new WaitForSeconds(Cooldown);
            CanAttack = true;
        }
    }
}

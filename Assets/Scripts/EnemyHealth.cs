using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float Health = 100;
    public int InfluenceGain;
    public bool CanMove = true;
    public GameObject Hitbox;

    public void TakeDamage(float Damage, float StunTime)
    {
        Health -= Damage;
        EnemyPathfinding EP = (EnemyPathfinding)gameObject.GetComponent("EnemyPathfinding");
        EP.Damaged();
        if (Health <= 0)
        {
            InventoryManager IM = (InventoryManager)GameObject.FindGameObjectWithTag("Canvas").GetComponent("InventoryManager");
            IM.Influence += InfluenceGain;
            SpriteRenderer SR = (SpriteRenderer)gameObject.GetComponent("SpriteRenderer");
            SR.color = new Color(0.75f, 0.75f, 0.75f);
            gameObject.tag = "Dead";
            Destroy(gameObject.GetComponent("EnemyPathfinding"));
            Destroy(gameObject.GetComponent("Animator"));
            Destroy(gameObject.GetComponent("RandomSoundPlayer"));
            Destroy(Hitbox.GetComponent("ContactDamage"));
        }
        StartCoroutine(Stun(StunTime));
    }

    public IEnumerator Stun(float Time)
    {
        CanMove = false;
        yield return new WaitForSeconds(Time);
        CanMove = true;
    }
}

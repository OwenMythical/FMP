using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float Health = 100;
    public bool CanMove = true;

    public void TakeDamage(float Damage, float StunTime)
    {
        Health -= Damage;
        if (Health <= 0)
        {
            SpriteRenderer SR = (SpriteRenderer)gameObject.GetComponent("SpriteRenderer");
            SR.color = new Color(0.75f, 0.75f, 0.75f);
            Destroy(gameObject.GetComponent("EnemyPathfinding"));
            Destroy(gameObject.GetComponent("Animator"));
            Destroy(gameObject.GetComponent("RandomSoundPlayer"));
            //Destroy enemy damaging script

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

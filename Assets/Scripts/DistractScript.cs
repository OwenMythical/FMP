using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistractScript : MonoBehaviour
{
    CircleCollider2D Trigger;
    void Start()
    {
        Trigger = (CircleCollider2D)gameObject.GetComponent("CircleCollider2D");
        StartCoroutine(Distract());
    }

    IEnumerator Distract()
    {
        yield return new WaitForSeconds(3);
        ContactFilter2D ContactF = new ContactFilter2D();
        ContactF.NoFilter();
        List<Collider2D> Results = new List<Collider2D>();
        Trigger.OverlapCollider(ContactF, Results);
        foreach (Collider2D Collider in Results)
        {
            if (Collider.gameObject.tag == "Enemy")
            {
                EnemyPathfinding EP = (EnemyPathfinding)Collider.gameObject.GetComponent("EnemyPathfinding");
                EP.Distract(gameObject.transform.position);
            }
        }
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }
}

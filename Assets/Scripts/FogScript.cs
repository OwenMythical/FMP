using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogScript : MonoBehaviour
{
    float X = 0;
    float Y = 0;
    float OX;
    float OY;
    void Start()
    {
        OX = gameObject.transform.position.x;
        OY = gameObject.transform.position.y;
        StartCoroutine(Move());
    }
    IEnumerator Move()
    {
        while (true)
        {
            X += Random.Range(-0.025f, 0.025f);
            Y -= Random.Range(-0.025f, 0.025f);
            if (X < -0.25f)
            {
                X = -0.25f;
            }
            if (Y < -0.25f)
            {
                Y = -0.25f;
            }
            if (X > 0.25f)
            {
                X = 0.25f;
            }
            if (X > 0.25f)
            {
                X = 0.25f;
            }
            gameObject.transform.position = new Vector2(OX + X, OY + Y);
            yield return new WaitForSeconds(Random.Range(0.2f,0.3f));
        }
    }

    bool Vanishing = false;
    public IEnumerator Vanish()
    {
        if (Vanishing == false)
        {
            Vanishing = true;
            SpriteRenderer SR = (SpriteRenderer)gameObject.GetComponent("SpriteRenderer");
            for (int i = 1; i < 10; i++)
            {
                SR.color = new Color(0, 0, 0, SR.color.a - 0.1f);
                yield return new WaitForSeconds(0.01f);
            }
            Destroy(gameObject);
        }
    }
}

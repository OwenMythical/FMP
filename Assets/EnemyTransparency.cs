using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTransparency : MonoBehaviour
{
    public bool Illuminated;
    SpriteRenderer SR;

    public void Start()
    {
        SR = (SpriteRenderer)gameObject.GetComponent("SpriteRenderer");
        SR.color = new Color(1, 1, 1, 0);
    }

    private void Update()
    {
        if (Illuminated == false)
        {
            SR.color = new Color(1, 1, 1, SR.color.a - 0.01f);
            if (SR.color.a < 0)
            {
                SR.color = new Color(1, 1, 1, 0);
            }
        }
        else
        {
            SR.color = new Color(1, 1, 1, SR.color.a + 0.01f);
            if (SR.color.a > 1)
            {
                SR.color = new Color(1, 1, 1, 1);
            }
        }
    }
}

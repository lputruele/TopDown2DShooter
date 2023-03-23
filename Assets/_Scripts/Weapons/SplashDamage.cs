using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SplashDamage : MonoBehaviour
{
    private bool isDead = false;
    [SerializeField]
    public int Damage { get; set; } = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isDead)
        {
            var hittable = collision.GetComponent<IHittable>();
            if (hittable != null)
            {
                hittable.GetHit(Damage, gameObject);
                isDead = true;
            }
            
        }
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(AttackAnimations))]
public class ConjuredFlameAttack : MonoBehaviour
{

    [SerializeField]
    private AttackAnimations attackAnimation;

    [SerializeField]
    private AudioSource flameClip;

    [SerializeField]
    private float firstPhaseDuration = 1f;

    [SerializeField]
    private int damage = 1;

    [SerializeField]
    private Collider2D collider2d;

    private bool isDead;

    void Start()
    {
        collider2d = GetComponent<Collider2D>();
        collider2d.enabled = false;
        StartCoroutine(ActivateAttackCoroutine());
    }

    IEnumerator ActivateAttackCoroutine()
    {
        yield return new WaitForSeconds(firstPhaseDuration);
        attackAnimation.ActivateAnimation(true);
        flameClip.Play();
        collider2d.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isDead)
        {
            var hittable = collision.GetComponent<IHittable>();
            if (hittable != null)
            {
                hittable.GetHit(damage, gameObject);
            }
            isDead = true;
        }
    }

    public void DestroyAfterAnimation()
    {
        Destroy(gameObject);
    }
}

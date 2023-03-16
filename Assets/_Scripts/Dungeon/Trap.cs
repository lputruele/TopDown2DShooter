using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField]
    private int damage = 1;
    [SerializeField]
    private float trapDelay = 2f;

    private bool isActive, hit;

    [SerializeField]
    private Sprite trapActiveSprite, trapInactiveSprite;
    [SerializeField]
    private AudioClip activeClip, spikesOutClip;

    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isActive)
        {
            audioSource.clip = activeClip;
            audioSource.Play();
            StartCoroutine(TrapDelayCoroutine());
        }
        if (isActive)
        {
            audioSource.clip = spikesOutClip;
            audioSource.Play();
            var hittable = collision.GetComponentInParent<IHittable>();
            if (hittable != null)
            {

                hittable.GetHit(damage, gameObject);
            }
            StartCoroutine(TrapDelayCoroutine());
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isActive && !hit)
        {
            var hittable = collision.GetComponentInParent<IHittable>();
            if (hittable != null)
            {
                hittable.GetHit(damage, gameObject);
            }
            StartCoroutine(HitCoroutine());
        }
    }

    IEnumerator TrapDelayCoroutine()
    {
        yield return new WaitForSeconds(trapDelay);
        spriteRenderer.sprite = trapActiveSprite;
        isActive = true;
        audioSource.clip = spikesOutClip;
        audioSource.Play();
        yield return new WaitForSeconds(trapDelay);
        spriteRenderer.sprite = trapInactiveSprite;
        isActive = false;
    }

    IEnumerator HitCoroutine()
    {
        hit = true;
        yield return new WaitForSeconds(trapDelay);
        hit = false;
    }
}

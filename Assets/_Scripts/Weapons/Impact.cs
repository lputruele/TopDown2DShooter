using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impact : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Collider2D collider2d;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2d = GetComponent<Collider2D>();
    }
    public void DestroyAfterAnimation()
    {
        spriteRenderer.enabled = false;
        if (collider2d != null)
            collider2d.enabled = false;
        StartCoroutine(WaitBeforeDestroyCoroutine());
    }

    IEnumerator WaitBeforeDestroyCoroutine()
    {
        yield return new WaitForSeconds(.5f);
        Destroy(gameObject);
    }
}

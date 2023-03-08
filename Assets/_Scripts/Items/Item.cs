using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Item : MonoBehaviour
{
    [field:SerializeField]
    public ResourceDataSO ResourceData { get; set; }

    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PickupItem()
    {
        StartCoroutine(DestroyCoroutine());        
    }

    IEnumerator DestroyCoroutine()
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
        Destroy(gameObject);
    }
}

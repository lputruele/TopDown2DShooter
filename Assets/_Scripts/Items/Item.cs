using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class Item : MonoBehaviour
{
    [field:SerializeField]
    public ResourceDataSO ResourceData { get; set; }

    [field: SerializeField]
    private MessagesUI messagesUI;

    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
         messagesUI = FindObjectOfType<MessagesUI>(); // there should be a better way to do this   
    }

    public void PickupItem()
    {
        messagesUI.ShowMessage(ResourceData.PickupMessage);
        StartCoroutine(DestroyCoroutine());        
    }

    IEnumerator DestroyCoroutine()
    {
        GetComponent<Collider2D>().enabled = false;
        foreach (SpriteRenderer sprite in GetComponentsInChildren<SpriteRenderer>())
        {
            sprite.enabled = false;
        }
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
        Destroy(gameObject);
    }
}

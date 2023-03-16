using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChest : MonoBehaviour
{
    [SerializeField]
    private Sprite openChestSprite;
    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip openClip, lockedClip, unlockedClip;
    private bool opened, tryingToOpen;

    private ItemDropper dropper;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        dropper = GetComponent<ItemDropper>();
    }
    public bool OpenChest(bool canOpen)
    {
        if (canOpen && !opened)
        {            
            opened = true;
            StartCoroutine(OpenChestCoroutine());
            dropper.DropItem();
            return true;
        }
        else
        {
            if (!tryingToOpen && !opened)
            {
                StartCoroutine(LockedChestCoroutine());
            }
            return false;
        }
    }

    IEnumerator OpenChestCoroutine()
    {
        if (unlockedClip != null)
        {
            audioSource.clip = unlockedClip;
            audioSource.Play();
        }
        yield return new WaitForSeconds(.02f);
        if (openClip != null)
        {
            audioSource.clip = openClip;
            audioSource.Play();
        }
        spriteRenderer.sprite = openChestSprite;
    }

    IEnumerator LockedChestCoroutine()
    {
        tryingToOpen = true;
        if (lockedClip != null)
        {
            audioSource.clip = lockedClip;
            audioSource.Play();
        }
        yield return new WaitForSeconds(.1f);
        tryingToOpen = false;
    }
}

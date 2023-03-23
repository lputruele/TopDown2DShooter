using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GracePeriodFeedback : Feedback
{
    [SerializeField]
    private SpriteRenderer spriteRenderer = null;
    [SerializeField]
    private float flashTime = 1.5f;
    [SerializeField]
    private Material flashMaterial = null;

    private Shader originalMaterialShader = null;

    private void Start()
    {
        originalMaterialShader = spriteRenderer.material.shader;
    }
    public override void CompletePreviousFeedback()
    {
        StopAllCoroutines();
        spriteRenderer.material.shader = originalMaterialShader;
    }

    public override void CreateFeedback()
    {
        if (!spriteRenderer.material.HasProperty("_MakeGhostColor"))
        {
            spriteRenderer.material.shader = flashMaterial.shader;
        }
        spriteRenderer.material.SetInt("_MakeGhostColor", 1);
        StartCoroutine(WaitBeforeChangingBack());
    }

    IEnumerator WaitBeforeChangingBack()
    {
        yield return new WaitForSeconds(flashTime);
        if (spriteRenderer.material.HasProperty("_MakeGhostColor"))
        {
            spriteRenderer.material.SetInt("_MakeGhostColor", 0);
        }
        else
        {
            spriteRenderer.material.shader = originalMaterialShader;
        }
    }
}

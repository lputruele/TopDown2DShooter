using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlashLightFeedback : Feedback
{
    [SerializeField]
    private Light2D lightTarget = null;

    [SerializeField]
    private float lightOnDelay = 0.01f, lightOffDelay = 0.01f;

    public override void CompletePreviousFeedback()
    {
        StopAllCoroutines();
        lightTarget.enabled = false;
    }

    public override void CreateFeedback()
    {
        StartCoroutine(ToggleLightCoroutine(lightOnDelay,true,() => StartCoroutine(ToggleLightCoroutine(lightOffDelay, false))));
    }

    IEnumerator ToggleLightCoroutine(float time, bool result, Action FinishCallback = null)
    {
        yield return new WaitForSeconds(time);
        lightTarget.enabled = result;
        FinishCallback?.Invoke();
    }
}

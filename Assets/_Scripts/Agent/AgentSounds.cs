using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentSounds : AudioPlayer
{
    [SerializeField]
    private AudioClip hitClip = null, deathClip = null, voiceLineCLip = null;

    public void PlayHitSound()
    {
        PlayClipWithVariablePitch(hitClip);
    }

    public void PlayDeathSound()
    {
        PlayClip(deathClip);
    }

    public void PlayVoiceSound()
    {
        PlayClipWithVariablePitch(voiceLineCLip);
    }

}

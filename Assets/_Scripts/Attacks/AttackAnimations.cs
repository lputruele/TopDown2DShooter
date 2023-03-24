using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AttackAnimations : MonoBehaviour
{
    protected Animator attackAnimator;

    private void Awake()
    {
        attackAnimator = GetComponent<Animator>();
    }

    public void ActivateAnimation(bool val)
    {
        attackAnimator.SetBool("Activate", val);
    }
}

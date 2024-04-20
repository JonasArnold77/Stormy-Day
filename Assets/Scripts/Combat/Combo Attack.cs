using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboAttack : MonoBehaviour
{

    private Animator _Animator;
    private RuntimeAnimatorController _AnimatorController;
    private void Start()
    {
        _Animator = GetComponent<Animator>();

        _AnimatorController = _Animator.runtimeAnimatorController;    //Get Animator controller
    }

    private AnimationClip CheckForRunningAnimationClip()
    {
        foreach (AnimationClip ac in _AnimatorController.animationClips)
        {
            if (_Animator.GetCurrentAnimatorStateInfo(1).IsName(ac.name))
            {
                return ac;
            }
        }

        return null;
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationDoll : MonoBehaviour
{
    public Animator _Animator;
    // Start is called before the first frame update
    void Start()
    {
        _Animator = GetComponent<Animator>();
        _Animator.Play("Sword Attack 1",0);
    }

}

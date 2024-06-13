using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class AbilityLootObject : LootObject
{
    public AttackItem _AttackItem;
    private Animator _animator;

    public AnimatorController Controller;

    public void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        var newAnim = Instantiate(_AttackItem._Animation);

        Controller.AddMotion(_AttackItem._Animation);

        _animator.Play(_AttackItem._Animation.name);
    }

    private void Update()
    {
        _animator.Play(_AttackItem._Animation.name);
    }
}

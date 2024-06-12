using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityLootObject : LootObject
{
    public AttackItem _AttackItem;
    private Animator _animator;

    public void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _animator.Play(_AttackItem._Animation.name);
    }

    private void Update()
    {
        _animator.Play(_AttackItem._Animation.name);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityLootObject : LootObject
{
    public AttackItem _AttackItem;
    private Animator _animator;

    public Transform AnimationObject;

    public RuntimeAnimatorController Controller;

    public void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        var newAnim = Instantiate(_AttackItem._Animation);

        //Controller.AddMotion(_AttackItem._Animation);

        SetAnimation(_animator, _animator.runtimeAnimatorController, _AttackItem._Animation);

        _animator.Play(_AttackItem._Animation.name);
    }

    private void SetAnimation(Animator animator, RuntimeAnimatorController originalController, AnimationClip newAnimationClip)
    {
        if (animator == null || originalController == null || newAnimationClip == null)
        {
            Debug.LogError("Animator, originalController oder newAnimationClip ist nicht zugewiesen.");
            return;
        }

        // Erstellen eines neuen AnimatorOverrideControllers
        AnimatorOverrideController overrideController = new AnimatorOverrideController();
        overrideController.runtimeAnimatorController = originalController;

        // Holen Sie sich die Animationen aus dem ursprünglichen Controller
        var animations = new List<KeyValuePair<AnimationClip, AnimationClip>>(overrideController.overridesCount);
        overrideController.GetOverrides(animations);

        // Durch die Animationen gehen und eine spezifische Animation überschreiben oder hinzufügen
        for (int i = 0; i < animations.Count; i++)
        {
            if (animations[i].Key.name == "OldAnimationName") // Der Name der Animation, die überschrieben werden soll
            {
                animations[i] = new KeyValuePair<AnimationClip, AnimationClip>(animations[i].Key, newAnimationClip);
            }
        }

        // Die geänderten Animationen anwenden
        overrideController.ApplyOverrides(animations);

        // Setzen des neuen Override Controllers
        animator.runtimeAnimatorController = overrideController;
    }

    private void Update()
    {
        _animator.Play(_AttackItem._Animation.name);
    }
}

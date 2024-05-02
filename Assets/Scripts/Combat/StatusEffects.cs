using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffects : MonoBehaviour
{
    public GameObject FireSword;
    public float StatusEffectDuration = 4;
    private bool EffectIsActive;

    public EStatusEffects ActualHitEffect;

    private void Start()
    {
        DeactivateEffect();
    }

    private void Update()
    {
        if (!EffectIsActive)
        {
            if (Input.GetKeyDown((KeyCode)InputManager.Instance.GetInputActionFromControlInput(EControls.Effect1)))
            {
                StartCoroutine(DoEffectOverTime(EStatusEffects.Fire));
                ActualHitEffect = EStatusEffects.Fire;
            }
            else if (Input.GetKeyDown((KeyCode)InputManager.Instance.GetInputActionFromControlInput(EControls.Effect2)))
            {
                StartCoroutine(DoEffectOverTime(EStatusEffects.Ice));
                ActualHitEffect = EStatusEffects.Ice;
            }
        }
    }

    public void ActivateEffect(EStatusEffects effect, EHitType type = EHitType.Sword)
    {
        EffectIsActive = true;
        if (effect == EStatusEffects.Fire)
        {
            WeaponManager.Instance.ActualWeapon.FireEffect.SetActive(true);
        }
        else if (effect == EStatusEffects.Ice)
        {
            WeaponManager.Instance.ActualWeapon.IceEffect.SetActive(true);
        }
    }

    public IEnumerator DoEffectOverTime(EStatusEffects effect)
    {
        EffectIsActive = true;
        if (effect == EStatusEffects.Fire)
        {
            WeaponManager.Instance.ActualWeapon.FireEffect.SetActive(true);
        }
        else if (effect == EStatusEffects.Ice)
        {
            WeaponManager.Instance.ActualWeapon.IceEffect.SetActive(true);
        }

        yield return new WaitForSeconds(4);

        DeactivateEffect();
    }

    public void DeactivateEffect()
    {
        EffectIsActive = false;
        ActualHitEffect = EStatusEffects.None;
        WeaponManager.Instance.ActualWeapon.FireEffect.SetActive(false);
        WeaponManager.Instance.ActualWeapon.IceEffect.SetActive(false);
    }
}

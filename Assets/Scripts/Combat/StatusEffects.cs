using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffects : MonoBehaviour
{
    public GameObject FireSword;


    private void Start()
    {
        DeactivateEffect();
    }

    public void ActivateEffect(EStatusEffects effect, EHitType type)
    {
        if(effect == EStatusEffects.Fire)
        {
            WeaponManager.Instance.ActualWeapon.FireEffect.SetActive(true);
        }
        else if (effect == EStatusEffects.Ice)
        {
            WeaponManager.Instance.ActualWeapon.IceEffect.SetActive(true);
        }
    }

    public void DeactivateEffect()
    {
        WeaponManager.Instance.ActualWeapon.FireEffect.SetActive(false);
        WeaponManager.Instance.ActualWeapon.IceEffect.SetActive(false);
    }
}

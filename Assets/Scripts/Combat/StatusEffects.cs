using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffects : MonoBehaviour
{
    public GameObject FireSword;
    public float StatusEffectDuration = 4;
    private bool EffectIsActive;

    private Coroutine EffectCoroutine;

    public EStatusEffects ActualHitEffect;

    private void Start()
    {
        DeactivateEffect();
    }

    private void Update()
    {
        if (WeaponManager.Instance.ActualWeapon.FireEffect.activeSelf)
        {
            HealthAndEndurancePanel.Instance.FireSymbol.color = Color.yellow;
            HealthAndEndurancePanel.Instance.IceSymbol.color = Color.white;
        }
        else if (WeaponManager.Instance.ActualWeapon.IceEffect.activeSelf)
        {
            HealthAndEndurancePanel.Instance.FireSymbol.color = Color.white;
            HealthAndEndurancePanel.Instance.IceSymbol.color = Color.yellow;
        }
        else
        {
            HealthAndEndurancePanel.Instance.FireSymbol.color = Color.white;
            HealthAndEndurancePanel.Instance.IceSymbol.color = Color.white;
        }

        if (!EffectIsActive && GetComponent<PlayerStatus>().ActualMana > 0)
        {
            if (Input.GetKeyDown((KeyCode)InputManager.Instance.GetInputActionFromControlInput(EControls.Effect1)))
            {
                EffectCoroutine = StartCoroutine(DoEffectOverTime(EStatusEffects.Fire));
                ActualHitEffect = EStatusEffects.Fire;
            }
            else if (Input.GetKeyDown((KeyCode)InputManager.Instance.GetInputActionFromControlInput(EControls.Effect2)))
            {
                EffectCoroutine = StartCoroutine(DoEffectOverTime(EStatusEffects.Ice));
                ActualHitEffect = EStatusEffects.Ice;
            }

            return;
        }

        //Stop Effect
        if(EffectIsActive && WeaponManager.Instance.ActualWeapon.FireEffect.activeSelf && Input.GetKeyDown((KeyCode)InputManager.Instance.GetInputActionFromControlInput(EControls.Effect1)))
        {
            StopCoroutine(EffectCoroutine);
            DeactivateEffect();
        }
        else if (WeaponManager.Instance.ActualWeapon.IceEffect.activeSelf && Input.GetKeyDown((KeyCode)InputManager.Instance.GetInputActionFromControlInput(EControls.Effect2)))
        {
            StopCoroutine(EffectCoroutine);
            DeactivateEffect();
        }

        //Switch Effect
        if (EffectIsActive && WeaponManager.Instance.ActualWeapon.FireEffect.activeSelf && Input.GetKeyDown((KeyCode)InputManager.Instance.GetInputActionFromControlInput(EControls.Effect2)))
        {
            StopCoroutine(EffectCoroutine);
            DeactivateEffect();
            EffectCoroutine = StartCoroutine(DoEffectOverTime(EStatusEffects.Ice));
            ActualHitEffect = EStatusEffects.Ice;
        }
        else if (WeaponManager.Instance.ActualWeapon.IceEffect.activeSelf && Input.GetKeyDown((KeyCode)InputManager.Instance.GetInputActionFromControlInput(EControls.Effect1)))
        {
            StopCoroutine(EffectCoroutine);
            DeactivateEffect();
            EffectCoroutine = StartCoroutine(DoEffectOverTime(EStatusEffects.Fire));
            ActualHitEffect = EStatusEffects.Fire;
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

        while (GetComponent<PlayerStatus>().ActualMana >= 0)
        {
            GetComponent<PlayerStatus>().ChangeMana(-10);
            yield return new WaitForSeconds(0.5f);
        }

        //yield return new WaitForSeconds(4);

        DeactivateEffect();
    }

    //public IEnumerator DoBloodEffectForSeconds()
    //{
    //    WeaponManager.Instance.ActualWeapon.EnemyBloodEffect.SetActive(true);
    //    yield return new WaitForSeconds(0.2f);
    //    WeaponManager.Instance.ActualWeapon.EnemyBloodEffect.SetActive(false);
    //}

    public void DeactivateEffect()
    {
        EffectIsActive = false;
        ActualHitEffect = EStatusEffects.None;
        WeaponManager.Instance.ActualWeapon.FireEffect.SetActive(false);
        WeaponManager.Instance.ActualWeapon.IceEffect.SetActive(false);
        //WeaponManager.Instance.ActualWeapon.EnemyBloodEffect.SetActive(false);
    }
}

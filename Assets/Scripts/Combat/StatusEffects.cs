using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffects : MonoBehaviour
{
    public GameObject FireSword;
    public float StatusEffectDuration = 4;
    private bool EffectIsActive;

    public bool IsDoingAttackItemEffect;

    private Coroutine EffectCoroutine;

    public EStatusEffects ActualHitEffect;

    private void Start()
    {
        DeactivateEffect();
    }

    private void Update()
    {
        if (!EffectIsActive && FindObjectOfType<PlayerAnimation>().ActualAttackItem != null && FindObjectOfType<PlayerAnimation>().ActualAttackItem.StatusEffect == EStatusEffects.Fire)
        {
            WeaponManager.Instance.ActualWeapon.FireEffect.SetActive(true);
            WeaponManager.Instance.ActualWeapon.IceEffect.SetActive(false);
            ActualHitEffect = EStatusEffects.Fire;
            IsDoingAttackItemEffect = true;
            EffectIsActive = true;
        }
        else if (!EffectIsActive && FindObjectOfType<PlayerAnimation>().ActualAttackItem != null && FindObjectOfType<PlayerAnimation>().ActualAttackItem.StatusEffect == EStatusEffects.Ice)
        {
            WeaponManager.Instance.ActualWeapon.FireEffect.SetActive(false);
            WeaponManager.Instance.ActualWeapon.IceEffect.SetActive(true);
            ActualHitEffect = EStatusEffects.Ice;
            IsDoingAttackItemEffect = true;
            EffectIsActive = true;
        }
        else if (IsDoingAttackItemEffect && FindObjectOfType<PlayerAnimation>().ActualAttackItem == null || FindObjectOfType<PlayerAnimation>().ActualAttackItem?.StatusEffect == EStatusEffects.None)
        {
            WeaponManager.Instance.ActualWeapon.FireEffect.SetActive(false);
            WeaponManager.Instance.ActualWeapon.IceEffect.SetActive(false);
            ActualHitEffect = EStatusEffects.None;
            IsDoingAttackItemEffect = false;
            EffectIsActive = false;
        }

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
            EffectIsActive = false;
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
        if(EffectCoroutine != null && EffectIsActive && WeaponManager.Instance.ActualWeapon.FireEffect.activeSelf && Input.GetKeyDown((KeyCode)InputManager.Instance.GetInputActionFromControlInput(EControls.Effect1)))
        {
            StopCoroutine(EffectCoroutine);
            DeactivateEffect();
        }
        else if (EffectCoroutine != null && EffectIsActive && WeaponManager.Instance.ActualWeapon.IceEffect.activeSelf && Input.GetKeyDown((KeyCode)InputManager.Instance.GetInputActionFromControlInput(EControls.Effect2)))
        {
            StopCoroutine(EffectCoroutine);
            DeactivateEffect();
        }

        //Switch Effect
        if (EffectIsActive && !IsDoingAttackItemEffect && WeaponManager.Instance.ActualWeapon.FireEffect.activeSelf && Input.GetKeyDown((KeyCode)InputManager.Instance.GetInputActionFromControlInput(EControls.Effect2)))
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
            WeaponManager.Instance.ActualWeapon.IceEffect.SetActive(false);
            ActualHitEffect = EStatusEffects.Fire;
            EffectIsActive = true;
        }
        else if (effect == EStatusEffects.Ice)
        {
            WeaponManager.Instance.ActualWeapon.FireEffect.SetActive(false);
            WeaponManager.Instance.ActualWeapon.IceEffect.SetActive(true);
            ActualHitEffect = EStatusEffects.Ice;
            EffectIsActive = true;
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
            //if (IsDoingAttackItemEffect)
            //{
                yield return new WaitUntil(() => !IsDoingAttackItemEffect);
                if (effect == EStatusEffects.Fire)
                {
                    WeaponManager.Instance.ActualWeapon.FireEffect.SetActive(true);
                    WeaponManager.Instance.ActualWeapon.IceEffect.SetActive(false);
                }
                else if (effect == EStatusEffects.Ice)
                {
                    WeaponManager.Instance.ActualWeapon.IceEffect.SetActive(true);
                    WeaponManager.Instance.ActualWeapon.FireEffect.SetActive(false);
            }
            //}
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

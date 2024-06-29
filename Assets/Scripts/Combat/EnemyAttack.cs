using Magio;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour
{
    public bool IsDoingAttack;
    private NavMeshAgent Agent;

    public List<EnemyWeapon> Weapons = new List<EnemyWeapon>();

    public EnemyAnimation _EnemyAnimation;

    public AudioSource _AudioSource;
    public AudioClip SwingAudioClip;

    private void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        Weapons.ForEach(w => w.GetComponent<Collider>().enabled = false);
        _EnemyAnimation = GetComponent<EnemyAnimation>();
        _AudioSource = GetComponent<AudioSource>();
        _AudioSource.loop = false;
    }

    private void Update()
    {
        if (!IsDoingAttack && !GetComponent<EnemyAnimation>().MagioEffect.magioObjects.Where(m => m.effectClass == EffectClass.Ice).FirstOrDefault().enabled)
        {
            Agent.speed = _EnemyAnimation.speed;
        }
    }

    public void ActivateWeaponCollider(EHitType type)
    {
        Weapons.Where(w => w.WeaponType == type).ToList().ForEach(w2 => w2.GetComponent<Collider>().enabled = true);
        _AudioSource.Stop();
        _AudioSource.clip = SwingAudioClip;
        _AudioSource.Play();
    }

    public void DeactivateAllWeaponColliders()
    {
        Weapons.ForEach(w => w.GetComponent<Collider>().enabled = false);
    }

    public void SetIsDoingAttackTrue()
    {
        IsDoingAttack = true;
    }

    public void SetIsDoingAttackFalse()
    {
        IsDoingAttack = false;
    }

    public void SetSpeed(float speed)
    {
        Agent.speed = speed;
    }
}

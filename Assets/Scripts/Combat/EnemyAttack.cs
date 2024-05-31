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

    private void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        Weapons.ForEach(w => w.GetComponent<Collider>().enabled = false);
    }

    private void Update()
    {
       
    }

    


    public void ActivateWeaponCollider(EHitType type)
    {
        Weapons.Where(w => w.WeaponType == type).ToList().ForEach(w2 => w2.GetComponent<Collider>().enabled = true);
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

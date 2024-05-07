using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour
{
    public bool IsDoingAttack;
    private NavMeshAgent Agent;

    private void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        
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

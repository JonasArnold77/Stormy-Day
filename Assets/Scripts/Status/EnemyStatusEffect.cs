using Magio;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStatusEffect : MonoBehaviour
{
    public int TotalFireAmount;
    public int ActualFireAmount;

    public int TotalIceAmount;
    public int ActualIceAmount;

    public NavMeshAgent Agent;
    public Animator _Animator;

    public Coroutine CurrentCoroutine;

    private void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        _Animator = GetComponent<Animator>();
    }

    public void IncreaseFireAmount(int amount)
    {
        ActualFireAmount = ActualFireAmount + amount;

        if(ActualFireAmount >= TotalFireAmount)
        {
            ActualFireAmount = TotalFireAmount;
            GetComponent<EnemyAnimation>().MagioEffect.magioObjects.Where(m => m.effectClass == EffectClass.Flame).FirstOrDefault().enabled = true;
            GetComponent<EnemyAnimation>().MagioEffect.magioObjects.Where(m => m.effectClass == EffectClass.Ice).FirstOrDefault().enabled = false;

            StartCoroutine(DoFireOverTimeCoroutine());
        }

        UIManager.Instance._EnemyStatusPanel.SetActualFireAmount((float)((float)ActualFireAmount/(float)TotalFireAmount));

        UIManager.Instance._EnemyStatusPanel.IceWindow.SetActive(false);
        UIManager.Instance._EnemyStatusPanel.FireWindow.SetActive(true);

        StartCoroutine(OpenPanelForSeconds());
    }

    public void IncreaseIceAmount(int amount)
    {
        ActualIceAmount = ActualIceAmount + amount;

        if (ActualIceAmount >= TotalIceAmount)
        {
            ActualIceAmount = TotalIceAmount;
            GetComponent<EnemyAnimation>().MagioEffect.magioObjects.Where(m => m.effectClass == EffectClass.Flame).FirstOrDefault().enabled = false;
            GetComponent<EnemyAnimation>().MagioEffect.magioObjects.Where(m => m.effectClass == EffectClass.Ice).FirstOrDefault().enabled = true;


            StartCoroutine(DoIceOverTimeCoroutine());
        }

        UIManager.Instance._EnemyStatusPanel.SetActualIceAmount((float)((float)ActualIceAmount / (float)TotalIceAmount));

        UIManager.Instance._EnemyStatusPanel.IceWindow.SetActive(true);
        UIManager.Instance._EnemyStatusPanel.FireWindow.SetActive(false);

        StartCoroutine(OpenPanelForSeconds());
    }

    public IEnumerator DoIceOverTimeCoroutine()
    {
        _Animator.speed = 0.25f;
        Agent.speed = 0.5f;
        yield return new WaitForSeconds(5);

        ActualIceAmount = 0;

        _Animator.speed = 1f;
        Agent.speed = GetComponent<EnemyAnimation>().speed;

        GetComponent<EnemyAnimation>().MagioEffect.magioObjects.Where(m => m.effectClass == EffectClass.Ice).FirstOrDefault().enabled = false;
    }

    public IEnumerator DoFireOverTimeCoroutine()
    {
        for(int i = 0; i<4; i++)
        {
            yield return new WaitForSeconds(1);
            GetComponent<EnemyHealth>().Health -= 20;
        }

        ActualFireAmount = 0;

        GetComponent<EnemyAnimation>().MagioEffect.magioObjects.Where(m => m.effectClass == EffectClass.Flame).FirstOrDefault().enabled = false;
    }

    public IEnumerator OpenPanelForSeconds()
    {
        UIManager.Instance._EnemyStatusPanel.gameObject.SetActive(true);
        yield return new WaitForSeconds(4);
        UIManager.Instance._EnemyStatusPanel.gameObject.SetActive(false);
    }
}

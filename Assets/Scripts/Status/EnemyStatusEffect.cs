using Magio;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyStatusEffect : MonoBehaviour
{
    public int TotalFireAmount;
    public int ActualFireAmount;

    public Coroutine CurrentCoroutine;


    public void IncreaseFireAmount(int amount)
    {
        ActualFireAmount = ActualFireAmount + amount;

        if(ActualFireAmount >= TotalFireAmount)
        {
            ActualFireAmount = TotalFireAmount;
            GetComponent<EnemyAnimation>().MagioEffect.magioObjects.Where(m => m.effectClass == EffectClass.Flame).FirstOrDefault().enabled = true;
            GetComponent<EnemyAnimation>().MagioEffect.magioObjects.Where(m => m.effectClass == EffectClass.Ice).FirstOrDefault().enabled = false;
        }

        EnemyStatusPanel.Instance.SetActualFireAmount((float)((float)ActualFireAmount/(float)TotalFireAmount));

        StartCoroutine(OpenPanelForSeconds());
    }

    public IEnumerator OpenPanelForSeconds()
    {
        UIManager.Instance._EnemyStatusPanel.gameObject.SetActive(true);
        yield return new WaitForSeconds(4);
        UIManager.Instance._EnemyStatusPanel.gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public int TotalHealth;
    public int ActualHealth;
    public int TotalEndurance;
    public int ActualEndurance;

    private void Start()
    {
        ActualHealth = TotalHealth;
        StartCoroutine(EnduranceCoroutine());
    }

    private IEnumerator EnduranceCoroutine()
    {
        yield return new WaitUntil(() => ActualEndurance < TotalEndurance);

        while(ActualEndurance <= TotalEndurance)
        {
            yield return new WaitUntil(() => !GetComponent<Attack>().isDoingAttack);
            yield return new WaitForSeconds(0.25f);
            ChangeEndurance(4);
        }

        ActualEndurance = TotalEndurance;
        HealthAndEndurancePanel.Instance.SetActualEndurance(1);
        StartCoroutine(EnduranceCoroutine());
    }

    public void ChangeHealth(int amount)
    {
        ActualHealth = ActualHealth + amount;
        HealthAndEndurancePanel.Instance.SetActualHealth((float)((float)ActualHealth/(float)TotalHealth));
    }

    public void ChangeEndurance(int amount)
    {
        ActualEndurance = ActualEndurance + amount;
        HealthAndEndurancePanel.Instance.SetActualEndurance((float)((float)ActualEndurance / (float)TotalEndurance));
    }
}

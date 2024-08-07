using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    public int TotalHealth;
    public int ActualHealth;
    public int TotalEndurance;
    public int ActualEndurance;
    public int ActualMana;
    public int TotalMana;

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

        if (ActualHealth <= 0)
        {
            StartCoroutine(DoDeathOfPlayer());
            GetComponent<Attack>().playerTransform.position = new Vector3(382f, 149f, -211f);
        }

        HealthAndEndurancePanel.Instance.SetActualHealth((float)((float)ActualHealth/(float)TotalHealth));
    }

    public void ChangeEndurance(int amount)
    {
        ActualEndurance = ActualEndurance + amount;
        HealthAndEndurancePanel.Instance.SetActualEndurance((float)((float)ActualEndurance / (float)TotalEndurance));
    }

    public void ChangeMana(int amount)
    {
        ActualMana = ActualMana + amount;
        if(ActualMana + amount > TotalMana)
        {
            ActualMana = TotalMana;
        }
        HealthAndEndurancePanel.Instance.SetActualMana((float)((float)ActualMana / (float)TotalMana));
    }

    private IEnumerator DoDeathOfPlayer()
    {
        
        FindObjectsOfType<FollowPalyer>().ToList().ForEach(e => e.transform.position = e.OriginalPosition);

        var o = FindObjectsOfType<FollowPalyer>().ToList();

        ActualHealth = TotalHealth;
        ActualEndurance = TotalEndurance;
        ActualMana = TotalMana;

        UIManager.Instance._DarknessPanel.gameObject.SetActive(true);
        UIManager.Instance._DarknessPanel.GetComponent<Image>().color = new Color(UIManager.Instance._DarknessPanel.GetComponent<Image>().color.r, UIManager.Instance._DarknessPanel.GetComponent<Image>().color.g, UIManager.Instance._DarknessPanel.GetComponent<Image>().color.b, 1f);

        FindObjectOfType<ThirdPersonController>().MoveSpeed = 0;

        yield return new WaitForSeconds(3);

        FindObjectOfType<ThirdPersonController>().MoveSpeed = 6;

        UIManager.Instance._DarknessPanel.GetComponent<Image>().color = new Color(UIManager.Instance._DarknessPanel.GetComponent<Image>().color.r, UIManager.Instance._DarknessPanel.GetComponent<Image>().color.g, UIManager.Instance._DarknessPanel.GetComponent<Image>().color.b, 0f);
        UIManager.Instance._DarknessPanel.gameObject.SetActive(false);

        GetComponent<Attack>().playerTransform.position = new Vector3(382f, 149f, -211f);
        FindObjectOfType<CameraFollow>().SetPosition();
    }
}

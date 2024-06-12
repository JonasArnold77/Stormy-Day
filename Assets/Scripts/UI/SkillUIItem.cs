using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillUIItem : MonoBehaviour
{
    public AttackItem _AttackItem;
    public Image Image;
    public bool IsInPool;

    private void Start()
    {
        if (!IsInPool)
        {
            GetComponent<Button>().onClick.AddListener(() => OpenAttackPool(EHitType.Sword));
        }

        if(_AttackItem == null)
        {

        }
    }


    
    private void OpenAttackPool(EHitType type)
    {
        FindObjectOfType<AttackPoolMenu>().GetComponent<CanvasGroup>().alpha = 1;
        FindObjectOfType<AttackPoolMenu>().GetComponent<CanvasGroup>().blocksRaycasts = true;
        FindObjectOfType<ComboPanel>().GetComponent<CanvasGroup>().alpha = 0;
        FindObjectOfType<ComboPanel>().GetComponent<CanvasGroup>().blocksRaycasts = false;
        FindObjectOfType<AttackPoolMenu>().InitializeMenu(type);
        FindObjectOfType<AttackPoolMenu>().AttackComboItem = this;
    }

    public void SetAttackItem(AttackItem item)
    {
        _AttackItem = item;
        Image.sprite = _AttackItem.Image;
        GetComponentInChildren<TMP_Text>().text = _AttackItem.name;

    }
}

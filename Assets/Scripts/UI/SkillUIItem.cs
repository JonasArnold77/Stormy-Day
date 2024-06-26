using Kamgam.UGUIWorldImage;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillUIItem : MonoBehaviour
{
    public AttackItem _AttackItem;
    public Image _Image;
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
        UIManager.Instance._AttackPoolMenu.gameObject.SetActive(true);
        UIManager.Instance._ComboPanel.gameObject.SetActive(false);
        UIManager.Instance._AttackPoolMenu.InitializeMenu(type);
        UIManager.Instance._AttackPoolMenu.AttackComboItem = this;
    }

    public void SetAttackItem(AttackItem item)
    {
        _AttackItem = item;
        _Image.gameObject.SetActive(false);
        GetComponent<WorldImage>().m_worldObjects.Clear();
        GetComponent<WorldImage>().m_worldObjects.Add(item.AnimationObject);
        GetComponentInChildren<TMP_Text>().text = "";
    }
}

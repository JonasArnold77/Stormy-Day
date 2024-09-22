using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicUiItem : MonoBehaviour
{
    public MagicItem _MagicItem;
    public Image _Image;
    public bool IsInPool;

    public bool IsSelected;
    public GameObject OutlineGO;

    private void Start()
    {
        if (!IsInPool)
        {
            GetComponent<Button>().onClick.AddListener(() => OpenAttackPool());
        }
    }

    private void OpenAttackPool()
    {
        UIManager.Instance._MagicPool.gameObject.SetActive(true);
        UIManager.Instance._ComboPanel.gameObject.SetActive(false);
        UIManager.Instance._MagicPool.InitializeMenu();
        UIManager.Instance._MagicPool.MagicItem = this;
    }
     
    public void SetAttackItem(MagicItem item)
    {
        _MagicItem = item;
        //_Image.gameObject.SetActive(false);
        GetComponentInChildren<Image>().sprite = item.Image;
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanel : MonoBehaviour
{
    private CanvasGroup _canvasGroup;
    public RectTransform Content;


    public static InventoryPanel Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        DestroyLastElements();
        LoadAllItems();
    }

    private void DestroyLastElements()
    {
        foreach (Transform child in Content)
        {
            Destroy(child);
        }
    }

    private void LoadAllItems()
    {
        foreach (var a in InventoryManager.Instance.AllArmors)
        {
            var button = Instantiate(PrefabManager.Instance.ArmorUiItem, Content);
            button.GetComponentInChildren<TMP_Text>().text = a.ArmorName;
            button.GetComponent<Image>().sprite = a._Image;

            button.GetComponent<Button>().onClick.AddListener(() => InventoryManager.Instance.SetArmor(a));
        }
    }
}

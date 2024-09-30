using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanel : MonoBehaviour
{
    private CanvasGroup _canvasGroup;
    public RectTransform BreastArmorContent;
    public RectTransform LegArmorContent;
    public RectTransform WeaponArmorContent;

    public Color EquippedColor;
    public Color NotEquippedColor;

    public bool IsInInventoryMode;

    public List<RectTransform> ButtonList = new List<RectTransform>();

    public static InventoryPanel Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        DestroyLastElements();
        LoadAllItems();
    }

    private void Update()
    {
        if (IsInInventoryMode)
        {
            foreach (var b in ButtonList)
            {
                if (Equipment.Instance.ArmorParts.Where(a1 => a1.name == b.GetComponent<ArmorUIItem>().ArmorName).ToList().FirstOrDefault().IsEquipped)
                {
                    b.GetComponent<Image>().color = EquippedColor;
                }
                else
                {
                    b.GetComponent<Image>().color = NotEquippedColor;
                }
            }
        }
    }

    private void OnEnable()
    {
        IsInInventoryMode = true;
        DestroyLastElements();
        LoadAllItems();
    }

    private void OnDisable()
    {
        ButtonList.Clear();
        IsInInventoryMode = false;
    }

    private void DestroyLastElements()
    {
        foreach (Transform child in BreastArmorContent)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in LegArmorContent)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in WeaponArmorContent)
        {
            Destroy(child.gameObject);
        }
    }

    private void LoadAllItems()
    {
        foreach (var a in InventoryManager.Instance.AllArmors)  
        {
            RectTransform button = new RectTransform();
            
            if(a.ArmorType == EArmorType.Body)
            {
                button = Instantiate(PrefabManager.Instance.ArmorUiItem, BreastArmorContent);
            }
            else if(a.ArmorType == EArmorType.Legs)
            {
                button = Instantiate(PrefabManager.Instance.ArmorUiItem, LegArmorContent);
            }
            else if (a.ArmorType == EArmorType.Sword)
            {
                button = Instantiate(PrefabManager.Instance.ArmorUiItem, WeaponArmorContent);
            }

            button.GetComponentInChildren<TMP_Text>().text = a.ArmorName;
            button.GetComponent<Image>().sprite = a._Image;

            button.GetComponent<ArmorUIItem>().ArmorName = a.ArmorName;

            ButtonList.Add(button);

            button.GetComponent<Button>().onClick.AddListener(() => InventoryManager.Instance.SetArmor(a));
        }
    }
}

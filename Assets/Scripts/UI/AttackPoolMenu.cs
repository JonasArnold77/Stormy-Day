using Kamgam.UGUIWorldImage;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class AttackPoolMenu : MonoBehaviour
{
    public GameObject AttackPoolItem;
    public Transform Content;

    public GameObject _AttackItemGO;

    public SkillUIItem AttackComboItem;

    public Button CloseButton;

    public Transform DetailInfoPanel;
    public Button ApplyButton;

    public List<GameObject> allButtons = new List<GameObject>();
    public Button ActualButton;

    public static AttackPoolMenu Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        DetailInfoPanel.gameObject.SetActive(false);
    }

    public void InitializeMenu(EHitType type)
    {
        List<AttackItem> myScriptableObjects = InventoryManager.Instance.AllSkills.Select(s => s.GetComponent<AbilityLootObject>()._AttackItem).ToList();
        //List<AttackItem> myScriptableObjects = GetScriptableObjectsOfType<AttackItem>();

        foreach (Transform child in Content)
        {
            // Zerstöre das Kind-GameObject
            Destroy(child.gameObject);
        }

        foreach (var s in myScriptableObjects)
        {
            if(s.Type == type)
            {
                var item = Instantiate(AttackPoolItem, Content);
                allButtons.Add(item);

                item.GetComponent<SkillUIItem>().IsSelected = false;
                item.GetComponent<SkillUIItem>().OutlineGO.SetActive(false);

                item.GetComponentInChildren<TMP_Text>().text = "";
                //item.GetComponent<Image>().sprite = s.Image;
                item.GetComponent<Button>().onClick.AddListener(() => ChooseButton(item.GetComponent<Button>(), item));
                item.GetComponent<WorldImage>().m_worldObjects.Add(s.AnimationObject);
            }
        }
    }

    public void ChooseButton(Button button, GameObject item)
    {
        ActualButton = button;
        ActualButton.GetComponent<SkillUIItem>().IsSelected = true;
        ActualButton.GetComponent<SkillUIItem>().OutlineGO.SetActive(true);

        _AttackItemGO = item;
        DetailInfoPanel.gameObject.SetActive(true);

        allButtons.Where(b => !item.Equals(b)).ToList().ForEach(b1 => b1.GetComponent<SkillUIItem>().IsSelected = false);
        allButtons.Where(b => !item.Equals(b)).ToList().ForEach(b1 => b1.GetComponent<SkillUIItem>().OutlineGO.SetActive(false));
    }

    public void SetComboItem()
    {
        SetComboAttackItem(_AttackItemGO.GetComponent<SkillUIItem>()._AttackItem);
    }

    public void SetComboAttackItem(AttackItem item)
    {
        AttackComboItem.SetAttackItem(item);
        gameObject.SetActive(false);

        UIManager.Instance._MainMenu.gameObject.SetActive(true);
        UIManager.Instance._ComboPanel.gameObject.SetActive(true);

        ComboManager.Instance.Combos.FirstOrDefault().ComboList.Clear();

        foreach (var a in FindObjectOfType<ComboPanel>().AtackItemPanel)
        {
            ComboManager.Instance.Combos.FirstOrDefault().ComboList.Add(a.GetComponent<SkillUIItem>()._AttackItem);
        }
    }

    //public List<T> GetScriptableObjectsOfType<T>() where T : ScriptableObject
    //{
    //    List<T> scriptableObjects = new List<T>();

    //    // Suche nach allen Scriptable Objects im Projekt
    //    string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);

    //    foreach (string guid in guids)
    //    {
    //        string path = AssetDatabase.GUIDToAssetPath(guid);
    //        T scriptableObject = AssetDatabase.LoadAssetAtPath<T>(path);
    //        if (scriptableObject != null)
    //        {
    //            scriptableObjects.Add(scriptableObject);
    //        }
    //    }

    //    return scriptableObjects;
    //}
}

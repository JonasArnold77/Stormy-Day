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

    public AttackItem _AttackItem;

    public SkillUIItem AttackComboItem;

    public Button CloseButton;

    public Transform DetailInfoPanel;
    public Button ApplyButton;

    public List<GameObject> allButtons = new List<GameObject>();
    public Button ActualButton;

    public TMP_Text nameText;
    public TMP_Text subscriptionText;
    public TMP_Text damageText;
    public TMP_Text speedText;
    public TMP_Text effectText;

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

        allButtons.Clear();
        _AttackItem = null;
        DetailInfoPanel.gameObject.SetActive(false);

        foreach (Transform child in Content)
        {
            // Zerst�re das Kind-GameObject
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
                item.GetComponent<Button>().onClick.AddListener(() => ChooseButton(item.GetComponent<Button>(), item, s));
                item.GetComponent<WorldImage>().m_worldObjects.Add(s.AnimationObject);
            }
        }
    }

    public void ChooseButton(Button button, GameObject item, AttackItem aitem)
    {
        ActualButton = button;
        ActualButton.GetComponent<SkillUIItem>().IsSelected = true;
        ActualButton.GetComponent<SkillUIItem>().OutlineGO.SetActive(true);

        _AttackItem = aitem;
        DetailInfoPanel.gameObject.SetActive(true);

        SetValuesInDetailwindow(aitem);

        allButtons.Where(b => !item.Equals(b)).ToList().ForEach(b1 => b1.GetComponent<SkillUIItem>().IsSelected = false);
        allButtons.Where(b => !item.Equals(b)).ToList().ForEach(b1 => b1.GetComponent<SkillUIItem>().OutlineGO.SetActive(false));
    }

    private void SetValuesInDetailwindow(AttackItem aitem)
    {
        nameText.text = aitem.Name;
        subscriptionText.text = aitem.Description;
        damageText.text = aitem.Damage.ToString();
        speedText.text = aitem.Speed.ToString();

        if(aitem.StatusEffect == EStatusEffects.Fire)
        {
            effectText.color = Color.red;
            effectText.text = aitem.StatusEffect.ToString();
        }
        else if (aitem.StatusEffect == EStatusEffects.Ice) 
        {
            effectText.color = Color.cyan;
            effectText.text = aitem.StatusEffect.ToString();
        }
        else if (aitem.StatusEffect == EStatusEffects.None)
        {
            effectText.color = Color.black;
            effectText.text = aitem.StatusEffect.ToString();
        }
    }

    public void SetComboItem()
    {
        SetComboAttackItem(_AttackItem);
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

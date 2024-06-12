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

    public SkillUIItem AttackComboItem;

    public Button CloseButton;

    public void Start()
    {

    }

    public void InitializeMenu(EHitType type)
    {
        GetComponent<CanvasGroup>().alpha = 1;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        List<AttackItem> myScriptableObjects = InventoryManager.Instance.AllSkills;
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
                item.GetComponentInChildren<TMP_Text>().text = s.Name;
                //item.GetComponent<Image>().sprite = s.Image;
                item.GetComponent<Button>().onClick.AddListener(() => SetComboAttackItem(s));
            }
        }
    }

    public void SetComboAttackItem(AttackItem item)
    {
        AttackComboItem.SetAttackItem(item);
        GetComponent<CanvasGroup>().alpha = 0;
        GetComponent<CanvasGroup>().blocksRaycasts = false;

        FindObjectOfType<MainMenu>().GetComponent<CanvasGroup>().blocksRaycasts = true;
        FindObjectOfType<MainMenu>().GetComponent<CanvasGroup>().alpha = 1;
        FindObjectOfType<ComboPanel>().GetComponent<CanvasGroup>().blocksRaycasts = true;
        FindObjectOfType<ComboPanel>().GetComponent<CanvasGroup>().alpha = 1;

        ComboManager.Instance.Combos.FirstOrDefault().ComboList.Clear();

        foreach (var a in FindObjectOfType<ComboPanel>().AtackItemPanel)
        {
            ComboManager.Instance.Combos.FirstOrDefault().ComboList.Add(a.GetComponent<SkillUIItem>()._AttackItem);
        }
    }

    public List<T> GetScriptableObjectsOfType<T>() where T : ScriptableObject
    {
        List<T> scriptableObjects = new List<T>();

        // Suche nach allen Scriptable Objects im Projekt
        string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            T scriptableObject = AssetDatabase.LoadAssetAtPath<T>(path);
            if (scriptableObject != null)
            {
                scriptableObjects.Add(scriptableObject);
            }
        }

        return scriptableObjects;
    }
}

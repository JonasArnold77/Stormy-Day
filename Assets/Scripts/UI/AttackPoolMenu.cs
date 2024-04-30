using System.Collections;
using System.Collections.Generic;
using TMPro;
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
        List<AttackItem> myScriptableObjects = GetScriptableObjectsOfType<AttackItem>();

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
                item.GetComponentInChildren<TMP_Text>().text = s.name;
                item.GetComponent<Button>().onClick.AddListener(() => SetComboAttackItem(s));
            }
        }
    }

    public void SetComboAttackItem(AttackItem item)
    {
        AttackComboItem.SetAttackItem(item);
        GetComponent<CanvasGroup>().alpha = 0;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        FindObjectOfType<ComboPanel>().GetComponent<CanvasGroup>().blocksRaycasts = true;
        FindObjectOfType<ComboPanel>().GetComponent<CanvasGroup>().alpha = 1;
    }

    public List<T> GetScriptableObjectsOfType<T>() where T : ScriptableObject
    {
        List<T> scriptableObjects = new List<T>();

        // Suche nach allen Scriptable Objects im Projekt
        T[] allScriptableObjects = Resources.FindObjectsOfTypeAll<T>();

        // Überprüfe jedes gefundene Scriptable Object
        foreach (T scriptableObject in allScriptableObjects)
        {
            // Füge das Scriptable Object zur Liste hinzu, wenn es den gewünschten Typ hat
            if (scriptableObject.GetType() == typeof(T))
            {
                scriptableObjects.Add(scriptableObject);
            }
        }

        return scriptableObjects;
    }
}

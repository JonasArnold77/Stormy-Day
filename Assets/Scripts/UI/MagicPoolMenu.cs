using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicPoolMenu : MonoBehaviour
{
    //public void InitializeMenu(EHitType type)
    //{
    //    List<AttackItem> myScriptableObjects = InventoryManager.Instance.AllSkills.Select(s => s.GetComponent<AbilityLootObject>()._AttackItem).ToList();
    //    //List<AttackItem> myScriptableObjects = GetScriptableObjectsOfType<AttackItem>();

    //    allButtons.Clear();
    //    _AttackItem = null;
    //    DetailInfoPanel.gameObject.SetActive(false);

    //    foreach (Transform child in Content)
    //    {
    //        // Zerstöre das Kind-GameObject
    //        Destroy(child.gameObject);
    //    }

    //    foreach (var s in myScriptableObjects)
    //    {
    //        if (s.Type == type)
    //        {
    //            var item = Instantiate(AttackPoolItem, Content);
    //            allButtons.Add(item);

    //            item.GetComponent<SkillUIItem>().IsSelected = false;
    //            item.GetComponent<SkillUIItem>().OutlineGO.SetActive(false);

    //            item.GetComponentInChildren<TMP_Text>().text = "";
    //            //item.GetComponent<Image>().sprite = s.Image;
    //            item.GetComponent<Button>().onClick.AddListener(() => ChooseButton(item.GetComponent<Button>(), item, s));
    //            item.GetComponent<WorldImage>().m_worldObjects.Add(s.AnimationObject);
    //        }
    //    }
    //}
}

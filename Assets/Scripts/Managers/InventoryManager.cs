using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public GameObject ActualWeapon;

    public List<GameObject> AllMagicSkills = new List<GameObject>();
    public List<GameObject> AllSkills = new List<GameObject>();
    public List<ArmorLootObject> AllArmors = new List<ArmorLootObject>();
    public List<GameObject> QuestItems = new List<GameObject>();

    public static InventoryManager Instance;
    
    private void Awake()
    {
        Instance = this;
    }

    public UnityAction GetInputFromControlAction()
    {
        return null;
    }

    private void Update()
    {
        var x = QuestItems;
    }

    public void SetArmor(ArmorLootObject armorObject)
    {
        Equipment.Instance.ArmorParts.Where(a => a.ArmorType == armorObject.ArmorType).ToList().ForEach(a1 => a1.gameObject.SetActive(false));
        //Equipment.Instance.ArmorParts.Where(a => a.ArmorType == armorObject.ArmorType).ToList().ForEach(a1 => a1.IsEquipped = false);

        if(!Equipment.Instance.ArmorParts.Where(a => a.gameObject.name == armorObject.ArmorName).FirstOrDefault().IsEquipped)
        {
            Equipment.Instance.ArmorParts.Where(a => a.gameObject.name == armorObject.ArmorName).FirstOrDefault().gameObject.SetActive(true);
            Equipment.Instance.ArmorParts.Where(a => a.gameObject.name == armorObject.ArmorName).FirstOrDefault().IsEquipped = true;
            Equipment.Instance.ArmorParts.Where(a => a.ArmorType == armorObject.ArmorType && a.gameObject.name != armorObject.ArmorName).ToList().ForEach(a1 => a1.IsEquipped = false);
        }
        else
        {
            Equipment.Instance.ArmorParts.Where(a => a.gameObject.name == armorObject.ArmorName).FirstOrDefault().gameObject.SetActive(false);
            Equipment.Instance.ArmorParts.Where(a => a.gameObject.name == armorObject.ArmorName).FirstOrDefault().IsEquipped = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class InventoryManager : MonoBehaviour
{
    public GameObject ActualWeapon;

    public List<AttackItem> AllSkills = new List<AttackItem>();
    public List<ArmorLootObject> AllArmors = new List<ArmorLootObject>();

    public static InventoryManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public UnityAction GetInputFromControlAction()
    {
        return null;
    }

    public void SetArmor(ArmorLootObject armorObject)
    {
        Equipment.Instance.ArmorParts.Where(a => a.ArmorType == armorObject.ArmorType).ToList().ForEach(a1 => a1.gameObject.SetActive(false));
        Equipment.Instance.ArmorParts.Where(a => a.gameObject.name == armorObject.ArmorName).FirstOrDefault().gameObject.SetActive(true);
    }

}

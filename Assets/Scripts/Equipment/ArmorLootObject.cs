using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmorLootObject : LootObject
{
    public string ArmorName;
    public GameObject Armor;
    public Sprite _Image;
    public EArmorType ArmorType;

    private void Start()
    {
        ArmorName = Armor.name;
    }
}

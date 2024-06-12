using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorLootObject : LootObject
{
    public string ArmorName;
    public GameObject Armor;

    private void Start()
    {
        ArmorName = Armor.name;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorLootObject : MonoBehaviour
{
    public string Name;
    public GameObject Armor;

    private void Start()
    {
        Name = Armor.name;
    }
}

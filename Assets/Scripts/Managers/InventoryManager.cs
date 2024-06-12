using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryManager : MonoBehaviour
{
    public GameObject ActualWeapon;

    public List<AttackItem> AllSkills = new List<AttackItem>();
    public List<GameObject> AllArmors = new List<GameObject>();

    public static InventoryManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public UnityAction GetInputFromControlAction()
    {
        return null;
    }

}

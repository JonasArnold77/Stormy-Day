using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance;
    public EWeaponType ActualWeaponType;
    public Sword ActualWeapon;

    private void Awake()
    {
        Instance = this;
    }
}

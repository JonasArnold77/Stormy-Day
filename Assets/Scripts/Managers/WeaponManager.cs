using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance;
    public EWeaponType ActualWeaponType;
    public Sword ActualWeapon;

    public Transform RightFist;
    public Transform LeftFist;
    public Transform RightFoot;
    public Transform LeftFoot;
    public Transform RightKnee;
    public Transform LeftKnee;
    public Transform RightElbow;
    public Transform LeftElbow;
    public Transform Sword;
    public Transform Head;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        DeactivateAllColliders();
    }

    private void Update()
    {
        if (Input.GetKeyDown((KeyCode)InputManager.Instance.GetInputActionFromControlInput(EControls.SwitchWeapon)))
        {
            if (ActualWeaponType == EWeaponType.OneHanded)
            {
                ActualWeaponType = EWeaponType.Pistol;
            }
            else
            {
                ActualWeaponType = EWeaponType.OneHanded;
            }
        }
    }

    private void DeactivateAllColliders()
    {
        RightFist.GetComponent<Collider>().enabled = false;
        LeftFist.GetComponent<Collider>().enabled = false;
        RightFoot.GetComponent<Collider>().enabled = false;
        LeftFoot.GetComponent<Collider>().enabled = false;
        RightKnee.GetComponent<Collider>().enabled = false;
        LeftKnee.GetComponent<Collider>().enabled = false;
        RightElbow.GetComponent<Collider>().enabled = false;
        LeftElbow.GetComponent<Collider>().enabled = false;
        Head.GetComponent<Collider>().enabled = false;
        Sword.GetComponent<Collider>().enabled = false;
    }

    public Transform GetWeaponComponent(EHitType type)
    {
        if (type == EHitType.RightFist)
        {
            return RightFist;
        }
        else if(type == EHitType.LeftFist)
        {
            return LeftFist;
        }
        else if (type == EHitType.RightFoot)
        {
            return RightFoot;
        }
        else if (type == EHitType.LeftFoot)
        {
            return LeftFoot;
        }
        else if (type == EHitType.RightKnee)
        {
            return RightKnee;
        }
        else if (type == EHitType.LeftKnee)
        {
            return LeftKnee;
        }
        else if (type == EHitType.RightElbow)
        {
            return RightElbow;
        }
        else if (type == EHitType.LeftElbow)
        {
            return LeftElbow;
        }
        else if (type == EHitType.Head)
        {
            return Head;
        }
        else if (type == EHitType.Sword)
        {
            return Sword;
        }
        return null;
    }
}

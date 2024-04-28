using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/AttackItem", order = 1)]
public class AttackItem : ScriptableObject
{
    public AnimationClip _Animation;
    public EControls Control;
    public EHitEffect Effect;
    public EHitType Type;
    public EWeaponType WeaponType;
    public int Damage;
    public string Name;
    public string Description;
}

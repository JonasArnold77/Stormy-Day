using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/AttackItem", order = 1)]
public class AttackItem : ScriptableObject
{
    public AnimationClip _Animation;
    public Transform AnimationObject;
    public EControls Control;
    public EHitEffect Effect;
    public EHitType Type;
    public EWeaponType WeaponType;
    public EStatusEffects StatusEffect;
    public int Damage;
    public float Speed;
    public int EnduranceCost;
    public string Name;
    public string Description;
    public Sprite Image;
    public int Level;
}

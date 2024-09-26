using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Magic Item", order = 1)]
public class MagicItem : ScriptableObject
{
    public GameObject EffectGameObject;
    public EMagicTypes Type;
    public float DamagePerSecond;
    public int TimeOfAttack;
    public Sprite Image;
    public EStatusEffects effect;
}

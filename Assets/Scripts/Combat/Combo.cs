using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Combo
{
    public List<AnimationClip> ComboList;
    public int Counter;
    public EControls InputType;
    public EWeaponType WeaponType;
    public void IncreaseCounter()
    {
        if(Counter == ComboList.Count - 1)
        {
            Counter = 0;
        }
        else
        {
            Counter++;
        }
    }

    public void ResetCounter()
    {
        Counter = 0;
    }
}

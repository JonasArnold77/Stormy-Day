using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SuperCombo
{
    public List<EControls> InputList;
    public int Counter;

    public void Effect()
    {
        Debug.Log("Combo Done");
    }
}

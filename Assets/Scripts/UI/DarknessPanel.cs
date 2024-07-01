using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarknessPanel : MonoBehaviour
{
    public static DarknessPanel Instance;

    private void Awake()
    {
        Instance = this;
    }
}

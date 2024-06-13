using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    public List<ArmorPart> ArmorParts = new List<ArmorPart>();

    public static Equipment Instance;

    private void Awake()
    {
        Instance = this;
    }
}

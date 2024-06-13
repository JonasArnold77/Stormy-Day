using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : MonoBehaviour
{
    private CanvasGroup _canvasGroup;
    public static InventoryPanel Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if(_canvasGroup.alpha == 0)
        {

        }
        else
        {

        }
    }
}

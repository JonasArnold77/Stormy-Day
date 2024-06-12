using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseButton : MonoBehaviour
{
    public List<CanvasGroup> TargetWindows = new List<CanvasGroup>();

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => CloseWindow());
    }

    private void CloseWindow()
    {
        foreach(var t in TargetWindows)
        {
            t.alpha = 0;
            t.blocksRaycasts = false;
        }
    }
}

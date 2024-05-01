using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseButton : MonoBehaviour
{
    public CanvasGroup TargetWindow;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => CloseWindow());
    }

    private void CloseWindow()
    {
        TargetWindow.alpha = 0;
        TargetWindow.blocksRaycasts = false;
    }
}

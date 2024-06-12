using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private CanvasGroup _canvasGroup;
    void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (_canvasGroup.alpha == 1)
            {
                _canvasGroup.alpha = 0;
                _canvasGroup.blocksRaycasts = false;
            }
            else
            {
                _canvasGroup.alpha = 1;
                _canvasGroup.blocksRaycasts = true;
            }
        }
    }
}

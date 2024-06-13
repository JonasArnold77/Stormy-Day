using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeySuggestionMenu : MonoBehaviour
{
    private CanvasGroup _canvasGroup;
    public TMP_Text KeyText;
    public static KeySuggestionMenu Instance;

    private void Awake()
    {
        Instance = this;
        _canvasGroup = GetComponent<CanvasGroup>();
    }
    
    public void SetVisiabilityState(bool state)
    {
        gameObject.SetActive(state);
    }
}

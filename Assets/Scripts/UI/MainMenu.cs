using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private CanvasGroup _canvasGroup;
    public static MainMenu Instance;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    
}

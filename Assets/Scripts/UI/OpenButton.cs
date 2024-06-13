using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenButton : MonoBehaviour
{
    public List<GameObject> TargetWindows = new List<GameObject>();

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => OpenWindow());
    }

    private void OpenWindow()
    {
        foreach (var t in TargetWindows)
        {
            t.SetActive(true);
        }
    }
}

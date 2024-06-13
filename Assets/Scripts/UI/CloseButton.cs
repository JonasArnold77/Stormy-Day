using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseButton : MonoBehaviour
{
    public List<GameObject> TargetWindows = new List<GameObject>();

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => CloseWindow());
    }

    private void CloseWindow()
    {
        foreach(var t in TargetWindows)
        {
            t.gameObject.SetActive(false);
        }
    }
}

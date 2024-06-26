using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStatusPanel : MonoBehaviour
{
    public Image FireBar;
    public Image IceBar;

    public GameObject FireWindow;
    public GameObject IceWindow;

    public static EnemyStatusPanel Instance;

    private void Start()
    {
        Instance = this;
    }

    public void SetActualFireAmount(float value)
    {
        FireBar.fillAmount = value;
    }
    public void SetActualIceAmount(float value)
    {
        IceBar.fillAmount = value;
    }
}

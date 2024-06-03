using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStatusPanel : MonoBehaviour
{
    public Image FireBar;

    public static EnemyStatusPanel Instance;

    private void Start()
    {
        Instance = this;
        gameObject.SetActive(false);
    }

    public void SetActualFireAmount(float value)
    {
        FireBar.fillAmount = value;
    }
}

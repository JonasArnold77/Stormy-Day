using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthAndEndurancePanel : MonoBehaviour
{
    public Image HealthBar;
    public Image EnduranceBar;
    public Image ManaBar;

    public static HealthAndEndurancePanel Instance;

    private void Start()
    {
        Instance = this;
    }

    public void SetActualHealth(float value)
    {
        HealthBar.fillAmount = value;
    }

    public void SetActualEndurance(float value)
    {
        EnduranceBar.fillAmount = value;
    }

    public void SetActualMana(float value)
    {
        ManaBar.fillAmount = value;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    public List<EControls> CollectingInputsList = new List<EControls>();

    private void Awake()
    {
        Instance = this;
    }

    public KeyCode? GetInputActionFromControlInput(EControls control)
    {
        if(control == EControls.LightHit)
        {
            return KeyCode.Mouse0;
        }
        if (control == EControls.HardHit)
        {
            return KeyCode.Mouse1;
        }
        if (control == EControls.Mage)
        {
            return KeyCode.F;
        }
        if (control == EControls.Effect1)
        {
            return KeyCode.Alpha1;
        }
        if (control == EControls.Effect2)
        {
            return KeyCode.Alpha2;
        }
        if (control == EControls.Effect3)
        {
            return KeyCode.Alpha3;
        }
        if (control == EControls.Dash)
        {
            return KeyCode.LeftControl;
        }
        if (control == EControls.Collect)
        {
            return KeyCode.E;
        }
        if (control == EControls.SwitchWeapon)
        {
            return KeyCode.Tab;
        }
        return null;
    }

    public void IncreaseInputCollection(EControls control)
    {
        CollectingInputsList.Add(control);
    }
}

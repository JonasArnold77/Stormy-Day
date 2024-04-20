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
        return null;
    }

    public void IncreaseInputCollection(EControls control)
    {
        CollectingInputsList.Add(control);
    }
}

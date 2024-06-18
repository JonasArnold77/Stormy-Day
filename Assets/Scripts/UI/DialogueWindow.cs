using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueWindow : MonoBehaviour
{
    public TMP_Text Text;

    public static DialogueWindow Instance;

    private void Awake()
    {
        Instance = this;
    }

}

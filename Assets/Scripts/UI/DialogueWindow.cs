using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueWindow : MonoBehaviour
{
    public TMP_Text Text;
    public TMP_Text NameText;

    public static DialogueWindow Instance;

    private void Awake()
    {
        Instance = this;
    }

}

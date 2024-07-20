using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    public GameObject DarkBloodEffect;
    public RectTransform ArmorUiItem;
    public GameObject QuestMarker;

    public static PrefabManager Instance;

    private void Awake()
    {
        Instance = this;
    }


}

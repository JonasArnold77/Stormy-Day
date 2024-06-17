using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestStep : MonoBehaviour
{
    public Transform QuestStepPlace;
    public List<string> Dialogue = new List<string>();
    public List<GameObject> TargetEnemies = new List<GameObject>();
    public List<GameObject> QuestObject = new List<GameObject>();
    public bool QuestStepIsActive;
}

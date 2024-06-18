using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObject : MonoBehaviour
{
    public List<QuestStep> QuestSteps = new List<QuestStep>();
    public int QuestStepIndex = 0;

    private void Start()
    {
        QuestSteps[QuestStepIndex].QuestStepIsActive = true;
    }

    private void Update()
    {
        if(QuestStepIndex < QuestSteps.Count - 1 && QuestSteps[QuestStepIndex].DialogueIsDone && QuestSteps[QuestStepIndex].EnemiesAreDone)
        {
            QuestSteps[QuestStepIndex].QuestStepIsActive = false;
            QuestStepIndex++;
            QuestSteps[QuestStepIndex].QuestStepIsActive = true;
        }
    }

}
    
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestObject : MonoBehaviour
{
    public List<QuestStep> QuestSteps = new List<QuestStep>();
    public int QuestStepIndex = 0;

    public List<RectTransform> AllMarkers = new List<RectTransform>();

    private void Start()
    {
        QuestSteps[QuestStepIndex].QuestStepIsActive = true;
        SetMarker();
    }

    private void Update()
    {
        if(QuestStepIndex < QuestSteps.Count - 1 && QuestSteps[QuestStepIndex].DialogueIsDone && QuestSteps[QuestStepIndex].EnemiesAreDone && QuestSteps[QuestStepIndex].QuestItemsAreDone)
        {
            QuestSteps[QuestStepIndex].QuestStepIsActive = false;
            QuestStepIndex++;
            QuestSteps[QuestStepIndex].QuestStepIsActive = true;
            SetMarker();
        }
    }

    public void SetMarker()
    {
        AllMarkers.ForEach(m => m.gameObject.SetActive(false));
        if (QuestSteps[QuestStepIndex].QuestGiverName == "Father")
        {
            AllMarkers.Where(m => m.name == "Marker Farther").FirstOrDefault().gameObject.SetActive(true);
        }
        else if (QuestSteps[QuestStepIndex].QuestGiverName == "Daughter")
        {
            AllMarkers.Where(m => m.name == "Marker Ruins").FirstOrDefault().gameObject.SetActive(true);
        }
        else if (QuestSteps[QuestStepIndex].QuestGiverName == "Letter")
        {
            AllMarkers.Where(m => m.name == "Marker Cages").FirstOrDefault().gameObject.SetActive(true);
        }
        else if (QuestSteps[QuestStepIndex].QuestGiverName == "Lynx")
        {
            AllMarkers.Where(m => m.name == "Marker Dead Boy").FirstOrDefault().gameObject.SetActive(true);
        }
    }
}
    
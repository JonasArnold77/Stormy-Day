using PixelCrushers.QuestMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public QuestJournal QuestJournalInstance;
    // Start is called before the first frame update
    void Start()
    {
        QuestJournalInstance = GetComponent<QuestJournal>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            QuestJournalInstance.ToggleJournalUI();
        }
    }
}

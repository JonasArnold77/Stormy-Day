using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class QuestStep : MonoBehaviour
{
    public Transform QuestStepPlace;
    public List<string> Dialogue = new List<string>();
    public List<GameObject> TargetEnemies = new List<GameObject>();
    public List<QuestItem> QuestItems = new List<QuestItem>();
    public bool QuestStepIsActive;

    public bool DialogueIsDone;
    private bool DialogueIsStarted;

    public bool EnemiesAreDone;
    private bool EnemiesAreStarted;

    public bool QuestItemsAreDone;
    private bool QuestItemsAreStarted;

    private GameObject CurrentKeyWindow;

    private Transform PlayerTransfomr;

    private void Start()
    {
        PlayerTransfomr = FindObjectOfType<ThirdPersonController>().transform;
    }

    private void Update()
    {
        if (QuestStepIsActive)
        {
            if (QuestStepPlace != null)
            {
                if (!DialogueIsStarted && Dialogue.Count > 0 && Vector3.Distance(PlayerTransfomr.position, QuestStepPlace.position) < 8)
                {
                    if(CurrentKeyWindow == null)
                    {
                        CurrentKeyWindow = Instantiate(UIManager.Instance._KeySuggestionMenu.gameObject, UIManager.Instance._KeySuggestionMenu.transform.parent);
                        CurrentKeyWindow.SetActive(true);
                    }
                    

                    if (Input.GetKeyDown((KeyCode)InputManager.Instance.GetInputActionFromControlInput(EControls.Collect)))
                    {
                        StartCoroutine(DoDialogueCoroutine());
                        PlayerTransfomr.GetComponent<ThirdPersonController>().MoveSpeed = 0;
                        UIManager.Instance._DialogueMenu.gameObject.SetActive(true);
                    }
                }
                else
                {
                    Destroy(CurrentKeyWindow);
                }
            }
            else
            {
                DialogueIsDone = true;
            }

            if(!EnemiesAreStarted && TargetEnemies.Count > 0)
            {
                EnemiesAreStarted = true;
                StartCoroutine(WaitForEnemiesAreDead());
            }
            else if(TargetEnemies.Count == 0)
            {
                EnemiesAreDone = true;
            }

            if(QuestItems.Count > 0)
            {
                WaitForQuetItemsAreCollected();
            }
            else
            {
                QuestItemsAreDone = true;
            }
        }  
    }

    private void WaitForQuetItemsAreCollected()
    {
        bool AllAreTrue = true;
        foreach (var qo in QuestItems)
        {
            if(!InventoryManager.Instance.QuestItems.Select(o => o.QuestItemID).ToList().Contains(qo.QuestItemID))
            {
                AllAreTrue = false;
            }
        }

        if (AllAreTrue)
        {
            QuestItemsAreDone = true;
        }
    }

    private IEnumerator WaitForEnemiesAreDead()
    {
        yield return new WaitUntil(() => TargetEnemies.Where(h => !h.GetComponent<EnemyAnimation>().IsDead).ToList().Count == 0);
        EnemiesAreDone = true;
    }


    IEnumerator TypeText(string fullText, TMP_Text currentText)
    {
        for (int i = 0; i < fullText.Length; i++)
        {
            currentText.text += fullText[i];
            yield return new WaitForSeconds(0.03f);
        }
    }

    private IEnumerator DoDialogueCoroutine()
    {
        yield return new WaitForSeconds(2f);
        KeySuggestionMenu.Instance.gameObject.SetActive(false);

        DialogueIsStarted = true;
        for (int i = 0; i<Dialogue.Count; i++)
        {
            //DialogueWindow.Instance.Text.text = Dialogue[i];

            var Coroutine = StartCoroutine(TypeText(Dialogue[i], DialogueWindow.Instance.Text));
            yield return new WaitForSeconds(1f);
            yield return new WaitUntil(() => Input.GetKeyDown((KeyCode)InputManager.Instance.GetInputActionFromControlInput(EControls.Collect)));
            StopCoroutine(Coroutine);
            DialogueWindow.Instance.Text.text = "";
        }

        PlayerTransfomr.GetComponent<ThirdPersonController>().MoveSpeed = 6;
        UIManager.Instance._DialogueMenu.gameObject.SetActive(false);
        DialogueIsDone = true;
    }
}
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
    public List<GameObject> QuestObject = new List<GameObject>();
    public bool QuestStepIsActive;

    public bool DialogueIsDone;
    private bool DialogueIsStarted;

    public bool EnemiesAreDone;

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
                    UIManager.Instance._KeySuggestionMenu.gameObject.SetActive(true);

                    if (Input.GetKeyDown((KeyCode)InputManager.Instance.GetInputActionFromControlInput(EControls.Collect)))
                    {
                        StartCoroutine(DoDialogueCoroutine());
                        PlayerTransfomr.GetComponent<ThirdPersonController>().MoveSpeed = 0;
                        UIManager.Instance._DialogueMenu.gameObject.SetActive(true);
                    }
                }
                else
                {
                    UIManager.Instance._KeySuggestionMenu.gameObject.SetActive(false);
                }
            }
            else
            {
                DialogueIsDone = true;
            }

            if(TargetEnemies.Count > 0)
            {
                StartCoroutine(WaitForEnemiesAreDead());
            }
            else
            {
                EnemiesAreDone = true;
            }
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

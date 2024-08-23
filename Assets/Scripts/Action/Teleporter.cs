using FMCOLOR;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Teleporter : MonoBehaviour
{
    public int KeyId;
    public bool IsDone;

    private void Start()
    {
        PostProcessingManager.Instance.LightGameObjects.ForEach(l => l.SetActive(false));
        PostProcessingManager.Instance._FMColor.enabled = false;
        StartCoroutine(PostProcessingManager.Instance.ActivateVolume(PostProcessingManager.Instance.Bar, 0.83f));
        StartCoroutine(PostProcessingManager.Instance.DeactivateVolume(PostProcessingManager.Instance.World));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && InventoryManager.Instance.QuestItems.Select(q => q.GetComponent<QuestItem>().QuestItemID).Contains(KeyId) && IsDone == false)
        {
            IsDone = true;
            StartCoroutine(TeleportCoroutine());
            FindObjectOfType<Attack>().playerTransform.position = new Vector3(445f, 148f, -250f);
            FindObjectOfType<CameraFollow>().SetPosition();
        }
    }

    public IEnumerator TeleportCoroutine()
    {
        UIManager.Instance._DarknessPanel.gameObject.SetActive(true);

        FindObjectOfType<ThirdPersonController>().MoveSpeed = 0;

        UIManager.Instance._DarknessPanel.GetComponent<Image>().color = new Color(UIManager.Instance._DarknessPanel.GetComponent<Image>().color.r, UIManager.Instance._DarknessPanel.GetComponent<Image>().color.g, UIManager.Instance._DarknessPanel.GetComponent<Image>().color.b,0f);

        

        //while(UIManager.Instance._DarknessPanel.GetComponent<Image>().color.a < 1f)
        //{
        //    UIManager.Instance._DarknessPanel.GetComponent<Image>().color = new Color(UIManager.Instance._DarknessPanel.GetComponent<Image>().color.r, UIManager.Instance._DarknessPanel.GetComponent<Image>().color.g, UIManager.Instance._DarknessPanel.GetComponent<Image>().color.b, UIManager.Instance._DarknessPanel.GetComponent<Image>().color.a + 0.01f);
        //    yield return new WaitForSeconds(0.015f);
        //}


        UIManager.Instance._DarknessPanel.GetComponent<Image>().color = new Color(UIManager.Instance._DarknessPanel.GetComponent<Image>().color.r, UIManager.Instance._DarknessPanel.GetComponent<Image>().color.g, UIManager.Instance._DarknessPanel.GetComponent<Image>().color.b, 1f);

        yield return new WaitForSeconds(1f);

        PostProcessingManager.Instance.LightGameObjects.ForEach(l => l.SetActive(true));
        PostProcessingManager.Instance._FMColor.enabled = true;
        StartCoroutine(PostProcessingManager.Instance.ActivateVolume(PostProcessingManager.Instance.World, 0.83f));
        StartCoroutine(PostProcessingManager.Instance.DeactivateVolume(PostProcessingManager.Instance.Bar));

        //while (UIManager.Instance._DarknessPanel.GetComponent<Image>().color.a > 0f)
        //{
        //    UIManager.Instance._DarknessPanel.GetComponent<Image>().color = new Color(UIManager.Instance._DarknessPanel.GetComponent<Image>().color.r, UIManager.Instance._DarknessPanel.GetComponent<Image>().color.g, UIManager.Instance._DarknessPanel.GetComponent<Image>().color.b, UIManager.Instance._DarknessPanel.GetComponent<Image>().color.a - 0.01f);
        //    yield return new WaitForSeconds(0.015f);
        //}

        FindObjectOfType<ThirdPersonController>().MoveSpeed = 6;

        

        UIManager.Instance._DarknessPanel.GetComponent<Image>().color = new Color(UIManager.Instance._DarknessPanel.GetComponent<Image>().color.r, UIManager.Instance._DarknessPanel.GetComponent<Image>().color.g, UIManager.Instance._DarknessPanel.GetComponent<Image>().color.b, 0f);
        UIManager.Instance._DarknessPanel.gameObject.SetActive(false);

        FindObjectOfType<Attack>().playerTransform.position = new Vector3(445f, 148f, -250f);
        FindObjectOfType<CameraFollow>().SetPosition();
    }
}

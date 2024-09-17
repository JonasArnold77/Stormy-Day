using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthItem : MonoBehaviour
{
    private GameObject Player;
    private float HealthAmount = 20;

    private bool isCollected;

    private RectTransform uiElement;

    private void Start()
    {
        Player = FindObjectOfType<ThirdPersonController>().gameObject;
        GenerateItem();
        uiElement = UIManager.Instance._HealthPanel.HealthBar.GetComponent<RectTransform>();
    }

    public void GenerateItem()
    {
        var percentageAmount = Random.Range(0.4f, 1f);
        transform.localScale = transform.localScale * percentageAmount;
        HealthAmount = HealthAmount * percentageAmount;
    }

    public void Update()
    {
        if (Vector3.Distance(transform.position, Player.transform.position)<6 && !isCollected)
        {
            FollowPlayer();
        }

        if (Vector3.Distance(transform.position, Player.transform.position) < 2 && !isCollected)
        {
            //Destroy(gameObject);
            isCollected = true;
            StartCoroutine(MoveToUIElement());

            //var object2d = Instantiate(PrefabManager.Instance.HealthItem2D, position: GetScreenPosition(), Quaternion.identity, parent: UIManager.Instance._HealthPanel.transform);
            //StartCoroutine(MoveToTarget(object2d));
        }
    }

    private IEnumerator MoveToTarget(RectTransform obj)
    {
        Vector3 startPosition = obj.anchoredPosition;
        Vector3 targetPosition = UIManager.Instance._HealthPanel.HealthBar.GetComponent<RectTransform>().anchoredPosition;
        float elapsedTime = 0f;

        while (elapsedTime < 3f)
        {
            elapsedTime += Time.deltaTime;
            obj.anchoredPosition = Vector3.Lerp(startPosition, targetPosition, elapsedTime / 3f);
            yield return null;  // Wartet bis zum nächsten Frame
        }

        // Sicherstellen, dass die Endposition erreicht wird
        obj.anchoredPosition = targetPosition;
    }

    private Vector2 GetScreenPosition()
    {
        // 1. Hole die Weltposition des 3D-Objekts
        Vector3 worldPosition = Player.transform.position;

        // 2. Konvertiere die Weltposition in Bildschirmkoordinaten
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);

        // 3. Konvertiere die Bildschirmkoordinaten in den UI-Canvas-Raum
        Vector2 uiPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            uiElement.parent as RectTransform, 
            screenPosition,
            Camera.main, 
            out uiPosition
        );

        // 4. Setze die Position des UI-Elements
       return uiPosition;
    }

    private IEnumerator MoveToUIElement()
    {
        // Konvertiere UI-Position (Screen Space) zu Weltposition
        Vector3 worldPosition;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(
            uiElement,
            uiElement.position,
            Camera.main,
            out worldPosition
        );

        worldPosition = new Vector3(worldPosition.x, transform.position.y, worldPosition.z);

        // Objekt bewegt sich zur UI-Position
        while (Vector3.Distance(transform.position, worldPosition) > 1f)
        {
            // Bewegung in Richtung des Ziels
            transform.position = Vector3.MoveTowards(
                transform.position,
                worldPosition,
                10f * Time.deltaTime
            );

            // Warten bis zum nächsten Frame
            yield return null;
        }

        // Optionale Aktion wenn das Ziel erreicht ist
        Destroy(gameObject);
        Debug.Log("Ziel erreicht!");
    }

    public void FollowPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, 0.5f);
    }
}

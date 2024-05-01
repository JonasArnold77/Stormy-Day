using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ComboPanel : MonoBehaviour
{
    public Button AddNewAttackItemButton;
    public List<GameObject> AtackItemPanel = new List<GameObject>();

    public float distanceBetweenPanels;

    // Start is called before the first frame update
    void Start()
    {
        AddNewAttackItemButton.onClick.AddListener(() => AddNewAttackItem());
    }

    private void AddNewAttackItem()
    {
        //// Erstellen und Platzieren des zweiten Panels
        //RectTransform secondPanel = Instantiate(secondPanelPrefab, firstPanel.parent);

        //// Die Größe und Position des zweiten Panels setzen
        //secondPanel.sizeDelta = firstPanel.sizeDelta;

        var lastPanel = AtackItemPanel.LastOrDefault();
        var newPanel = Instantiate(lastPanel, transform);

        newPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(lastPanel.GetComponent<RectTransform>().anchoredPosition.x + distanceBetweenPanels + lastPanel.GetComponent<RectTransform>().rect.width * lastPanel.GetComponent<RectTransform>().localScale.x, lastPanel.GetComponent<RectTransform>().anchoredPosition.y);

        //var x = newPanel.GetComponent<RectTransform>();
        //// Position des zweiten Panels setzen, indem der Ankerpunkt angepasst wird
        //newPanel.GetComponent<RectTransform>().anchorMin = new Vector2(lastPanel.GetComponent<RectTransform>().anchorMin.x + lastPanel.GetComponent<RectTransform>().lossyScale.x, lastPanel.GetComponent<RectTransform>().anchorMin.y);
        //newPanel.GetComponent<RectTransform>().anchorMax = new Vector2(lastPanel.GetComponent<RectTransform>().anchorMax.x + lastPanel.GetComponent<RectTransform>().lossyScale.x, lastPanel.GetComponent<RectTransform>().anchorMax.y);
        //newPanel.GetComponent<RectTransform>().pivot = new Vector2(lastPanel.GetComponent<RectTransform>().pivot.x + lastPanel.GetComponent<RectTransform>().lossyScale.x, lastPanel.GetComponent<RectTransform>().pivot.y);
        //newPanel.GetComponent<RectTransform>().anchoredPosition = lastPanel.GetComponent<RectTransform>().anchoredPosition;

        AtackItemPanel.Add(newPanel);

        
    }
}

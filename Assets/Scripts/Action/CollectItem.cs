using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CollectItem : MonoBehaviour
{
    private List<GameObject> ListOfCollectableItems = new List<GameObject>();
    public Transform playerTransform; // Der Transform des Spielers

    public GameObject NearestObject;

    private void Update()
    {
        CheckForCollectableItems();
    }

    public void CheckForCollectableItems()
    {
        if(ListOfCollectableItems.Count > 0)
        {
            var pivotDistance = Vector3.Distance(ListOfCollectableItems.FirstOrDefault().transform.position, playerTransform.position);
            var pivotElement = ListOfCollectableItems.FirstOrDefault();

            foreach (var item in ListOfCollectableItems)
            {
                if(Vector3.Distance(item.transform.position, playerTransform.position) < pivotDistance)
                {
                    pivotDistance = Vector3.Distance(item.transform.position, playerTransform.position);
                    pivotElement = item;
                }
            }

            if (Input.GetKeyDown((KeyCode)InputManager.Instance.GetInputActionFromControlInput(EControls.Collect)))
            {
                InventoryManager.Instance.AllSkills.Add(pivotElement.GetComponent<AbilityLootObject>()._AttackItem);
                ListOfCollectableItems.Remove(pivotElement);
                Destroy(pivotElement);
            }

            KeySuggestionMenu.Instance.SetVisiabilityState(true);
        }
        else
        {
            KeySuggestionMenu.Instance.SetVisiabilityState(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<AbilityLootObject>() && !ListOfCollectableItems.Contains(other.gameObject))
        {
            ListOfCollectableItems.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (ListOfCollectableItems.Contains(other.gameObject))
        {
            ListOfCollectableItems.Remove(other.gameObject);
        }
    }
}

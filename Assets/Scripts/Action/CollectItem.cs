using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CollectItem : MonoBehaviour
{
    private List<GameObject> ListOfCollectableItems = new List<GameObject>();
    public Transform playerTransform; // Der Transform des Spielers

    public GameObject CurrentKeyWindow;

    public GameObject NearestObject;

    private int AnimationObjectOffset = 4;

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
                if (pivotElement.GetComponent<AbilityLootObject>())
                {
                    pivotElement.GetComponent<AbilityLootObject>()._AttackItem.AnimationObject = pivotElement.GetComponent<AbilityLootObject>().AnimationObject;

                    InventoryManager.Instance.AllSkills.Add(pivotElement.GetComponent<AbilityLootObject>()._AttackItem);

                    ListOfCollectableItems.Remove(pivotElement);
                    pivotElement.transform.position = new Vector3(AnimationObjectOffset, 0,0);

                    pivotElement.GetComponentsInChildren<ParticleSystem>().ToList().ForEach(p => p.gameObject.SetActive(false));

                    AnimationObjectOffset = AnimationObjectOffset + 4;
                }
                else if (pivotElement.GetComponent<ArmorLootObject>())
                {
                    InventoryManager.Instance.AllArmors.Add(pivotElement.GetComponent<ArmorLootObject>());
                    ListOfCollectableItems.Remove(pivotElement);
                    Destroy(pivotElement);
                }else if (pivotElement.GetComponent<QuestItem>())
                {
                    InventoryManager.Instance.QuestItems.Add(pivotElement.GetComponent<QuestItem>());
                    ListOfCollectableItems.Remove(pivotElement);
                    Destroy(pivotElement);
                }
            }

            if(CurrentKeyWindow == null)
            {
                CurrentKeyWindow = Instantiate(UIManager.Instance._KeySuggestionMenu.gameObject, UIManager.Instance._KeySuggestionMenu.transform.parent);
                CurrentKeyWindow.SetActive(true);
            }
        }
        else
        {
            Destroy(CurrentKeyWindow);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.GetComponent<AbilityLootObject>() || other.GetComponent<ArmorLootObject>() || other.GetComponent<QuestItem>()) && !ListOfCollectableItems.Contains(other.gameObject))
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

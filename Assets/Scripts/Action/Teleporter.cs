using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public int KeyId;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && InventoryManager.Instance.QuestItems.Select(q => q.QuestItemID).Contains(KeyId))
        {
            FindObjectOfType<Attack>().playerTransform.position = new Vector3(445.929993f, 148.039993f, -250.720001f);
            FindObjectOfType<CameraFollow>().SetPosition();

        }
    }
}

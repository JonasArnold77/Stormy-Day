using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public bool GoingIndoor;

    public List<GameObject> UnselectObjects = new List<GameObject>();
    public List<GameObject> SelectObjects = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        return;
        UnselectObjects.ForEach(o => o.SetActive(false));
        SelectObjects.ForEach(o => o.SetActive(true));

        if (GoingIndoor)
        {
            FindObjectOfType<CameraFollow>().CameraDistance = 12;
            FindObjectOfType<CameraFollow>().ZOffset = 7;
        }
        else
        {
            FindObjectOfType<CameraFollow>().CameraDistance = 20;
            FindObjectOfType<CameraFollow>().ZOffset = 14;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public int QuestItemKeyID;

    public Transform LeftDoor;
    public Transform RightDoor;

    public bool DoorIsOpen;

    private void Update()
    {
        if (DoorIsOpen)
        {
            LeftDoor.localRotation = Quaternion.RotateTowards(LeftDoor.localRotation, new Quaternion(LeftDoor.localRotation.x, -0.5f, LeftDoor.localRotation.z, LeftDoor.localRotation.w), 0.1f * Time.deltaTime);
            RightDoor.localRotation = Quaternion.RotateTowards(RightDoor.localRotation, new Quaternion(RightDoor.localRotation.x, 0.5f, RightDoor.localRotation.z, RightDoor.localRotation.w), 0.1f * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(InventoryManager.Instance.QuestItems.Select(q => q.GetComponent<QuestItem>().QuestItemID).Contains(QuestItemKeyID))
            {
                //StartCoroutine(RotateOverTime(-90f, 2f, LeftDoor));
                //StartCoroutine(RotateOverTime(90f, 2f, RightDoor));

                StartCoroutine(RotateObjectToY(LeftDoor, -80, 20f));
                StartCoroutine(RotateObjectToY(RightDoor, 80, 20f));

                //DoorIsOpen = true;
            }
        }
    }

    private IEnumerator RotateObjectToY(Transform target, float targetY, float speed)
    {
        // Calculate the target rotation
        Quaternion targetRotation = Quaternion.Euler(target.localEulerAngles.x, targetY, target.localEulerAngles.z);

        // Continue rotating until the target rotation is reached
        while (Quaternion.Angle(target.localRotation, targetRotation) > 0.01f)
        {
            // Rotate towards the target rotation at the given speed
            target.localRotation = Quaternion.RotateTowards(target.localRotation, targetRotation, speed * Time.deltaTime);

            // Yield until the next frame
            yield return null;
        }

        // Ensure the final rotation is exactly the target rotation
        target.localRotation = targetRotation;
    }

    IEnumerator RotateOverTime(float targetY, float duration, Transform door)
    {
        // Initiale Rotation speichern
        Quaternion startRotation = door.rotation;
        // Zielrotation berechnen
        Quaternion endRotation = Quaternion.Euler(door.eulerAngles.x, targetY, door.eulerAngles.z);

        // Zeitvariablen initialisieren
        float elapsedTime = 0;

        // Solange die Zeit noch nicht abgelaufen ist
        while (elapsedTime < duration)
        {
            // Interpoliere zwischen der Start- und Zielrotation
            door.rotation = Quaternion.Slerp(startRotation, endRotation, elapsedTime / duration);
            // Verstrichene Zeit erhöhen
            elapsedTime += Time.deltaTime;
            // Warte bis zum nächsten Frame
            yield return null;
        }

        //// Am Ende sicherstellen, dass die Zielrotation exakt gesetzt wird
        //transform.rotation = endRotation;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolWalk : MonoBehaviour
{
    private void Update()
    {
        MoveWithPistol();
    }

    private void MoveWithPistol()
    {
        // Eingaben sammeln
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Bewegungsrichtung berechnen
        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);


        transform.position = transform.position + movement * 6 * Time.deltaTime;
        // Spieler bewegen
        //transform.Translate(movement * 3 * Time.deltaTime, Space.World);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepDistance : MonoBehaviour
{
    public bool IsPlayer;

    private Vector3 lastPosition;

    void Update()
    {
        // Letzte Position speichern
        lastPosition = transform.position;
    }

    void Start()
    {
        lastPosition = transform.position;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!IsPlayer && collision.transform.tag == "Player")
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            transform.position = lastPosition;
        }
        else if(IsPlayer && collision.transform.tag == "Enemy")
        {
            transform.position = lastPosition;
        }     
    }
}

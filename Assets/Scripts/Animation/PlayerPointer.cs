using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPointer : MonoBehaviour
{
    public Transform HipsTransform;

    // Update is called once per frame
    void Update()
    {
        transform.position = HipsTransform.position;
    }
}

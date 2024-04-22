using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform PlayerTransform;

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position,new Vector3(PlayerTransform.position.x, PlayerTransform.position.y +15 , PlayerTransform.position.z-7),0.2f);
    }
}

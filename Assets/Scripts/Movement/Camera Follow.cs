using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform PlayerTransform;

    public float CameraDistance;
    public float ZOffset;

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position,new Vector3(PlayerTransform.position.x, PlayerTransform.position.y + CameraDistance, PlayerTransform.position.z- ZOffset),0.2f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachToBody : MonoBehaviour
{
    public GameObject target;
    void Start()
    {
        gameObject.GetComponent<SkinnedMeshRenderer>().bones = target.GetComponent<SkinnedMeshRenderer>().bones;
        gameObject.GetComponent<SkinnedMeshRenderer>().rootBone = target.GetComponent<SkinnedMeshRenderer>().rootBone;
    }
}

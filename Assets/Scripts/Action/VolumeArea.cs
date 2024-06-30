using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeArea : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            StartCoroutine(PostProcessingManager.Instance.ActivateVolume(PostProcessingManager.Instance.Bar, 0.83f));
            StartCoroutine(PostProcessingManager.Instance.DeactivateVolume(PostProcessingManager.Instance.World));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(PostProcessingManager.Instance.ActivateVolume(PostProcessingManager.Instance.World, 0.83f));
            StartCoroutine(PostProcessingManager.Instance.DeactivateVolume(PostProcessingManager.Instance.Bar));
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundArea : MonoBehaviour
{
    public AudioSource _AudioSource;

    void Start()
    {
        _AudioSource = GetComponent<AudioSource>();
        _AudioSource.loop = true;
        _AudioSource.playOnAwake = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            _AudioSource.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _AudioSource.Stop();
        }
    }
}

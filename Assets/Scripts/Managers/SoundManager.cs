using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource _AudioSource;
    public static SoundManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _AudioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip audioClip)
    {
        _AudioSource.clip = audioClip;
        _AudioSource.Play();
    }

    public void StopSound()
    {
        _AudioSource.Stop();
    }
}

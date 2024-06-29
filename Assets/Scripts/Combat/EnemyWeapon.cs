using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public EHitType WeaponType;

    public AudioSource SliceSFX;

    public int Damage;

    private AudioSource _AudioSource;
    public List<AudioClip> ÁudioClips = new List<AudioClip>();

    private void Start()
    {
        SliceSFX = GetComponent<AudioSource>();
        var x = FindObjectOfType<Defence>();
        _AudioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (!FindObjectOfType<Defence>().IsDoingDash)
            {
                FindObjectOfType<PlayerAnimation>().PlayHitAnimation();

                // SliceSFX.pitch = Random.Range(0.9f, 1.1f);

                // //StartCoroutine(Camera.main.GetComponent<CameraShake>().Shake(0.2f,0.3f));
                //SliceSFX.Stop();
                // SliceSFX.Play();

                PlayRandomSound();

                FindObjectOfType<Defence>().ScratchParticleSystem.Play();

                FindObjectOfType<PlayerStatus>().ChangeHealth(-Damage);

                StartCoroutine(PlayerHitStun());
            } 
        }
    }

    public void PlayRandomSound()
    {
        _AudioSource.Stop();
        _AudioSource.clip = ÁudioClips[Random.Range(0, ÁudioClips.Count)];
        _AudioSource.Play();
    }

    private IEnumerator PlayerHitStun()
    {
        FindObjectOfType<ThirdPersonController>().MoveSpeed = 0;
        yield return new WaitForSeconds(1f);
        FindObjectOfType<ThirdPersonController>().MoveSpeed = 6;
    }
}

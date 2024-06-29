using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForWater : MonoBehaviour
{

    public PlayerAnimation _PlayerAnimation;
    public Transform PlayerTransform;
    // Start is called before the first frame update
    void Start()
    {
        PlayerTransform =_PlayerAnimation.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector3(PlayerTransform.position.x, PlayerTransform.position.y +1, PlayerTransform.position.z); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Water")
        {
            _PlayerAnimation._AudioSource.Stop();
            _PlayerAnimation._AudioSource.clip = _PlayerAnimation.RunningInWater;
            _PlayerAnimation.IsInWater = true;
            _PlayerAnimation._AudioSource.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Water")
        {
            _PlayerAnimation._AudioSource.Stop();
            _PlayerAnimation.IsInWater = false;
            _PlayerAnimation._AudioSource.clip = _PlayerAnimation.Running;
            _PlayerAnimation._AudioSource.Play();
        }
    }
}

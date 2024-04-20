using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableTorch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<ParticleSystem>().enableEmission = false;
        GetComponent<Light>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GetComponent<ParticleSystem>().enableEmission = true;
            GetComponent<Light>().enabled = true;
        }
    }
}

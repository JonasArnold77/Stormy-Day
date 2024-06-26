using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMarker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Assuming you have a reference to the Renderer component of the object
        Renderer renderer = GetComponent<Renderer>();

        // Set the render queue to a high value to ensure it renders last
        renderer.material.renderQueue = 4000;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

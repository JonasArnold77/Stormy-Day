using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAiManager : MonoBehaviour
{
    public GameObject player; // Reference to the player GameObject
    public GameObject YourPrefab; 
    public int numberOfPositions = 6; // Number of positions in the half circle
    public float radius = 5f; // Radius of the half circle
    public float angleOffset = 0f;

    public List<GameObject> enemies = new List<GameObject>();

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.X))
        //{
            PlacePositionsInHalfCircle(enemies);
        //}
        
    }

    private void PlacePositionsInHalfCircle(List<GameObject> _enemies)
    {
        if (player == null || _enemies.Count <= 0)
        {
            Debug.LogWarning("Player reference not set or invalid number of positions.");
            return;
        }

        Vector3 playerPosition = player.transform.position;
        Vector3 playerForward = player.transform.forward;

        // Calculate the angle step between positions
        float angleStep = 180f / (_enemies.Count - 1);

        for (int i = 0; i < _enemies.Count; i++)
        {
            // Calculate the angle based on the player's forward direction and offset
            float angle = (angleStep * i) - 90f + angleOffset;
            Vector3 offset = Quaternion.Euler(0, angle, 0) * playerForward;

            // Calculate the position based on the player's position, radius, and offset
            Vector3 position = playerPosition + offset.normalized * radius;

            // Instantiate or place your objects at 'position'
            // For example, you can instantiate a prefab and position it here.
            // Replace 'YourPrefab' with the actual prefab you want to use.
            //GameObject spawnedObject = Instantiate(YourPrefab, position, Quaternion.identity);

            _enemies[i].GetComponent<NavMeshAgent>().destination = position;

            // Optionally, you can parent the spawned objects to keep the hierarchy clean.
            //spawnedObject.transform.SetParent(transform);
        }
    }
}

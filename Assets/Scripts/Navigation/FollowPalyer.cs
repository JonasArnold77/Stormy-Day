using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowPalyer : MonoBehaviour
{
    private Transform player;
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    //void Update()
    //{
    //    agent.destination = GetRandomTargetPos(10,20);   
    //}


    Vector3 GetRandomTargetPos(float minRadius, float maxRadius)
    {
        Vector2 rndPos = new Vector2(3,3);
        rndPos += rndPos.normalized * 3;
        return new Vector3(player.position.x + rndPos.x, player.position.y, player.position.z + rndPos.y);
    }

}

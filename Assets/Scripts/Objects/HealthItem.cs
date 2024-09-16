using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : MonoBehaviour
{
    private GameObject Player;
    private float HealthAmount = 20;



    private void Start()
    {
        Player = FindObjectOfType<ThirdPersonController>().gameObject;
        GenerateItem();
    }

    public void GenerateItem()
    {
        var percentageAmount = Random.Range(0.4f, 1f);
        transform.localScale = transform.localScale * percentageAmount;
        HealthAmount = HealthAmount * percentageAmount;
    }

    public void Update()
    {
        if (Vector3.Distance(transform.position, Player.transform.position)<6)
        {
            FollowPlayer();
        }

        if (Vector3.Distance(transform.position, Player.transform.position) < 2)
        {
            Destroy(gameObject);
        }
    }

    public void FollowPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, 0.5f);
    }
}

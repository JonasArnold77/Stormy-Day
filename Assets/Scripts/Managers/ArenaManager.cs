using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ArenaManager : MonoBehaviour
{
    public Transform LootPositionObj;
    public List<LootObject> AllLootObjects = new List<LootObject>();
    public List<GameObject> Enemies = new List<GameObject>();

    public GameObject NormalEnemy;
    public GameObject BossEnemy;

    public int Counter;

    public int BossCounter;
    public int BossWaiter = 6;

    // Start is called before the first frame update
    void Start()
    {
        AllLootObjects = FindObjectsOfType<LootObject>().ToList();
        StartCoroutine(GenerateEnemyCoroutineLoop());
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            GenerateLoot();
        }
    }

    public IEnumerator GenerateEnemyCoroutineLoop()
    {
        for(int i = 5; i >= 0; i--)
        {
            yield return new WaitForSeconds(1);
        }

        Counter++;
        BossCounter++;

        if(BossCounter >= BossWaiter)
        {
            Enemies.Add(GenerateEnemy(BossEnemy));
            BossCounter = 0;
        }else
        {
            var amount = Random.Range(1,3);

            for(int i = 0; i<amount; i++) 
            {
                Enemies.Add(GenerateEnemy(NormalEnemy));
            }
        }

        yield return new WaitUntil(() => !Enemies.Any(e => !e.GetComponent<EnemyAnimation>().IsDead));

        foreach (var e in Enemies)
        {
            Destroy(e);
        }

        Enemies.Clear();
        GenerateLoot();
        StartCoroutine(GenerateEnemyCoroutineLoop());
    }

    public GameObject GenerateEnemy(GameObject enemy) 
    {
        return Instantiate(enemy, position: LootPositionObj.position, Quaternion.identity);
    }

    public void GenerateLoot()
    {
        Instantiate(AllLootObjects[Random.Range(0, AllLootObjects.Count-1)], position: LootPositionObj.position,Quaternion.identity);
    }
}

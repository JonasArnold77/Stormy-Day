using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Next : MonoBehaviour
{
    public GameObject[] objects;
    int currntObject = 0;

    private void Start()
    {
        objects[currntObject].SetActive(true);
    }


    public void NextObject()
    {
        objects[currntObject].SetActive(false);
        if (currntObject < 9)
        {
            currntObject += 1;
            
        }
        else
        {
            currntObject = 0;
        }

        objects[currntObject].SetActive(true);
    }
}

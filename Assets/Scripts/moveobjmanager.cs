
using System.Collections.Generic;
using UnityEngine;

public class moveobjmanager : MonoBehaviour
{
    [HideInInspector]public int chanceOfSpawning;

    private List<GameObject> movingObjects=new List<GameObject>();

    private void Awake()
    {

        foreach(Transform child in transform)
        {
            movingObjects.Add(child.gameObject);
        }


        
    }

    private void Start()
    {
        chanceOfSpawning = Random.Range(0, 2);

        for(int i = 0; i < movingObjects.Count; i++)
        {
            if (chanceOfSpawning == i)
            {
                movingObjects[i].SetActive(true);
            }
            else
            {
                movingObjects[i].SetActive(false);
            }
        }
    }

    public void ResetMovingObject()
    {
        chanceOfSpawning = Random.Range(0, 2);
        for (int i = 0; i < movingObjects.Count; i++)
        {
            if (chanceOfSpawning == i)
            {
                movingObjects[i].SetActive(true);
            }
            else
            {
                movingObjects[i].SetActive(false);
            }
        }
    }




}

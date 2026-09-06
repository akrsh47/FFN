
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class initSpawnerManhole : MonoBehaviour
{
    private SpriteRenderer sp;
    


    [SerializeField] private GameObject objectToSpawn;
    private SpriteRenderer objectSp;

    private Vector3 randomPosition;

    private int spawnCondition;

    

    private void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
        objectSp = objectToSpawn.GetComponent<SpriteRenderer>();
        randomPosition = new Vector3(
            UnityEngine.Random.Range(sp.bounds.min.x + objectSp.bounds.extents.x, sp.bounds.max.x - objectSp.bounds.extents.x),// bounds.extends used for getting half size of object
            UnityEngine.Random.Range(sp.bounds.min.y + objectSp.bounds.extents.y, sp.bounds.max.y - objectSp.bounds.extents.y),// so that object spawns inside the parent and not half spawned into the parent
            transform.position.z
            );
        spawnCondition = UnityEngine.Random.Range(0, 5);// the chance for object spawning is 1 in 5
        
    }

    private void Start()
    {
        
        

        if (spawnCondition == 1)
        {
            Instantiate(objectToSpawn, randomPosition, quaternion.identity);
        }
    }


}

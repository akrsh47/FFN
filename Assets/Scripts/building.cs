using System;
using UnityEditor.PackageManager;
using UnityEngine;

public class building : MonoBehaviour
{

    private float interval;
    [SerializeField]private Sprite[] buildingSprites;
    private SpriteRenderer sp;
    public static event Action dropGarbage;// event declared

    private float timer;


    
    [HideInInspector]public int garbagePerBuilding;
    [SerializeField]private GameObject[] garbageBags;
    [HideInInspector]public int randomGarbage;

    private void Awake()
    {
        sp = GetComponent<SpriteRenderer>();


        
    }
    void Start()
    {
        sp.sprite = buildingSprites[0];
        interval = UnityEngine.Random.Range(3f, 5f);
        timer = 1f;

        if (gameObject.tag == "b1")
        {

            garbagePerBuilding = 4;
            garbageBags = new GameObject[garbagePerBuilding];
            randomGarbage = UnityEngine.Random.Range(0, garbagePerBuilding);

        }

        for (int i = 0; i < garbagePerBuilding; i++)
        {
            garbageBags[i]=transform.GetChild(i).gameObject;
        }
    }

    void Update()
    {
        StartTimer();
        if (timer > interval)
        {
            changeBuilding();
            Invoke(nameof(ResetTimer),interval);
        }



    }

    void ResetTimer()
    {
        timer =1f;
        sp.sprite=buildingSprites[0];
        interval = UnityEngine.Random.Range(3f, 5f);
        randomGarbage = UnityEngine.Random.Range(0, garbagePerBuilding);

    }
    void StartTimer()
    {
        timer += Time.deltaTime;
    }

    void changeBuilding()
    {
        sp.sprite = buildingSprites[1];
        dropGarbage?.Invoke();// event fires here

    }

    void dropingGarbage()// function to happen in the event
    {
        garbageBags[randomGarbage].SetActive(true);
    }

    private void OnEnable()// when object enabled add this function to list of events happening
    {
        building.dropGarbage += dropingGarbage;
    }

    private void OnDisable()// when object disabled remove this function to list of events happening
    {
        building.dropGarbage -= dropingGarbage;
    }
}
using System;
using UnityEngine;

public class building : MonoBehaviour
{

    private float interval;
    [SerializeField]private Sprite[] buildingSprites;
    private SpriteRenderer sp;
    public static event Action dropGarbage;

    private float timer;


    
    private int garbagePerBuilding;
    [SerializeField]private GameObject[] garbageBags;
    private int randomGarbage;

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
        dropGarbage?.Invoke();

    }

    void dropingGarbage()
    {
        garbageBags[randomGarbage].SetActive(true);
    }

    private void OnEnable()
    {
        building.dropGarbage += dropingGarbage;
    }

    private void OnDisable()
    {
        building.dropGarbage -= dropingGarbage;
    }
}
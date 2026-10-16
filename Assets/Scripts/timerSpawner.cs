
using UnityEngine;

public class timerSpawner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float timer;
    [SerializeField] private float[] intervalRange = new float[2];

    private float interval;

    [SerializeField] private GameObject spawnItem;
    [SerializeField] private bool spawnCondition;

    private void Start()
    {
        interval = Random.Range(intervalRange[0], intervalRange[1]);


    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > interval && spawnCondition)
        {
            spawnItem.SetActive(true);
            timer = 0;
        }
    }
}

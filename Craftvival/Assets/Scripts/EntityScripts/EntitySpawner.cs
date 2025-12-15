using System.Collections;
using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
    public DayNightCycle dayNightCycle;

    public GameObject entityPrefab1, entityPrefab2;

    public float spawnInterval = 10f;

    public void Start()
    {
        StartCoroutine(SpawnTimer());
    }

    public void SpawnEntity()
    {
        if (dayNightCycle.isDay)
        {
            Instantiate(entityPrefab1, transform.position, Quaternion.identity);
        }
        else if (dayNightCycle.isNight)
        {
            Instantiate(entityPrefab2, transform.position, Quaternion.identity);
        }
    }

    private IEnumerator SpawnTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnEntity();
        }
    }
}

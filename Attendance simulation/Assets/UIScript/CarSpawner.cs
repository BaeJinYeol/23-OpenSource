using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject[] carPrefabs;
    public float spawnIntervalMin = 2f;
    public float spawnIntervalMax = 8f;

    void Start()
    {
        InvokeRepeating("SpawnCar", 0f, Random.Range(spawnIntervalMin, spawnIntervalMax));
    }

    void SpawnCar()
    {
        int randomIndex = Random.Range(0, carPrefabs.Length);

        Instantiate(carPrefabs[randomIndex], transform.position, Quaternion.identity);
    }
}

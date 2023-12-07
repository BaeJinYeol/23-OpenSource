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

        GameObject car = Instantiate(carPrefabs[randomIndex], transform.position, Quaternion.identity);

        //car.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, moveSpeed);
        //car.GetComponent<Rigidbody>().velocity = transform.forward * moveSpeed;

        //Destroy(car, 20f);
    }
}

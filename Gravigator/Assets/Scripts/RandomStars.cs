using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomStars : MonoBehaviour
{
    [Header("Variables")]
    public int spawnAmount;

    [Header("Dependencies")]
    public GameObject[] starPrefabs;
    public Transform lowerBounds;
    public Transform upperBpunds;

    private void Start()
    {
        SpawnStars();
    }

    void SpawnStars()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            InstantiateStar();
        }
    }

    void InstantiateStar()
    {
        int rndPrefab = Random.Range(0, starPrefabs.Length);
        float rndX = Random.Range(lowerBounds.position.x, upperBpunds.position.x);
        float rndY = Random.Range(lowerBounds.position.y, upperBpunds.position.y);
        Vector3 rndPos = new Vector3(rndX, rndY, 0);

        Instantiate(starPrefabs[rndPrefab], rndPos, Quaternion.identity);
    }
}

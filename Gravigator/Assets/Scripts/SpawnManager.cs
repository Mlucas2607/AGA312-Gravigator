using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    [Header("Spawn Variables")]
    public float spawnDelay = 0.5f, baseDelay = 20f;
    public int difficulty = 0;
    private float nextTimeToSpawn;
    private int enemyCount;

    [Header("Spawn Dependencies")]
    public Transform[] spawnPos;
    public GameObject enemyPrefab;

    [Header("UI Dependencies")]
    public Text difficultyText;

    //Enemy Stats
    private int health = 100, baseHealth = 100;
    private float fireRate = 0.5f, baseFirerate = 0.5f;
    private int bulletDamage = 10, baseDamage = 10;

    private void Update()
    {
        SpawnCheck();
        ChangeDifficulty();
    }

    void SpawnCheck()
    {
        if (Time.time < nextTimeToSpawn)
            return;

        nextTimeToSpawn = Time.time + spawnDelay;
        SpawnEnemy();
    }
    void ChangeDifficulty()
    {
        int a = enemyCount / 5;
        health = baseHealth + (a * 10);
        fireRate = baseFirerate - (a / 100);
        bulletDamage = baseDamage * a;
        spawnDelay = baseDelay - a;
        difficultyText.text = "Level:" + a;
    }
    void SpawnEnemy()
    {
        enemyCount++;
        Transform pos = spawnPos[Random.Range(0, spawnPos.Length)];
        GameObject enemy = Instantiate(enemyPrefab, pos);
        enemy.GetComponent<Enemy>().health = health;
        enemy.GetComponent<Enemy>().fireRate = fireRate;
        enemy.GetComponent<Enemy>().bulletDamage = bulletDamage;
    }
}

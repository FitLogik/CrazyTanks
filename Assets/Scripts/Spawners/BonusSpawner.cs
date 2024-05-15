using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] bonusPrefabs;
    [SerializeField] float spawnInterval = 5f;
    [SerializeField] float repeatSpawnInterval = 10f;
    [SerializeField] Vector2 spawnPosition = new Vector2(0, 3);

    void Start()
    {
        InvokeRepeating(nameof(SpawnBonus), spawnInterval, repeatSpawnInterval);
    }

    void SpawnBonus()
    {
        int randomIndex = Random.Range(0, bonusPrefabs.Length);
        Instantiate(bonusPrefabs[randomIndex], spawnPosition, Quaternion.identity);
    }
}

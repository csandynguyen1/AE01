using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSpawners : MonoBehaviour
{

    [SerializeField]
    private GameObject[] enemyPrefabs;  // Array of enemy prefabs to spawn

    [SerializeField]
    private Transform[] spawnerLocations;  // Array of Transforms where enemies will spawn

    // Method to trigger the spawning of enemies
    public void SpawnEnemies()
    {
        foreach (Transform spawner in spawnerLocations)
        {
            SpawnEnemyAtLocation(spawner);
        }
    }

    private void SpawnEnemyAtLocation(Transform spawner)
    {
        if (enemyPrefabs.Length == 0)
        {
            Debug.LogError("No enemy prefabs assigned!");
            return;
        }

        // Select a random enemy prefab
        int index = Random.Range(0, enemyPrefabs.Length);
        GameObject enemyPrefab = enemyPrefabs[index];

        // Instantiate the enemy at the spawner's position and rotation
        Instantiate(enemyPrefab, spawner.position, spawner.rotation);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H)) // Replace this with your condition.
        {
            SpawnEnemies();
        }
    }
}

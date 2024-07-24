using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FissureSpawner : MonoBehaviour
{
    public GameObject fissurePrefab; // Assign the animated fissure prefab
    public Transform target; // The target, typically the player
    public float spawnRate = 0.5f; // Time between spawns
    public float segmentLength = 1.0f; // Length of each fissure segment
    public float fissureLifetime = 5f; // How long each fissure segment lasts before disappearing
    private Vector3 lastSpawnPoint;
    private Vector3 direction; // Direction towards the initial target position
    private bool isSpawning = false; // Control flag to start/stop spawning
    private int segmentsSpawned = 0; // Counter for the number of segments spawned
    public int maxSegments = 5; // Maximum number of segments to spawn
    private AudioSource audioSource;
    public AudioClip fissureSound;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        // Check if a specific key is pressed to start spawning the fissure
        if (Input.GetKeyDown(KeyCode.K)) // Press Space to start the fissure
        {
            //if (!isSpawning)
           // {
               // StartFissure();
               // isSpawning = true; // Prevent further initialization unless stopped and restarted
                //segmentsSpawned = 0;
            //}
        }

        // Once spawning has started, continue spawning at intervals
        if (isSpawning && Time.timeSinceLevelLoad % spawnRate < Time.deltaTime)
        {
            SpawnFissureSegment();
        }

    }

    public void ActivateFissure()
    {
        if (!isSpawning)
        {
            audioSource.PlayOneShot(fissureSound);
            StartFissure();
            isSpawning = true;
            segmentsSpawned = 0;
        }

    }

    void StartFissure()
    {
        if (target == null)
        {
            Debug.LogError("Target not assigned on FissureSpawner.");
            return;
        }

        // Initialize the fissure direction and start point
        lastSpawnPoint = transform.position; // Start the fissure from this object's position
        direction = (target.position - lastSpawnPoint).normalized; // Fixed direction based on the initial target position
    }

    private void SpawnFissureSegment()
    {
        if (segmentsSpawned >= maxSegments)
        {
            isSpawning = false; // Stop spawning after reaching the max number of segments
            return;
        }

        Vector3 spawnPosition = lastSpawnPoint + direction * segmentLength;
        GameObject fissure = Instantiate(fissurePrefab, spawnPosition, Quaternion.LookRotation(Vector3.forward, direction));
        lastSpawnPoint = spawnPosition; // Update the last spawn position
        segmentsSpawned++; // Increment the segment counter
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaterLevelTransition : MonoBehaviour
{
    public string WaterLevel = "Water_Level"; // Name of the scene to transition to

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collider is tagged as "Player"
        if (collision.CompareTag("Player"))
        {
            // Transition to the specified scene
            SceneManager.LoadScene(WaterLevel);
        }
    }
}

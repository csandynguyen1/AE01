using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FireTransition : MonoBehaviour
{
    // Start is called before the first frame update
    public string Arena = "Fire Boss Arena"; // Name of the scene to transition to

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collider is tagged as "Player"
        if (collision.CompareTag("Player"))
        {
            // Transition to the specified scene
            SceneManager.LoadScene(Arena);
        }
    }
}

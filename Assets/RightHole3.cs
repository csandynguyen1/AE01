using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RightHole3 : MonoBehaviour
{
    // Start is called before the first frame update
    public string Level = "Air Boss Stage"; // Name of the scene to transition to

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collider is tagged as "Player"
        if (collision.CompareTag("Player"))
        {
            // Transition to the specified scene
            SceneManager.LoadScene(Level);
        }
    }
}

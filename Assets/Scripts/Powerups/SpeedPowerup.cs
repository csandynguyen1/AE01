using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpeedPowerup : MonoBehaviour
{
    public float speedBoost = 1f;
    public string nextLevel;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("powerup collision");
        if (other.CompareTag("Player"))
        {
            Debug.Log("player touched powerup");
            PowerupManager.Instance.playerSpeed += speedBoost;
            // Optionally, update the health bar immediately if needed
            PlayerControllerRedo player = other.GetComponent<PlayerControllerRedo>();
            player.setSpeed(PowerupManager.Instance.playerSpeed);

            // Destroy the power-up object
            DestroyPowerups();
            EnterNextLevel();
        }
    }
    private void DestroyPowerups()
    {
        Debug.Log("powerups deleted");
        GameObject[] powerups = GameObject.FindGameObjectsWithTag("Powerup");
        foreach (GameObject powerup in powerups)
        {
            Destroy(powerup);
        }
    }

    private void EnterNextLevel()
    {
        if(nextLevel != null)
        {
            SceneManager.LoadScene(nextLevel);
        }
        
    }
}

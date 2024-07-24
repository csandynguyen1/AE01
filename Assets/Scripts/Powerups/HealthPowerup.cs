using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthPowerup : MonoBehaviour
{
    public float healthBoost = 5f;
    public string nextLevel;


    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("powerup collision");
        if (other.CompareTag("Player"))
        {
            Debug.Log("player touched powerup");
            PowerupManager.Instance.playerHealth += healthBoost;
            // Optionally, update the health bar immediately if needed
            DamageableCharacter player = other.GetComponent<DamageableCharacter>();
            player.healthBar.setMaxHealth((int)PowerupManager.Instance.playerHealth);
            player.healthBar.setHealth((int)PowerupManager.Instance.playerHealth);
            player.setHealth(PowerupManager.Instance.playerHealth);

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
        if (nextLevel != null)
        {
            SceneManager.LoadScene(nextLevel);
        }
    }
}

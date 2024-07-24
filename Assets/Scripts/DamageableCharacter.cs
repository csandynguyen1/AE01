using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DamageableCharacter : MonoBehaviour, IDamageable
{
    public float _health = 9;
    public bool isAlive = true;
    public HPBar healthBar;

    public bool damageable = true; 
    Animator animator;
    Rigidbody2D rb;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isAlive", isAlive);
        rb = GetComponent<Rigidbody2D>();
        if(gameObject.CompareTag("Player"))
        {
            _health = PowerupManager.Instance.playerHealth;
            if (healthBar != null)
            {
                healthBar.setMaxHealth((int)_health);
                healthBar.setHealth((int)_health);
            }
        }
    }
    public void setHealth(float health)
    {
        _health = health;
    }

    // Health property for any Damageable
    public float Health {
        set{
            if( value < _health && damageable == true)
            {
                animator.SetTrigger("hit");
            }
            _health = value;

            if( value < 0 && damageable == true)
            {
            animator.SetTrigger("hit");
            }
            if(_health <= 0 && damageable == true)
            {
                animator.SetBool("isAlive", false);
                isAlive = false;
            }
        }
        get {
            return _health;
        }
    }

    public void SetDamageable( bool setting)
    {
        damageable = setting;
    }

    // When hit by actor
    public void OnHit(float damage, GameObject attacker)
    {
        Debug.Log("Attack hit for " + damage);
        Health -= damage;
        if (healthBar != null)
        {
            healthBar.setHealth((int)Health);
        }
    }
    public void OnHit(float damage)
    {
        if (damageable == true)
            OnHit(damage, null);
    }

    // Remove the game object upon death
    public void OnObjectDestroy() 
    {
        // remove enemy from scene
        Debug.Log("tap in");
        Destroy(gameObject);

        
        
    }

}

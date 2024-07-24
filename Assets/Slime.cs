using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public float damage = 1;
    public float moveSpeed = 1.5f;
    public bool canMove = true;

    public DetectionZone detectionZone;
    
    private Rigidbody2D rb;
    public Animator animator;
    SpriteRenderer spriteRenderer;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetBool("isMoving", false);
    }

    void FixedUpdate()
    {
        if(canMove)
        {
            
            if(detectionZone.detectedObjs.Count > 0)
            {
                Debug.Log("I found the player");
                animator.SetBool("isMoving", true);
                // Calculate vector 
                Vector2 direction = (detectionZone.detectedObjs[0].transform.position - transform.position).normalized;
                // move towards object
                rb.AddForce(direction * moveSpeed, ForceMode2D.Impulse);
                
                
            }
            else 
            {
                animator.SetBool("isMoving", false);
            }    
            
        }
        
    }

    // Deal damage to object if enter collider box
    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Player"))
        {
            Debug.Log("I ran into the player");
            animator.SetTrigger("attack");
            IDamageable damageable = col.collider.GetComponent<IDamageable>();
            damageable?.OnHit(damage);
        }
    }

    public void LockMovement()
    {
        canMove = false;
    }

    public void UnlockMovement() 
    {
        canMove = true;
    }

}

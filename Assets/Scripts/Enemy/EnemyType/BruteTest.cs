using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BruteTest : MonoBehaviour
{
    public float damage = 1;
    public float moveSpeed = 1.5f;
    public bool canMove = true;

    public DetectionZone detectionZone;

    private Rigidbody2D rb;
    public Animator animator;
    SpriteRenderer spriteRenderer;
    public Transform enemyGFX;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetBool("isMoving", false);
    }

    void FixedUpdate()
    {

        if (canMove)
        {

            if (detectionZone.detectedObjs.Count > 0)
            {
                animator.SetBool("isMoving", true);
                Vector2 direction = (detectionZone.detectedObjs[0].transform.position - transform.position).normalized;
                rb.AddForce(direction * moveSpeed, ForceMode2D.Impulse);
                if (direction.x > 0)
                {
                    enemyGFX.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                }
                else if (direction.x < 0)
                {
                    enemyGFX.localScale = new Vector3(-1.5f, 1.5f, 1.5f);
                }
            }
            else
            {
                animator.SetBool("isMoving", false);
            }

        }
    }
    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Player"))
        {
            animator.SetTrigger("attack");
            IDamageable damageable = col.collider.GetComponent<IDamageable>();
            damageable.OnHit(damage);
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

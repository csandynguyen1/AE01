using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostESS : MonoBehaviour
{
    public float damage = 1;
    private Rigidbody2D rb;
    public Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("I ran into the player");
        IDamageable damageable = col.collider.GetComponent<IDamageable>();
        damageable?.OnHit(damage);
    }
}

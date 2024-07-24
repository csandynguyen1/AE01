using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceShard : MonoBehaviour
{
    public float speed = 8f;
    public int damage = 10;
    public float slowEffectDuration = 2f;

    public void Cast(Transform firePoint)
    {
        var iceShard = Instantiate(this, firePoint.position, firePoint.rotation);
        iceShard.GetComponent<Rigidbody2D>().velocity = firePoint.right * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var enemy = collision.collider.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            enemy.ApplySlow(slowEffectDuration);
        }
        Destroy(gameObject);
    }
}


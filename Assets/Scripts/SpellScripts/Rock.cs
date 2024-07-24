using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public float speed = 8f;
    public int damage = 15;
    public float knockbackForce = 5f;

    public void Cast(Transform firePoint)
    {
        var rock = Instantiate(this, firePoint.position, firePoint.rotation);
        rock.GetComponent<Rigidbody2D>().velocity = firePoint.right * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var enemy = collision.collider.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            // Apply knockback
            Vector2 knockbackDirection = (enemy.transform.position - transform.position).normalized;
            enemy.GetComponent<Rigidbody2D>().AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
        }
        Destroy(gameObject);
    }
}

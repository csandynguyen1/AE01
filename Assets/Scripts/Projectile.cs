using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float bulletSpeed;
    public float lifeTime;
    public float damage = 1;

    public void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
    }

    void FixedUpdate()
    {
        transform.Translate(Vector2.right * bulletSpeed * Time.deltaTime);
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("eye collision detected");
        Debug.Log(col.name);
        if (col.CompareTag("Player"))
        {
            Debug.Log("Attempting damage");
            IDamageable damageable = col.GetComponent<IDamageable>();
            // Deal damage
            damageable.OnHit(damage);
        }
    }
}

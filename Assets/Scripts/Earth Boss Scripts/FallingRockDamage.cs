using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRockDamage : MonoBehaviour
{
    public int damage = 1;
    private bool hasImpacted = false;
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasImpacted) return; // Ignore collisions after the first impact

        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.OnHit(damage);
            hasImpacted = true; // Mark as impacted to prevent further damage

            // After damage is dealt, you can disable the rock's collider or destroy the rock
            // GetComponent<Collider2D>().enabled = false;
            // Destroy(gameObject);
        }
    }
}

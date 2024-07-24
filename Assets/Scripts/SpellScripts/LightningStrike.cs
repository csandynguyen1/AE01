using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningStrike : MonoBehaviour
{
    public float range = 10f;
    public int damage = 30;

    public void Cast(Transform firePoint)
    {
        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, firePoint.right, range);
        if (hit.collider != null)
        {
            var enemy = hit.collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
        // Optionally instantiate a visual effect or destroy if it's a one-time use object
        Destroy(gameObject);
    }
}

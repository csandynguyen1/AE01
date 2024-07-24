using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaDamage : MonoBehaviour
{

    public float damage = 1;
    

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if( collider.tag == "Player")
        {
            Debug.Log("Player is too hot!");
            IDamageable damageable = collider.GetComponent<IDamageable>();
            // Deal damage
            damageable.OnHit(damage);
        }
    }
}

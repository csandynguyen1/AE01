using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public float damage = 3f;

    public float knockbackForce = 50f;
    private AudioSource audioSource;
    public AudioClip attackSound;

    public Collider2D swordCollider;
    Vector2 rightAttackOffest;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rightAttackOffest = transform.localPosition;
        Debug.Log("Updating damage");
        damage = PowerupManager.Instance.playerDamage;
    }
    

    public void AttackRight()
    {
        audioSource.PlayOneShot(attackSound);
        print("Right Attack");
        swordCollider.enabled = true;
        transform.localPosition = rightAttackOffest;
    }

    public void AttackLeft()
    {
        audioSource.PlayOneShot(attackSound);
        print("Left Attack");
        swordCollider.enabled = true;
        transform.localPosition = new Vector2(-rightAttackOffest.x, rightAttackOffest.y);
    }

    public void StopAttack()
    {
        swordCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if( collider.tag == "Enemy")
        {
            IDamageable damageable = collider.GetComponent<IDamageable>();
            // Deal damage
            damageable.OnHit(damage,transform.parent.gameObject);
        }
    }

}

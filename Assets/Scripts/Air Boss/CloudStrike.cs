using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudStrike : MonoBehaviour
{
    private Animator animator;
    public float minInterval = 2f;
    public float maxInterval = 5f;
    private float timer;
    public float damage = 2f;
    private BoxCollider2D boxCollider;
    private AudioSource audioSource;
    public AudioClip strikeSound;

    void Awake()
    {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        ScheduleNextStrike();
    }


    void Update()
    {
        timer -= Time.deltaTime;
        if(timer<=0)
        {
            TriggerLightning();
            ScheduleNextStrike();
        }
    }

    private void ScheduleNextStrike()
    {
        timer = Random.Range(minInterval, maxInterval);
    }

    private void TriggerLightning()
    {
        audioSource.PlayOneShot(strikeSound);
        animator.SetTrigger("Strike");
    }

    // Method to trigger eruption

    public void ApplyDamage()
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll(boxCollider.bounds.center, boxCollider.bounds.size, 0f);
        foreach (var hit in hits)
        {
            Debug.Log(hit.name);
            if (hit.CompareTag("Player") || hit.CompareTag("Enemy"))
            {
                Debug.Log("attempting to do damage");
                IDamageable damageable = hit.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    Debug.Log("doing damage");
                    damageable.OnHit(damage);
                }
                else
                {
                    Debug.Log("Couldn't find damageable");
                }
            }
        }
    }

    
}

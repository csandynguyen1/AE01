using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brute : MonoBehaviour
{
    public float damage = 1;
    public float moveSpeed = 500f;
    public float knockbackForce = 100f;

    public DetectionZone detectionZone;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (detectionZone.detectedObjs.Count > 0)
        {
            Vector2 direction = (detectionZone.detectedObjs[0].transform.position - transform.position).normalized;
            // move towards object
            rb.AddForce(direction * moveSpeed * Time.deltaTime);
        }
    }
}

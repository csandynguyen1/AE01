using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowballController : MonoBehaviour
{
    public float speed = 5f;
    public int damage = 1;
    private Transform player;
    private Vector2 target;
    private Vector3 offset = new Vector3(0, 0.5f, 0);
    // Start is called before the first frame update
    void Awake()
    {
        // Make the prefab itself inactive in visuals and physics
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
    }

    // Update is called once per frame
    void OnEnable()
    {
        // Unfreeze Rigidbody constraints for clones
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.None; // Unfreeze all constraints

        // Make the sprite visible for clones
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = true;
        // Add movement or other initialization logic here
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = ((player.transform.position-offset) - transform.position).normalized;
    }
    void Update()
    {
        // Move in the set direction every frame
        transform.position += (Vector3)target * speed * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Player hit by snowball");
        if (collision.CompareTag("Player"))
        {
            IDamageable damageable = collision.GetComponent<IDamageable>();
            // Deal damage
            damageable.OnHit(damage);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    
}

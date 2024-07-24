using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour
{
    public float speed = 5f;
    public int damage = 1;
    private Transform player;
    private Vector2 target;
    private HeatBar heatbar;
    // Start is called before the first frame update
    void Start()
    {
        GameObject heatBarObject = GameObject.FindGameObjectWithTag("HeatBar");
        if (heatBarObject != null)
        {
            heatbar = heatBarObject.GetComponent<HeatBar>();
            if (heatbar == null)
            {
                Debug.LogError("FireballController: HeatBar component not found on tagged object.");
            }
        }
        else
        {
            Debug.LogError("FireballController: No object with tag 'HeatBar' found in scene.");
        }
    }
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
        Vector3 offset = new Vector3(0, 0.5f, 0);
        target = (player.transform.position - transform.position-offset).normalized;
    }
    void Update()
    {
        // Move in the set direction every frame
        transform.position += (Vector3)target * speed * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Player hit by fireball");
        if (collision.CompareTag("Player"))
        {
            IDamageable damageable = collision.GetComponent<IDamageable>();
            // Deal damage
            heatbar.IncreaseHeat(6);
            damageable.OnHit(damage);
        }
        //else if(collision.CompareTag(""))
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    
}

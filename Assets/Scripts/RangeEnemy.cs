using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : MonoBehaviour
{
    public float moveSpeed;
    public Transform player;
    public Transform shotPoint;
    public Transform gun;

    public GameObject EnemyProjectile;

    public float followPlayerRange;
    private bool inRange;
    public float attackRange;

    public float startTimeBtwnShots;
    private float timeBtwnShots;

    private Rigidbody2D rb;
    public Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetBool("isMoving", false);
    }
    void Update()
    {
        Vector3 offset = new Vector3(0, 0.5f, 0);
        Vector3 shotOffset = new Vector3(0, 0.3f, 0);
        Vector3 diff = (player.position-offset) - gun.position;
        // diff.y -= 0.65f;
        float rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        gun.transform.rotation = Quaternion.Euler(0f, 0f, rotZ);

        if(Vector2.Distance(transform.position, player.position) < followPlayerRange && Vector2.Distance(transform.position, player.position) > attackRange )
        {
            inRange = true;
        }
        else
        {
            inRange = false;
        }

        if(Vector2.Distance(transform.position, player.position) <= attackRange )
        {
            if(timeBtwnShots <= 0)
            {
                animator.SetTrigger("summon");
                Instantiate(EnemyProjectile, transform.position-shotOffset, shotPoint.transform.rotation);
                timeBtwnShots = startTimeBtwnShots;
            }
            else
            {
                timeBtwnShots -= Time.deltaTime;
            }
        }
    }

    private void FixedUpdate()
    {
        if(inRange)
        {
            animator.SetBool("isMoving", true);
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, followPlayerRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere (transform.position, attackRange);
    }
}

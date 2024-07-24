using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : MonoBehaviour
{
    public int health;

    [HideInInspector]
    public Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health < 0)
        {
            Destroy(this.gameObject);
        }
    }
}

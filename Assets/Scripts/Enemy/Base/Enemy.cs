using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class Enemy : MonoBehaviour
{
    public int health;

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;

        if (health < 0)
        {
            Destroy(gameObject);
        }
    }

    internal void ApplySlow(float slowEffectDuration)
    {
        throw new NotImplementedException();
    }
}

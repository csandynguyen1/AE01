using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summoner : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;

    public float timeToSummon;
    private float summonTime;
    public float health;
    public EnemyTest summonEnemy;

    public void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void Update()
    {
        if(Time.time >= summonTime)
        {
            summonTime = Time.time + timeToSummon;
            anim.SetTrigger("summon");
        }
    }

    public void Summon()
    {
        Instantiate(summonEnemy, transform.position, transform.rotation);
    }
}

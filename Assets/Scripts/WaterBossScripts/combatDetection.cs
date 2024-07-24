using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class combatDetection : MonoBehaviour
{
    public GameObject bossObject;
    private IBossAI bossAI;
    private void Awake()
    {
        bossAI = bossObject.GetComponent<IBossAI>();
        if (bossAI == null)
        {
            Debug.LogError("The assigned boss does not implement IBossAI!");
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            bossAI.StartCombat();
        }
    }
}

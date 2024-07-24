using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ringExpand : MonoBehaviour
{
    public float expandRate = 1f; // Rate at which the collider expands
    private CircleCollider2D circleCollider;
    private ParticleSystem particles;
    private float initialRadius = 2f;
    private float maxRadius = 29f;

    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        particles = GetComponent<ParticleSystem>();
    }


    public void playRing()
    {
        Debug.Log("Playing ring");
        particles.Play();
        StartCoroutine(RingRoutine());
    }

    IEnumerator RingRoutine()
    {
        while(circleCollider.radius < maxRadius)
        {
            circleCollider.radius += expandRate * Time.deltaTime;
            yield return null;
        }
        circleCollider.radius = 2f;
    }
}

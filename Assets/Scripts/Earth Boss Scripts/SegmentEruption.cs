using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentEruption : MonoBehaviour
{
    private Animator animator;
    public bool hasErupted = false;
    public LayerMask playerLayer; // Layer to detect the player
    public float damageRadius = 1.0f; // Radius within which damage is dealt
    public Vector2 damageBoxSize = new Vector2(1f,2.5f);
    public int damageAmount = 10; // Damage dealt to the player
    public Vector3 offset = new Vector3(0.5f, 0, 0);

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Method to trigger eruption
    public void Erupt()
    {
        animator.SetTrigger("Erupt"); // Assumes there is a trigger set up in the Animator
        CheckForDamage();
    }

    // Check and apply damage if player is within radius
    void CheckForDamage()
    {
        //Vector3 rotatedOffset = transform.rotation * offset; // Rotate the offset by the GameObject's rotation
        Vector3 boxCenter = transform.position + (Quaternion.Euler(0, 0, transform.eulerAngles.z) * offset); // Calculate the center of the box with the rotated offset
        Collider2D[] hits = Physics2D.OverlapBoxAll(boxCenter, damageBoxSize, transform.eulerAngles.z);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Player")) // Check if the hit object has the player tag
            {
                IDamageable damageable = hit.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    damageable.OnHit(damageAmount);
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        // To visualize the damage radius in the editor
        Matrix4x4 rotationMatrix = Matrix4x4.TRS(transform.position+ (Quaternion.Euler(0, 0, transform.eulerAngles.z) * offset), Quaternion.Euler(0f, 0f, transform.eulerAngles.z), Vector3.one);
        Gizmos.matrix = rotationMatrix;

        // To visualize the damage box area in the editor as a rectangle
        Gizmos.color = Color.red;
        // Draw a cube with a width of damageBoxSize.x and height of damageBoxSize.y
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(damageBoxSize.x, damageBoxSize.y, 0.01f)); // Z-dimension is small since it's 2D

        // Reset the Gizmos matrix after drawing
        Gizmos.matrix = Matrix4x4.identity;
    }
}

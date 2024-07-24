using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFallingRock : MonoBehaviour
{
    public GameObject rockPrefab;
    public Transform playerTransform;
    public GameObject shadowPrefab;
    public float rockOffset = 10f;
    public float fallDuration = 2f;
    public float damageRadius = 0.5f;
    private AudioSource audioSource;
    public AudioClip rockfallSound;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void SpawnRock()
    {
        Vector2 shadowPosition = new Vector2(playerTransform.position.x, playerTransform.position.y-0.7f); // Directly above the player
        GameObject shadowInstance = Instantiate(shadowPrefab, shadowPosition, shadowPrefab.transform.rotation);
        Vector2 rockPosition = shadowPosition + new Vector2(0, rockOffset);
        GameObject rockInstance = Instantiate(rockPrefab, rockPosition, Quaternion.identity);
        audioSource.PlayOneShot(rockfallSound);
        StartCoroutine(ManageFallingRock(rockInstance, shadowInstance, fallDuration));
    }

    private IEnumerator ManageFallingRock(GameObject rock,GameObject shadow, float time)
    {
        StartCoroutine(ShrinkShadowOverTime(shadow, time));
        yield return new WaitForSeconds(time);
        Collider2D rockCollider = rock.GetComponent<Collider2D>();
        Rigidbody2D rb = rock.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
            rb.gravityScale = 0; // Optionally, remove gravity influence.
            rockCollider.enabled = true;
            if (Vector2.Distance(rock.transform.position, playerTransform.position) <= damageRadius)
            {
                IDamageable damageable = playerTransform.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    damageable.OnHit(1); // Assuming 1 is the damage value
                }
            }
        }
        else
        {
            Debug.LogError("Rock does not have a Rigidbody2D component.");
        }
        Destroy(shadow);
    }

    private IEnumerator ShrinkShadowOverTime(GameObject shadow, float time)
    {
        float elapsedTime = 0;
        float minScaleFactor = 0.5f;
        Vector3 originalScale = shadow.transform.localScale;

        while (elapsedTime < time)
        {
            // Calculate the scale of the shadow based on the elapsed time.
            float scaleFactor = 1 - (elapsedTime / time) * (1-minScaleFactor);
            shadow.transform.localScale = originalScale * scaleFactor;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Optional: make the shadow disappear instantly when the rock lands
        shadow.transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J)) // Replace this with your condition.
        {
            // SpawnRock();
        }
    }
}

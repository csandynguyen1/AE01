using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class freezeEffect : MonoBehaviour
{
    public GameObject iceBlockPrefab;
    private GameObject iceBlockInstance;
    private bool isFrozen = false;
    public float freezeTime;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collision Detected");
        if(other.CompareTag("Freezing"))
        {
            FreezePlayer();
        }
    }
    public void FreezePlayer()
    {
        if(isFrozen)
        {
            unfreezePlayer();
            return;
        }
        isFrozen = true;
        iceBlockPrefab.GetComponent<SpriteRenderer>().enabled = true;
        //iceBlockInstance = Instantiate(iceBlockPrefab, transform.position + new Vector3(0,0.5f,0), Quaternion.identity, transform);
        //iceBlockInstance.GetComponent<SpriteRenderer>().color = new Color(255,255,255,(197/255f));  
        // Disable the PlayerController script
        var playerController = GetComponent<PlayerControllerRedo>();
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        if (playerController != null)
        {
            playerController.enabled = false;
        }

        // Start the coroutine to unfreeze the player after 10 seconds
        StartCoroutine(UnfreezePlayerAfterDelay(freezeTime));
    }

    private void unfreezePlayer()
    {
        isFrozen = false;
        var playerController = GetComponent<PlayerControllerRedo>();
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        if (playerController != null)
        {
            playerController.enabled = true;
            iceBlockPrefab.GetComponent<SpriteRenderer>().enabled = false;
            //Destroy(iceBlockInstance);
        }

        // Destroy the ice block instance
        if (iceBlockPrefab != null)
        {
            iceBlockPrefab.GetComponent<SpriteRenderer>().enabled = false;
            //Destroy(iceBlockInstance);
        }
    }

    IEnumerator UnfreezePlayerAfterDelay(float delay)
    {
        // Wait for the specified delay
        Debug.Log("Frozen,waiting.");
        yield return new WaitForSeconds(delay);

        // Re-enable the PlayerController script
        unfreezePlayer();
    }
}

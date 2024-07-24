using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCloud : MonoBehaviour
{
    public GameObject cloudPrefab;
    public Transform playerTransform;
    public GameObject shadowPrefab;
    public float cloudOffset = 10f;
    public float fallDuration = 2f;
    public float damageRadius = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void InstantiateCloud()
    {
        Vector2 shadowPosition = new Vector2(playerTransform.position.x, playerTransform.position.y-0.7f); // Directly above the player
        GameObject shadowInstance = Instantiate(shadowPrefab, shadowPosition, shadowPrefab.transform.rotation);
        Vector2 cloudPosition = shadowPosition + new Vector2(0, cloudOffset);
        GameObject cloudInstance = Instantiate(cloudPrefab, cloudPosition, Quaternion.identity);

     
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
            // InstantiateCloud();
        }
    }
}

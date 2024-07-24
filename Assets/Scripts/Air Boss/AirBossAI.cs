using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirBossAI : MonoBehaviour, IBossAI
{
    private Animator anim;
    public GameObject airbolt;
    public float cloudCD = 5f;
    public float summonCD = 10f;
    public float airboltCD = 4f;
    public Canvas bossHPBar;
    private float lastCloudTime = -Mathf.Infinity; // Initialize to a far past time to allow immediate use
    private float lastSummonTime = -Mathf.Infinity;
    private float lastAirboltTime = -Mathf.Infinity;
    public Vector3 airboltOffset = new Vector3(1.15f, 0.35f, 0);
    private bool inCombat = false;
    [SerializeField] private GameObject[] powerups;
    private AudioSource audioSource;
    public AudioClip cloudSound;
    public AudioClip airboltSound;
    public AudioClip summonSound;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    public void TryActivateCloud()
    {
        if (Time.time - lastCloudTime >= cloudCD)
        {
            audioSource.PlayOneShot(cloudSound);
            StartCloudAnimation();
            lastCloudTime = Time.time; // Update the last used time
        }
        else
        {
            TryActivateAirbolt();
        }
    }

    public void TryActivateSpawners()
    {
        if (Time.time - lastSummonTime >= summonCD)
        {
            audioSource.PlayOneShot(summonSound);
            StartSpawnerAnimation();
            lastSummonTime = Time.time; // Update the last used time
        }
    }

    public void TryActivateAirbolt()
    {
        if (Time.time - lastAirboltTime >= airboltCD)
        {
            audioSource.PlayOneShot(airboltSound);
            StartAirboltAnimation();
            lastAirboltTime = Time.time; // Update the last used time
        }
        else
        {
            TryActivateCloud();
        }
    }

    IEnumerator BossAttackPattern()
    {
        while (inCombat)
        {
            //yield return new WaitForSeconds(Random.Range(3, 5));
            int attackType = Random.Range(0, 3); // Assuming 3 types of attacks
            switch (attackType)
            {
                case 0:
                    TryActivateCloud();
                    yield return new WaitForSeconds(4);
                    break;
                case 1:
                    TryActivateSpawners();
                    yield return new WaitForSeconds(2);
                    break;
                case 2:
                    TryActivateAirbolt();
                    yield return new WaitForSeconds(2);
                    break;
            }
        }
    }
    public void OnObjectDestroy()
    {
        // remove enemy from scene
        Destroy(gameObject);
        GameObject[] allClouds = GameObject.FindGameObjectsWithTag("Cloud");
        foreach (GameObject cloud in allClouds)
        {
            Destroy(cloud);
        }
        foreach (GameObject powerup in powerups)
        {
            powerup.GetComponent<SpriteRenderer>().enabled = true;
            powerup.GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    public void activateAirbolt()
    {
        Debug.Log("Airbolt Cast");
        if (airbolt != null)
        {
            GameObject clone = Instantiate(airbolt, airbolt.transform.position + airboltOffset, airbolt.transform.rotation);
            clone.SetActive(true);
        }
    }

    public void StartCombat()
    {
        if (!inCombat)
        {
            Debug.Log("Combat Started");
            inCombat = true;
            bossHPBar.enabled = true;
            StartCoroutine(BossAttackPattern());
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y)) // This could be any condition you want to test
        {
            StartSpawnerAnimation();
        }
        else if (Input.GetKeyDown(KeyCode.U)) // This could be any condition you want to test
        {
            StartCloudAnimation();
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            StartAirboltAnimation();
        }
    }
    public void StartAirboltAnimation()
    {
        // Play the attack animation by setting a trigger or changing a boolean parameter
        anim.SetTrigger("Airbolt"); // Replace "AttackTrigger" with the name of your trigger parameter
    }
    public void StartSpawnerAnimation()
    {
        // Play the attack animation by setting a trigger or changing a boolean parameter
        anim.SetTrigger("Spawners"); // Replace "AttackTrigger" with the name of your trigger parameter
    }
    public void StartCloudAnimation()
    {
        // Play the attack animation by setting a trigger or changing a boolean parameter
        anim.SetTrigger("Cloud"); // Replace "AttackTrigger" with the name of your trigger parameter
    }

}

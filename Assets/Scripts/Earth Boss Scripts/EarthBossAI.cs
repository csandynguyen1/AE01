using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthBossAI : MonoBehaviour, IBossAI
{
    private Animator anim;
    public FissureSpawner fissureSpawn;
    public float eruptionCD = 5f;
    public float fissureCD = 10f;
    public float rockfallCD = 4f;
    public Canvas bossHPBar;
    private float lastEruptionTime = -Mathf.Infinity; // Initialize to a far past time to allow immediate use
    private float lastFissureTime = -Mathf.Infinity;
    private float lastRockfallTime = -Mathf.Infinity;
    private bool inCombat = false;
    private AudioSource audioSource;
    public AudioClip eruptionSound;
    [SerializeField] private GameObject[] powerups;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    public void TryActivateEruption()
    {
        if (Time.time - lastEruptionTime >= eruptionCD)
        {
            StartEruptAnimation();
            lastEruptionTime = Time.time; // Update the last used time
        }
        else
        {
            TryActivateRockfall();
        }
    }
    
    public void OnObjectDestroy()
    {
        Destroy(gameObject);
        foreach (GameObject powerup in powerups)
        {
            powerup.GetComponent<SpriteRenderer>().enabled = true;
            powerup.GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    public void TryActivateFissure()
    {
        if (Time.time - lastFissureTime >= fissureCD)
        {
            StartFissureAnimation();
            lastFissureTime = Time.time; // Update the last used time
        }
    }

    public void TryActivateRockfall()
    {
        if (Time.time - lastRockfallTime >= rockfallCD)
        {
            StartRockfallAnimation();
            lastRockfallTime = Time.time; // Update the last used time
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
                    TryActivateEruption();
                    yield return new WaitForSeconds(4);
                    break;
                case 1:
                    TryActivateFissure();
                    yield return new WaitForSeconds(2);
                    break;
                case 2:
                    TryActivateRockfall();
                    yield return new WaitForSeconds(2);
                    break;
            }
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
            StartFissureAnimation();
        }
        else if (Input.GetKeyDown(KeyCode.U)) // This could be any condition you want to test
        {
            StartEruptAnimation();
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            //StartRockfallAnimation();
        }
    }
    public void StartRockfallAnimation()
    {
        // Play the attack animation by setting a trigger or changing a boolean parameter
        anim.SetTrigger("Rockfall"); // Replace "AttackTrigger" with the name of your trigger parameter
    }
    public void StartFissureAnimation()
    {
        // Play the attack animation by setting a trigger or changing a boolean parameter
        anim.SetTrigger("Fissure"); // Replace "AttackTrigger" with the name of your trigger parameter
    }
    public void StartEruptAnimation()
    {
        // Play the attack animation by setting a trigger or changing a boolean parameter
        anim.SetTrigger("Erupt"); // Replace "AttackTrigger" with the name of your trigger parameter
    }

    public void TriggerFissureEruption()
    {
        SegmentEruption[] allFissures = FindObjectsOfType<SegmentEruption>();
        audioSource.PlayOneShot(eruptionSound);
        foreach (SegmentEruption fissure in allFissures)
        {
            fissure.Erupt();
        }
    }
}

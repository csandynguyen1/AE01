using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBossAI : MonoBehaviour, IBossAI
{
    public GameObject IceBeam;
    public GameObject IceRing;
    public GameObject Snowball;
    public float iceBeamCD = 10f;
    public float iceRingCD = 20f;
    public float snowballCD = 4f;
    public Canvas bossHPBar;
    private float lastIceBeamTime = -Mathf.Infinity; // Initialize to a far past time to allow immediate use
    private float lastIceRingTime = -Mathf.Infinity;
    private float lastSnowballTime = -Mathf.Infinity;
    private Animator anim;
    private bool inCombat = false;
    [SerializeField] private GameObject[] powerups;
    public AudioClip iceBeamSound;
    public AudioClip iceRingSound;
    public AudioClip snowballSound;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y)) // This could be any condition you want to test
        {
            StartAttackAnimation();
        }
        else if (Input.GetKeyDown(KeyCode.N))
        {
            StartRingAnimation();
        }
        else if (Input.GetKeyDown(KeyCode.H))
        {
            StartSnowballAnimation();
        }
    }
    public void TryActivateIceBeam()
    {
        if (Time.time - lastIceBeamTime >= iceBeamCD)
        {
            StartAttackAnimation();
            lastIceBeamTime = Time.time; // Update the last used time
        }
        else
        {
            TryActivateSnowball();
        }
    }

    public void TryActivateIceRing()
    {
        if (Time.time - lastIceRingTime >= iceRingCD)
        {
            StartRingAnimation();
            lastIceRingTime = Time.time; // Update the last used time
        }
        else
        {
            TryActivateSnowball();
        }
    }

    public void TryActivateSnowball()
    {
        if (Time.time - lastSnowballTime >= snowballCD)
        {
            StartSnowballAnimation();
            lastSnowballTime = Time.time; // Update the last used time
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

    IEnumerator BossAttackPattern()
    {
        while (inCombat)
        {
            //yield return new WaitForSeconds(Random.Range(3, 5));
            int attackType = Random.Range(0, 3); // Assuming 3 types of attacks
            switch (attackType)
            {
                case 0:
                    TryActivateIceBeam();
                    yield return new WaitForSeconds(4);
                    break;
                case 1:
                    TryActivateIceRing();
                    yield return new WaitForSeconds(2);
                    break;
                case 2:
                    TryActivateSnowball();
                    yield return new WaitForSeconds(1);
                    break;
            }
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
    public void StartRingAnimation()
    {
        anim.SetTrigger("RingTrigger");
        
    }
    public void StartAttackAnimation()
    {
        // Play the attack animation by setting a trigger or changing a boolean parameter
        anim.SetTrigger("BeamTrigger"); // Replace "AttackTrigger" with the name of your trigger parameter
        
    }
    public void StartSnowballAnimation()
    {
        anim.SetTrigger("SnowballTrigger");
        
    }

    public void activateSnowball()
    {
        Debug.Log("Snowball Cast");
        if(Snowball != null)
        {
            StartCoroutine(spawnSnowballs());
        }
    }
    IEnumerator spawnSnowballs()
    {
        for(int i = 0; i < 6; i++)
        {
            GameObject clone = Instantiate(Snowball, Snowball.transform.position, Snowball.transform.rotation);
            clone.SetActive(true);
            audioSource.PlayOneShot(snowballSound);
            yield return new WaitForSeconds(0.15f);
        }
    }
    public void activateIceRing()
    {
        Debug.Log("Ice Ring Proxy called");
        if(IceRing != null)
        {
            Debug.Log("Attempting to call host method.");
            IceRing.GetComponent<ringExpand>().playRing();
            audioSource.PlayOneShot(iceRingSound);
            
            
        }
    }

    private IEnumerator StopBeamSoundAfterDuration(float duration)
    {
        yield return new WaitForSeconds(1f);
        audioSource.PlayOneShot(iceBeamSound);
        yield return new WaitForSeconds(duration); // Wait for the duration
        audioSource.Stop(); // Stop all sounds on this AudioSource
    }

    public void activateIceBeam()
    {
        Debug.Log("Proxy method called");
        if (IceBeam != null)
        {
            IceBeam.GetComponent<beamTester>().ActivateBeam();
            StartCoroutine(StopBeamSoundAfterDuration(3.75f));
        }
    }

}

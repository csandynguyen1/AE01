using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBossAI : MonoBehaviour, IDamageable, IBossAI
{
    private Animator anim;
    public float health = 100;
    public HPBar healthBar;
    public GameObject fireball;
    public GameObject shield;
    public float shieldDuration = 1.0f;
    private bool isShielded = true;
    public HeatBar heatbar;
    public float fireballCD = 0.5f;
    public Canvas bossHPBar;
    private bool inCombat = false;
    private float lastFireballTime = -Mathf.Infinity; // Initialize to a far past time to allow immediate use
    private Vector3 fireballOffset = new Vector3(1.15f,0.35f,0);
    [SerializeField] private GameObject[] powerups;
    private AudioSource audioSource;
    public AudioClip fireballSound;
    public AudioClip shieldSound;
    public AudioClip shieldFadeSound;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
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
        Debug.Log("Entered attack pattern");
        while (inCombat)
        {
            //yield return new WaitForSeconds(Random.Range(3, 5));
            
            TryActivateFireball();
            yield return new WaitForSeconds(0.15f);
        }
    }

    public void TryActivateFireball()
    {
        if (Time.time - lastFireballTime >= fireballCD)
        {
            StartFireballAnimation();
            lastFireballTime = Time.time; // Update the last used time
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            StartFireballAnimation();
        }
        else if(Input.GetKeyDown(KeyCode.J))
        {
            StartShieldAnimation();
        }
    }

    public void OnHit(float damage)
    {
        Debug.Log("entered basic method");
        //OnHit(damage, null);
    }
    public void OnHit(float damage, GameObject attacker)
    {
        Debug.Log("entered overload method");
        if (isShielded && attacker != null)
        {
            Debug.Log("reflecting damage");
            IDamageable attackerDmgable = attacker.GetComponent<IDamageable>();
            heatbar.IncreaseHeat(10);
            attackerDmgable.OnHit(damage);
            attacker.GetComponent<PlayerControllerRedo>().EndSwordAttack();
        }
        else
        {
            Debug.Log("taking damage");
            health -= damage;
            if (healthBar != null)
            {
                healthBar.setHealth((int)health);
            }
            if (health <= 0)
            {
                anim.SetBool("isAlive", false);
                OnObjectDestroy(); // Destroy boss if health depletes
            }
        }
    }
    public float Health
    {
        set
        {
            if (value < health)
            {
                anim.SetTrigger("hit");
            }
            health = value;

            if (value < 0)
            {
                anim.SetTrigger("hit");
            }
            if (health <= 0)
            {
                anim.SetBool("isAlive", false);
            }
        }
        get
        {
            return health;
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
    public void StartShieldAnimation()
    {
        anim.SetTrigger("FireShield");
    }
    public void StartFireballAnimation()
    {
        anim.SetTrigger("FireballTrigger");
    }
    public void activateShield()
    {
        audioSource.PlayOneShot(shieldSound);
        shield.GetComponent<SpriteRenderer>().enabled = true;
        isShielded = true;
        //StartCoroutine(shieldTimer());
    }
    public void deactivateShield()
    {
        audioSource.PlayOneShot(shieldFadeSound);
        shield.GetComponent<SpriteRenderer>().enabled = false;
        isShielded = false;
        //StartCoroutine(shieldTimer());
    }

    public void activateFireball()
    {
        Debug.Log("Fireball Cast");
        if (fireball != null)
        {
            StartCoroutine(spawnFireballs());
        }
    }

    IEnumerator shieldTimer()
    {
        yield return new WaitForSeconds(shieldDuration);
        shield.GetComponent<SpriteRenderer>().enabled = true;
        isShielded = true;
    }

    IEnumerator spawnFireballs()
    {
        int attackType = Random.Range(1, 4);
        for (int i = 0; i < attackType; i++)
        {
            GameObject clone = Instantiate(fireball, fireball.transform.position+fireballOffset, fireball.transform.rotation);
            clone.SetActive(true);
            audioSource.PlayOneShot(fireballSound);
            yield return new WaitForSeconds(0.25f);
        }
    }
}

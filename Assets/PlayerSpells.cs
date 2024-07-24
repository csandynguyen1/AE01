using UnityEngine;
public class PlayerSpells : MonoBehaviour
{

    public GameObject fireballPrefab;
    public GameObject rockPrefab;
    public GameObject waterSplashPrefab;
    public GameObject lightningBoltPrefab;

    private Vector2 facingDirection = Vector2.right; // Default to facing right

    private float nextFireTime = 0f;
    private float nextRockTime = 0f;
    private float nextWaterTime = 0f;
    private float nextLightningTime = 0f;
    private float cooldownDuration = 1f; // 1 second cooldown

    void Update()
    {
        // Update facingDirection based on player input
        if (Input.GetKey(KeyCode.W))
            facingDirection = Vector2.up;
        else if (Input.GetKey(KeyCode.S))
            facingDirection = Vector2.down;
        else if (Input.GetKey(KeyCode.A))
            facingDirection = Vector2.left;
        else if (Input.GetKey(KeyCode.D))
            facingDirection = Vector2.right;

        // Cast spells when keys are pressed and cooldown has elapsed
        if (Input.GetKeyDown(KeyCode.Alpha1) && Time.time >= nextFireTime)
        {
            CastSpell(fireballPrefab, facingDirection);
            nextFireTime = Time.time + cooldownDuration;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && Time.time >= nextRockTime)
        {
            CastSpell(rockPrefab, facingDirection);
            nextRockTime = Time.time + cooldownDuration;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && Time.time >= nextWaterTime)
        {
            CastSpell(waterSplashPrefab, facingDirection);
            nextWaterTime = Time.time + cooldownDuration;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && Time.time >= nextLightningTime)
        {
            CastSpell(lightningBoltPrefab, facingDirection);
            nextLightningTime = Time.time + cooldownDuration;
        }
    }

    private void CastSpell(GameObject spellPrefab, Vector2 direction)
    {
        GameObject spell = Instantiate(spellPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = spell.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.velocity = direction * 10;
    }
}






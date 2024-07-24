using UnityEngine;

public class SpellCasting : MonoBehaviour
{
    public GameObject fireballPrefab;
    public GameObject rockPrefab;
    public GameObject waterSplashPrefab;
    public GameObject lightningBoltPrefab;
    public Transform spellSpawnPoint;

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                CastSpell(fireballPrefab);
            else if (Input.GetKeyDown(KeyCode.Alpha2))
                CastSpell(rockPrefab);
            else if (Input.GetKeyDown(KeyCode.Alpha3))
                CastSpell(waterSplashPrefab);
            else if (Input.GetKeyDown(KeyCode.Alpha4))
                CastSpell(lightningBoltPrefab);
        }
    }

    void CastSpell(GameObject spellPrefab)
    {
        if (spellPrefab && spellSpawnPoint)
        {
            Instantiate(spellPrefab, spellSpawnPoint.position, Quaternion.identity);
        }
    }
}

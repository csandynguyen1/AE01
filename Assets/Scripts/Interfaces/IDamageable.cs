using UnityEngine;

public interface IDamageable
{
    // Health property for all things able to be hit
    public float Health {set; get;}

    // OnHit message with damage passed as param
    public void OnHit(float damage);
    public void OnHit(float damage, GameObject attacker);

    
}
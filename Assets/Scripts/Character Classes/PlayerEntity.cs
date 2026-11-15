using UnityEngine;

public class PlayerEntity : CharacterEntity
{
    void Start()
    {
        HUDManager.Instance.UpdateHealthCounter(activeHealth,definedHealth);
    }
    void Die()
    {
        
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        HUDManager.Instance.UpdateHealthCounter(activeHealth,definedHealth);
        if(activeHealth<=0)
            Die();
    }
}

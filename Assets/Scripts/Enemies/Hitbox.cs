using UnityEngine;
using UnityEngine.AI;

public class Hitbox : MonoBehaviour
{
    public Enemy enemy;
    public int damageMulitplier;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CallTakeDamage(int damage, bool useDamageMultiplier)
    {
        if(useDamageMultiplier)
            enemy.TakeDamage(damage*damageMulitplier);
        else
            enemy.TakeDamage(damage);
    }
}

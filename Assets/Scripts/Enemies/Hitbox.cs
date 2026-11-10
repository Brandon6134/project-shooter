using UnityEngine;

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

    public void CallTakeDamage(int damage)
    {
        enemy.TakeDamage(damage*damageMulitplier);
    }
}

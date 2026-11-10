using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float definedHealth = 10f;
    protected float activeHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        activeHealth = definedHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        activeHealth -= damage;
        print("enemy took " + damage + " damage; health at " + activeHealth);
    }

    public void Die()
    {
        
    }
}

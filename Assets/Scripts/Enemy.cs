using NUnit.Framework;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float definedHealth = 10f;
    protected float activeHealth;

    private Pool enemyPool;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        activeHealth = definedHealth;
        enemyPool = transform.parent.GetComponent<Pool>(); //gain access to the enemy pool when initailzed
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        activeHealth -= damage;
        print("enemy took " + damage + " damage; health at " + activeHealth);
        if(activeHealth<=0)
            Die();
    }

    public void Die() //make enemy "die"
    {
        enemyPool.Despawn(gameObject);
        activeHealth = definedHealth;
    }
}

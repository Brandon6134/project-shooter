using NUnit.Framework;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float definedHealth = 10f;
    protected float activeHealth;
    private Rigidbody rb;
    private Transform playerTransform;

    private Pool enemyPool;
    public float chaseSpeed = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        activeHealth = definedHealth;
        enemyPool = transform.parent.GetComponent<Pool>(); //gain access to the enemy pool when initailzed
        
        rb = GetComponent<Rigidbody>();
    }
    
    void Start()
    {
        playerTransform = PlayerManager.Player.transform; //gain access to player transform when initialized
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        ChasePlayer();
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

    public void ChasePlayer()
    {
        Vector3 chaseDirection = (playerTransform.position - transform.position).normalized;
        rb.MovePosition(rb.position + chaseDirection * chaseSpeed * Time.fixedDeltaTime);
    }
}

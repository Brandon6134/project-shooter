using System.Collections;
using NUnit.Framework;
using UnityEngine;

public class EnemyEntity : CharacterEntity
{
    protected Transform playerTransform;
    private Pool enemyPool;
    public float chaseSpeed = 2f;
    private Vector3 chaseDirection;
    [SerializeField] MeshRenderer[] meshRenderers;
    private Color originalColor;
    protected override void Awake()
    {
        base.Awake();
        enemyPool = transform.parent.GetComponent<Pool>(); //gain access to the enemy pool when initailzed
    }
    
    void Start()
    {
        playerTransform = PlayerManager.Player.transform; //gain access to player transform when initialized
        originalColor = meshRenderers[0].material.color;
    }

    protected virtual void FixedUpdate()
    {
        ChasePlayer();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
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
        chaseDirection = playerTransform.position - transform.position;
        chaseDirection.y = 0f; //enemy doesnt rotate in Y axis when rotating / chasing
        chaseDirection.Normalize();
        rb.MovePosition(rb.position + chaseDirection * chaseSpeed * Time.fixedDeltaTime);
        
        Quaternion targetRotation = Quaternion.LookRotation(chaseDirection);
        rb.MoveRotation(targetRotation);
    }

    protected IEnumerator FlashRed(float flashDuration)
    {
        float t = 0f;

        while(t < flashDuration)
        {
            t += Time.deltaTime;

            foreach(MeshRenderer mR in meshRenderers)
            {
                mR.material.color = Color.Lerp(originalColor, Color.red, t / flashDuration);
            }

            yield return null;
        }

        t = 0f;

        while(t < flashDuration)
        {
            t += Time.deltaTime;

            foreach(MeshRenderer mR in meshRenderers)
            {
                mR.material.color = Color.Lerp(Color.red, originalColor, t / flashDuration);
            }

            yield return null;
        }
    }
}

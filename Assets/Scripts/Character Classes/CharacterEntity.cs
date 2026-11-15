using UnityEngine;

public class CharacterEntity : MonoBehaviour
{
    public float definedHealth = 10f;
    protected float activeHealth;
    protected Rigidbody rb;

    protected virtual void Awake()
    {
        activeHealth = definedHealth;
        rb = GetComponent<Rigidbody>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void TakeDamage(int damage)
    {
        activeHealth -= damage;
    }
}

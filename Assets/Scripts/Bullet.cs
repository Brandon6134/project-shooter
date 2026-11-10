using UnityEditor.Callbacks;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 15f;
    public float lifeTime = 3f;
    public int damageDealt = 2;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = transform.forward * speed; //forward direction?
        Destroy(gameObject,lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Hitbox>())
        {
            collision.gameObject.GetComponent<Hitbox>().CallTakeDamage(damageDealt);
        }
        Destroy(gameObject);
    }
}

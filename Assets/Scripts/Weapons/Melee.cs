using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Melee : MonoBehaviour
{
    [UnitHeaderInspectable("Attacking Melee")]
    public float attackDistance = 3f;
    public float attackSpeed = 0.4f;
    public float attackCooldown = 1f;
    public int attackDamage = 1;

    bool attacking = false;
    bool readyToAttack = true;
    [SerializeField] GameObject cam;

    private Vector3 initialPosition;
    public Vector3 hitPositionOffset = new Vector3 (0,0,1);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack()
    {
        if(!readyToAttack || attacking) return;

        readyToAttack = false;
        attacking = true;

        StopCoroutine(KnifeAnimation());
        StartCoroutine(KnifeAnimation());

        Invoke(nameof(ResetAttack),attackCooldown);
        Invoke(nameof(AttackRayCast),attackSpeed);


    }

    void AttackRayCast()
    {
        if (Physics.Raycast(cam.transform.position,cam.transform.forward, out RaycastHit hit, attackDistance))
        {
            Hitbox hitbox = hit.collider.GetComponent<Hitbox>();
            if (hitbox)
                hitbox.CallTakeDamage(attackDamage,false);
        }  
    }

    void ResetAttack()
    {
        attacking = false;
        readyToAttack = true;
    }

    IEnumerator KnifeAnimation()
    {
        Vector3 targetPosition = initialPosition + hitPositionOffset;
        float halfAttackTime = attackSpeed / 2;
        float t = 0f;

        while(t < halfAttackTime) //rotate gun from initial angle to rotated angle
        {
            t += Time.deltaTime;
            transform.localPosition = Vector3.Slerp(initialPosition,targetPosition, t / halfAttackTime);
            yield return null; //wait 1 frame
        }

        t = 0f;

        while(t < halfAttackTime) //rotate angle back from titled angle to initial angle
        {
            t += Time.deltaTime;
            transform.localPosition = Vector3.Slerp(targetPosition,initialPosition, t / halfAttackTime);
            yield return null; //wait 1 frame
        }
    }
}

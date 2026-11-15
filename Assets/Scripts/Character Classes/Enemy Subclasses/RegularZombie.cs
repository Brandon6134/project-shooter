using Unity.VisualScripting;
using UnityEngine;

public class RegularZombie : EnemyEntity
{
    [SerializeField] Melee melee;
    private float attackRange;
    bool attackScheduled = false;
    public float timeDelayBeforeAttack = 2f;

    protected override void Awake()
    {
        base.Awake();
        attackRange = melee.attackDistance;
    }
    
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if(Vector3.Distance(playerTransform.position,transform.position) < attackRange)
        {
            if(!attackScheduled)
            {
                attackScheduled = true;
                StartCoroutine(FlashRed(timeDelayBeforeAttack*2));
                Invoke(nameof(PerformAttack),timeDelayBeforeAttack);
            }
        }
    }
    
    void PerformAttack()
    {
        melee.Attack();
        attackScheduled = false;
    }
}

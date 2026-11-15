using System;
using UnityEngine;

public class PlayerEntity : CharacterEntity
{
    [SerializeField] RectTransform healthRT;
    [SerializeField] Pool dmgTextPool;
    public Vector2 damageTextOffset = new Vector2(0,50);
    void Start()
    {
        HUDManager.Instance.UpdateHealthCounter(activeHealth,definedHealth);
    }
    void Die()
    {
        
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        Vector2 targetRectTransform = healthRT.anchoredPosition + damageTextOffset;
        GameObject newPopupDamageText = dmgTextPool.SpawnText(targetRectTransform);
        newPopupDamageText.GetComponent<PopupDamageBehaviour>().SetText("-" + damage.ToString());

        HUDManager.Instance.UpdateHealthCounter(activeHealth,definedHealth);
        if(activeHealth<=0)
            Die();
    }
}

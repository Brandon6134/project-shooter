using System.Collections;
using TMPro;
using UnityEngine;

public class PopupDamageBehaviour : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI damageText;
    private Pool dmgTextPool;
    public float fadeRate = 0.2f;
    public float timeUntilStartFade = 1f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dmgTextPool = transform.parent.GetComponent<Pool>(); //gain access to the dmg text pool when initailzed
    }

    // Update is called once per frame
    void Update()
    {
        Invoke(nameof(Fade),timeUntilStartFade);
        if (damageText.alpha <= 0f)
            Die();
    }

    public void SetText(string text)
    {
        damageText.text = text;
    }

    void Fade()
    {
        damageText.alpha -= fadeRate*Time.deltaTime;
    }

    void Die()
    {
        dmgTextPool.Despawn(gameObject);
        damageText.alpha = 1f;
    }
}

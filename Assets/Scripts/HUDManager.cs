using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance; //only one HUDManager class, so defining as Instance is fine
    [SerializeField] TextMeshProUGUI ammoText;
    [SerializeField] TextMeshProUGUI healthText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateAmmoCounter(int magAmmo, int totalAmmo)
    {
        ammoText.text = "Ammo: " + magAmmo + " | " + totalAmmo;
    }

    public void UpdateHealthCounter(float newHealth, float maxHealth)
    {
        healthText.text = "HP: " + newHealth + " / " + maxHealth;
    }

    
}

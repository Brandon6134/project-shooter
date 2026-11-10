using TMPro;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance; //only one HUDManager class, so defining as Instance is fine
    [SerializeField] TextMeshProUGUI ammoText;
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

    
}

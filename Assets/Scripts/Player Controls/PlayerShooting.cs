using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] InputActionReference shootInput;
    [SerializeField] InputActionReference releaseShootInput;
    [SerializeField] InputActionReference reloadInput;
    public Gun gun;
    private bool isHoldingShoot = false;
    
    void OnShoot()
    {
        isHoldingShoot = true;
    }

    void OnShootRelease()
    {
        isHoldingShoot = false;
    }

    void OnReload()
    {
        if(gun != null)
            gun.TryReload();
    }

     // Update is called once per frame
    void Update()
    {

        if(isHoldingShoot && gun != null)
            gun.Shoot();
    }
}

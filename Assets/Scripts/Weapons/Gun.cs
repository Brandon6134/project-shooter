using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;
using System;

public class Gun : MonoBehaviour
{
    
    public float reloadTime = 1f;
    public float fireRate = 0.15f;
    public int magSize = 8;

    public GameObject bullet;
    public Transform bulletSpawnPoint;

    public GameObject weaponFlash;

    public float recoilDistance = 0.1f;
    public float recoilSpeed = 15f;

    [Header("Ammo")]
    private int currentAmmo;
    public int reserveAmmo = 30;
    private bool isReloading = false;
    private float nextTimeToFire = 0f;

    private Quaternion initialRotation;
    private Vector3 initialPosition;
    private Vector3 reloadRotationOffset = new Vector3 (60,50,50);
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentAmmo = magSize;
        initialRotation = transform.localRotation;
        initialPosition = transform.localPosition;

        HUDManager.Instance.UpdateAmmoCounter(currentAmmo,reserveAmmo);
    }

    public void Shoot()
    {
        if (isReloading) return; //if reloading cannot shoot
        if (Time.time < nextTimeToFire) return; //if in shooting cooldown, dont shoot

        if (currentAmmo==0 && reserveAmmo==0) //if has no ammo at all, don't shoot or reload
            return;

        if (currentAmmo <= 0) //if out of ammo in mag
        {
            StartCoroutine(Reload());
            return;
        }

        nextTimeToFire = Time.time + fireRate;
        currentAmmo--;

        HUDManager.Instance.UpdateAmmoCounter(currentAmmo,reserveAmmo);

        Instantiate(bullet,bulletSpawnPoint.position,bulletSpawnPoint.rotation);
        //Instantiate(weaponFlash,bulletSpawnPoint.position,bulletSpawnPoint.rotation);

        StopCoroutine(nameof(Recoil));
        StartCoroutine(nameof(Recoil));
    }

    IEnumerator Reload()
    {
        isReloading = true;

        Quaternion targetRotation = Quaternion.Euler(initialRotation.eulerAngles + reloadRotationOffset);
        float halfReload = reloadTime / 2f;
        float t = 0f;

        while(t < halfReload) //rotate gun from initial angle to rotated angle
        {
            t += Time.deltaTime;
            transform.localRotation = Quaternion.Slerp(initialRotation,targetRotation, t / halfReload);
            yield return null; //wait 1 frame
        }

        t = 0f;

        while(t < halfReload) //rotate angle back from titled angle to initial angle
        {
            t += Time.deltaTime;
            transform.localRotation = Quaternion.Slerp(targetRotation,initialRotation, t / halfReload);
            yield return null; //wait 1 frame
        }

        int oldReserveAmmo = reserveAmmo;
        reserveAmmo = Mathf.Max(reserveAmmo - (magSize - currentAmmo),0); //if total ammo < 0, sets to 0

        if(reserveAmmo + currentAmmo < magSize)
            currentAmmo+=oldReserveAmmo;
        else
            currentAmmo = magSize;
        
        HUDManager.Instance.UpdateAmmoCounter(currentAmmo,reserveAmmo);

        isReloading = false;
    }

    public void TryReload()
    {
        if (isReloading) return;
        if (currentAmmo == magSize) return;
        if (reserveAmmo == 0) return;

        StartCoroutine(Reload());
    }

    private IEnumerator Recoil()
    {
        Vector3 recoilTarget = initialPosition + new Vector3(0,0,-recoilDistance);
        float t = 0;

        while(t < 1) //rotate gun from initial angle to rotated angle
        {
            t += Time.deltaTime*recoilSpeed;
            transform.localPosition = Vector3.Lerp(initialPosition,recoilTarget, t);
            yield return null; //wait 1 frame
        }

        t = 0;

        while(t < 1) //rotate gun from initial angle to rotated angle
        {
            t += Time.deltaTime*recoilSpeed;
            transform.localPosition = Vector3.Lerp(recoilTarget,initialPosition, t);
            yield return null; //wait 1 frame
        }

        transform.localPosition = initialPosition;
    }
}

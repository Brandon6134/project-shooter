using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;

public class Gun : MonoBehaviour
{
    public float reloadTime = 1f;
    public float fireRate = 0.15f;
    public int magSize = 8;

    public GameObject bullet;
    public Transform bulletSpawnPoint;

    private int currentAmmo;
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
    }

    public void Shoot()
    {
        if (isReloading) return; //if reloading cannot shoot
        if (Time.time < nextTimeToFire) return; //if in shooting cooldown, dont shoot

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        nextTimeToFire = Time.time + fireRate;
        currentAmmo--;

        Instantiate(bullet,bulletSpawnPoint.position,bulletSpawnPoint.rotation);
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
            transform.localRotation = Quaternion.Slerp(targetRotation,initialRotation, t / halfReload);
            yield return null; //wait 1 frame
        }

        t = 0f;

        while(t < halfReload) //rotate angle back from titled angle to initial angle
        {
            t += Time.deltaTime;
            transform.localRotation = Quaternion.Slerp(initialRotation, targetRotation, t / halfReload);
            yield return null; //wait 1 frame
        }

        currentAmmo = magSize;
        isReloading = false;
    }

    public void TryReload()
    {
        if (isReloading) return;
        if (currentAmmo == magSize) return;

        StartCoroutine(Reload());
    }
}

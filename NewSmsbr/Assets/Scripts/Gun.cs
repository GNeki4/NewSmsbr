using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    public int ammo;
    public int magazine;
    public int capacity;
    public float reloadTime;
    public bool notReloading = true;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public Recoil recoil;
    public Text ammoDisplay;

    private float nextTimeToFire = 0f;
    private AudioSource gunshot;

    private void Start()
    {
        gunshot = GetComponent<AudioSource>();
    }

    void Update()
    {
        ammoDisplay.text = string.Format("Ammo: {0} / {1}", ammo, magazine);

        if (Input.GetMouseButton(0) && Time.time >= nextTimeToFire && magazine > 0 && notReloading)
        {
            magazine--;
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        } 
        if (Input.GetKeyDown(KeyCode.R) && ammo > 0)
        {
            StartCoroutine(Reload());     
        }
    }

    IEnumerator Reload()
    {
        print("w8 bro");
        notReloading = false;

        yield return new WaitForSeconds(reloadTime);

        if (magazine == 0)
        {
            if (ammo >= capacity)
            {
                magazine = capacity;
                ammo -= capacity;
            }
            else
            {
                magazine = ammo;
                ammo = 0;
            }
        }
        else
        {
            int notEnough = capacity - magazine;

            if (ammo >= notEnough)
            {
                magazine = capacity;
                ammo -= notEnough;
            }
            else
            {
                magazine += ammo;
                ammo = 0;
            }
        }

        notReloading = true;
        print("reloaded!");
    }

    void Shoot()
    {
        muzzleFlash.Play();
        gunshot.Play();
        recoil.Fire();

        RaycastHit hit;

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            print(hit.transform.name);

            SimpleAIController target = hit.transform.GetComponent<SimpleAIController>();

            if (target != null)
            {
                target.TakeDamage(damage);
            }

            GameObject impactGo = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            
            Destroy(impactGo, 0.5f);
        }
    }
}

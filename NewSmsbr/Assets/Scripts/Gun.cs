using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public Recoil recoil;

    private float nextTimeToFire = 0f;
    private AudioSource gunshot;

    private void Start()
    {
        gunshot = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        } 
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

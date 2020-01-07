using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    public float damage;
    public float range;
    public float attackCD;
    private float attackTimer;

    public Camera fpsCam;
    private AudioSource[] sounds;
    public Animation anims; 

    private void Start()
    {
        sounds = GetComponents<AudioSource>();
        anims = GetComponent<Animation>();
        anims.Play("RakeDefault");
    }

    void Update()
    {
        attackTimer += Time.deltaTime;

        if (Input.GetMouseButton(0) && attackTimer >= attackCD)
            Attack();

        anims.PlayQueued("RakeDefault");
    }

    void Attack()
    {
        RaycastHit hit;
        anims.Play();

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            SimpleAIController target = hit.transform.GetComponent<SimpleAIController>();

            if (target != null)
            {
                sounds[0].Play();
                target.TakeDamage(damage);
            }
            else
                sounds[1].Play();
        }
        else
            sounds[1].Play();

        attackTimer = 0;
    }
}

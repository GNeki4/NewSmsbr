using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimpleAIController : MonoBehaviour
{
    public float range;
    public GameObject target;
    public Vector3 start;
    
    public float health;
    public float attackCD;
    private float attackTimer;
    public float damage;
    public Animation anims;
    public AudioSource[] sounds;

    PlayerHealth playerHealth;
    NavMeshAgent agent;

    public void Start()
    {
        sounds = GetComponents<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
        start = transform.position;
        playerHealth = target.GetComponent<PlayerHealth>();
        anims = GetComponent<Animation>();
        anims.Play("KontrolerDef");
    }

    public void Update()
    {
        float distance = Vector3.Distance(target.transform.position, transform.position);

        if (distance <= range)
        {           
            anims.PlayQueued("KontrolerWalk");

            agent.SetDestination(target.transform.position);

            if (distance <= agent.stoppingDistance)
            {
                FaceTarget();
                attackTimer += Time.deltaTime;

                if (attackTimer >= attackCD)
                {
                    Attack();
                }

            }          
        }
        else
        {
            agent.SetDestination(start);
            anims.Play("KontrolerDef");
        }

    }

    void Attack()
    {     
        anims.Play("KontrolerAttack");
        sounds[Random.Range(0,2)].Play();

        
        print("Attacking");
        playerHealth.TakeDamage(damage);

        attackTimer = 0;
    }


    public void FaceTarget()
    {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }

        void Die()
        {
            Destroy(gameObject);
        }
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}

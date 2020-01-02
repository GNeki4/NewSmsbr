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

    PlayerHealth playerHealth;
    NavMeshAgent agent;

    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        start = transform.position;
        playerHealth = target.GetComponent<PlayerHealth>();
    }

    public void Update()
    {
        float distance = Vector3.Distance(target.transform.position, transform.position);

        if (distance <= range)
        {
            agent.SetDestination(target.transform.position);

            if (distance <= agent.stoppingDistance)
            {
                FaceTarget();
                attackTimer += Time.deltaTime;

                if (attackTimer >= attackCD)
                {
                    // play anim
                    playerHealth.TakeDamage(damage);
                }

            }          
        }
        else
            agent.SetDestination(start);
    }

    void Attack()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            var target = hit.transform.GetComponent<PlayerHealth>();

            if (target != null)
                playerHealth.TakeDamage(damage);
        }
            

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

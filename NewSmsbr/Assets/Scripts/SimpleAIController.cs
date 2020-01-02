using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimpleAIController : MonoBehaviour
{
    public float range;
    public GameObject target;
    public Vector3 start;
    //
    public float health;
    public float attackCD;
    private float attackTimer;
    public float damage;

    NavMeshAgent agent;

    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        start = transform.position; 
    }

    public void Update()
    {
        float distance = Vector3.Distance(target.transform.position, transform.position);

        if (distance <= range)
        {
            agent.SetDestination(target.transform.position);

            print(distance);
            if (distance <= agent.stoppingDistance)
            {
                FaceTarget();
                print(distance.ToString());
                attackTimer += Time.deltaTime;

                if (attackTimer >= attackCD)
                    print("trying to attack lol but u suck at coding");
                    //Attack();
                //
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

            SimpleAIController target = hit.transform.GetComponent<SimpleAIController>();

            if (target != null)
                target.TakeDamage(damage);
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

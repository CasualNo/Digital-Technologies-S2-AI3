using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIMelee : MonoBehaviour
{
 

    Transform Player;
    NavMeshAgent agent;
    public int damage = 10;

    Transform enemy; // Ground check empty goes in here
    public float range = 0.1f; // How close the ground needs to be to register
    //LayerMask enemyMask = LayerMask.GetMask("Enemy");
    public int scoreValue = -1;
    public bool canAttack = true;
    public Target self;

    Animator anim;

    private void Awake()
    {
        self = GetComponent<Target>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = 3f;
        agent.stoppingDistance = 2.5f;
        enemy = gameObject.transform;

        anim = GetComponent<Animator>();

        Player = GameObject.Find("Player").transform;
    }
    
    
    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(self.target);

        if (isNextToPlayer())
        {
            anim.SetFloat("speedh", 0);

            if (canAttack == true)
            {
                PlayerHealth attacked = Player.GetComponent<PlayerHealth>();
                if (attacked != null && attacked.dmgInv == false)
                {
                    attacked.PlayerTakeDamage(damage);
                }
                canAttack = false;
                Invoke("ResetAttack", 2f);

                anim.SetTrigger("Attack1h1");
                agent.isStopped = true;
                Invoke("AgentOn", 1.2f);
            }
        }
        else anim.SetFloat("speedh", 1);
    }

    public void EnemyHit()
    {
        anim.SetTrigger("Hit1");
        agent.isStopped = true;
        Invoke("AgentOn", 0.3f);
    }

    void AgentOn()
    {
        agent.isStopped = false;
    }
    void ResetAttack()
    {
        canAttack = true;
    }

    private bool isNextToPlayer()
    {
        RaycastHit hit;
        Vector3 p1 = transform.position;
        Vector3 pDiff = Player.transform.position - p1;
        // Cast a sphere wrapping character controller 10 meters forward
        // to see if it is about to hit anything.
        if (Physics.SphereCast(p1, range, pDiff.normalized, out hit, 2))
        {
            GameObject go = GameObject.Find(hit.transform.name);

            if (go.name == "Player")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
        //return Physics.CheckSphere(Player.position, distanceToEnemy, enemyMask);
    }
}

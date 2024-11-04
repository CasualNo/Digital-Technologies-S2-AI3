using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Random = System.Random;
using UnityEngine.AI;

public class Target : MonoBehaviour
{
    //information injection
    public SpawnPointData spawn;
    public Vector3 target;
    [HideInInspector] public bool pTarget;
    public float maxhealth = 50f;
    [HideInInspector] public float health;
    public GameObject collectable;
    Random drop = new Random();

    public EnemyAIMelee Ai;

    Animator anim;
    bool hasAnimator = false;


    private void Awake()
    {
        health = maxhealth;
        
        if(gameObject.GetComponent<Animator>() != null)
        {
            anim = gameObject.GetComponent<Animator>();
            hasAnimator = true;
        }

    }
    public void TakeDamage(float amount)
    {
        health -= amount;
        
        if(hasAnimator == true)
        {
            Ai.EnemyHit();
        }
        

        if (health <= 0f)
        {
            if (drop.Next(2) == 0)
            {
                Instantiate(collectable, gameObject.transform.position, Quaternion.identity);
            }
            Die();
        }
        

    }

    async void Die()
    {
        if (spawn != null)
        {
            spawn.alive = false;
        }
        
        Destroy(GetComponent<NavMeshAgent>());
        Destroy(GetComponent<CapsuleCollider>());
        if(hasAnimator == true)
        {
            anim.SetTrigger("Fall1");
            await Task.Delay(1000);
        }
        
        Destroy(gameObject);
    }
}

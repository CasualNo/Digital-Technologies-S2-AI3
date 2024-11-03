using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIRanged : MonoBehaviour
{
    Transform Player;
    NavMeshAgent agent;
    public float distanceToEnemy = 0.1f;
    public bool canAttack = true;
    public GameObject bulletPrefab;
    public float bulletSpeed = 50f;
    public Target self;

    private void Awake()
    {
        self = GetComponent<Target>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = 3f;
        agent.stoppingDistance = 15f;
        bulletPrefab = Resources.Load("Prefabs/BulletPrefab") as GameObject;
        Player = GameObject.Find("Player").transform;
    }

    void Update()
    {
        agent.SetDestination(self.target);
        if (canAttack && self.pTarget)
        {
            spawnBullet();
            canAttack = false;
            Invoke("ResetAttack", 2f);
        }
    }

    void spawnBullet()
    {
        var bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        Vector3 pDiff = Player.position - transform.position;
        bullet.GetComponent<Rigidbody>().velocity = pDiff.normalized * bulletSpeed;
    }

    void ResetAttack()
    {
        canAttack = true;
    }
}

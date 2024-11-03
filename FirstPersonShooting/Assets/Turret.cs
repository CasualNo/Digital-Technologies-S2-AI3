using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Turret : MonoBehaviour
{
    public bool canAttack = true;
    public GameObject bulletPrefab;
    public float bulletSpeed = 50f;
    public float cooldown = 2f;

    private void Awake()
    {
        bulletPrefab = Resources.Load("Prefabs/BulletPrefab") as GameObject;
    }

    void Update()
    {
        if (canAttack)
        {
            spawnBullet();
            canAttack = false;
            Invoke("ResetAttack", cooldown);
        }
    }
    void spawnBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        bullet.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
    }
    void ResetAttack()
    {
        canAttack = true;
    }
}

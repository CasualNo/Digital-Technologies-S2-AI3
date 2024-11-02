using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Transform Player;
    int damage = 10;
    private void Start()
    {
        Invoke("killBullet", 15f);
    }
    void Awake()
    {
        Player = GameObject.Find("Player").transform;

    }

    void OnTriggerEnter(Collider other)
    {
        //Destroy(collision.gameObject);
        //Destroy(gameObject);
        GameObject go = GameObject.Find(other.transform.name);
        if (go.CompareTag("Player"))
        {
            killBullet();
            PlayerHealth attacked = Player.GetComponent<PlayerHealth>();
            if (attacked != null && attacked.dmgInv == false)
            {
                attacked.PlayerTakeDamage(damage);
            }
        }
        else if (go != null && !go.CompareTag("Enemies") && go.layer != 3)
        {
            killBullet();
        }
    }
    void killBullet()
    {
        Destroy(gameObject);
    }
}
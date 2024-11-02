using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Target : MonoBehaviour
{
    //information injection
    public SpawnPointData spawn;
    public Vector3 target;
    [HideInInspector] public bool pTarget;
    [SerializeField] private bool guaranteed;

    public float maxhealth = 50f;
    [HideInInspector] public float health;
    public GameObject guaranteedCollectable;
    Random drop = new Random();
    public List<GameObject> collectables = new List<GameObject>();

    private void Awake()
    {
        health = maxhealth;
    }
    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0f)
        {
            int rand = drop.Next(collectables.Count + 1);
            if (guaranteed)
            {
                Quaternion rotation = new Quaternion();
                rotation.eulerAngles = guaranteedCollectable.transform.rotation.eulerAngles;
                Instantiate(guaranteedCollectable, gameObject.transform.position, rotation);
            } else if (rand != collectables.Count)
            {
                Quaternion rotation = new Quaternion();
                rotation.eulerAngles = collectables[rand].transform.rotation.eulerAngles;
                Instantiate(collectables[rand], gameObject.transform.position, rotation);
            }
            Die();
        }
    }
    void Die()
    {
        if (spawn != null)
        {
            spawn.alive = false;
        }
        Destroy(gameObject);
    }
}

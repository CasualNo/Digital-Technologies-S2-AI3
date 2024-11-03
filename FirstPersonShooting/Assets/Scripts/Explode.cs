using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    public float damage = 20;
    public float falloff = 0.1f;
    public float fuseTime = 5f;
    public float blastRad = 1f;
    [HideInInspector] public string ownerName;
    private string changeName;

    void Start()
    {
        changeName = ownerName == "Player" ? "Enemy" : "Player";
        Invoke("Blast", fuseTime);
    }

    //I think i'll leave it that enemies can blow up walls, that's a cool feature
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains(changeName) || collision.gameObject.CompareTag("FakeWall"))
        {
            Blast();
        }
    }

    //Whoever threw the bomb shouldn't be hurt
    //Just remember, if the enemy is throwing it, the bomb's damage MUST be an integer
    private void Blast()
    {
        Collider[] hit = Physics.OverlapSphere(transform.position, blastRad);
        foreach (Collider collider in hit)
        {
            //Hell
            if (collider.gameObject.CompareTag("FakeWall"))
            {
                Destroy(collider.gameObject);
            } else if (collider.gameObject.name.Contains("Enemy") && changeName == "Enemy")
            {
                Vector3 enemyDist = collider.transform.position - transform.position;
                collider.gameObject.GetComponent<Target>().TakeDamage(damage - (enemyDist.magnitude * falloff));
            } else if (collider.gameObject.name == "Player" && changeName == "Player")
            {
                collider.gameObject.GetComponent<PlayerHealth>().PlayerTakeDamage(((int)damage));
            }
        }
        Destroy(gameObject);
    }
}

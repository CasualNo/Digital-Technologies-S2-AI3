using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using Random = System.Random;

public class EnemySpawn : MonoBehaviour
{
    public int numEnemies = 1;
    public GameObject prefab;
    Random rand = new Random();
    [SerializeField] List<GameObject> SpawnPoints;
    [SerializeField] List<GameObject> Turrets;
    [SerializeField] bool randomised;
    [SerializeField] bool boss;
    [SerializeField] bool lockDoors;
    [SerializeField] List<GameObject> doors; //put door collider triggers here

    void MakeAChild(int totalEnemies)
    {
        for (int i = 0; i < totalEnemies; i++)
        {
            Transform SpawnLocation;
            SpawnPointData SpawnObj;

            if (SpawnPoints.Count > 0 && randomised)
            {
                SpawnLocation = SpawnPoints[rand.Next(SpawnPoints.Count)].transform;
            } else if (SpawnPoints.Count > 0 && i <= SpawnPoints.Count - 1)
            {
                SpawnLocation = SpawnPoints[i].transform;
            } else
            {
                SpawnLocation = transform;
            }
            if (SpawnLocation.gameObject.TryGetComponent(out SpawnObj))
            {
                if (SpawnObj.respawn || SpawnObj.alive)
                {
                    GameObject newChild = Instantiate(SpawnObj.prefab, SpawnLocation.position, Quaternion.identity, transform);
                    Target enemy = newChild.GetComponent<Target>();
                    //stores enemy's spawn point's data in the enemy
                    enemy.spawn = SpawnObj;
                    enemy.target = SpawnLocation.position + new Vector3(0.001f, 0, 0);
                }
            } else if (SpawnLocation == transform)
            {
                GameObject newChild = Instantiate(prefab, SpawnLocation.position, Quaternion.identity);
                newChild.transform.parent = transform;
            }
        }
    }
    public void OnTriggerExit(Collider other)
    {
        //When Player exits trigger, kills all children
        if (other.transform.root.CompareTag("Player"))
        {
            for (int c = 0; c < transform.childCount; c++)
            {
                Destroy(transform.GetChild(c).gameObject);
            }
            foreach (GameObject turret in Turrets)
            {
                turret.GetComponent<Turret>().enabled = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.CompareTag("Player") && transform.childCount == 0)
        {
            MakeAChild(numEnemies);
            foreach (GameObject turret in Turrets)
            {
                turret.GetComponent<Turret>().enabled = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.root.CompareTag("Player"))
        {
            List<Target> ais = new();
            ais.AddRange(GetComponentsInChildren<Target>());
            Vector3 diff = other.transform.position - transform.position;
            if ((!boss && diff.x < 15.5 && diff.x > -15.5 && diff.z < 15.5 && diff.z > -15.5) || (boss && diff.x < 30.5 && diff.x > -30.5 && diff.z < 30.5 && diff.z > -30.5))
            {
                other.GetComponent<PlayerHealth>().spawnArea = this;
                foreach (Target ai in ais)
                {
                    ai.target = other.transform.position;
                    ai.pTarget = true;
                }
                if (lockDoors && transform.childCount == 0)
                {
                    foreach (GameObject door in doors)
                    {
                        door.GetComponent<BoxCollider>().enabled = true;
                    }
                    lockDoors = false;
                } else if (lockDoors)
                {
                    foreach (GameObject door in doors)
                    {
                        door.GetComponent<BoxCollider>().enabled = false;
                        door.GetComponent<DoorCollider>().isUp = true;
                    }
                }

            }
            else
            {
                foreach (Target ai in ais)
                {
                    ai.target = ai.spawn.transform.position;
                    ai.health = ai.maxhealth;
                    ai.pTarget = false;
                }
            }
        }
    }
}

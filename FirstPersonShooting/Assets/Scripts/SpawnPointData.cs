using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointData : MonoBehaviour
{
    // Stores the enemy prefab, if it respawns, and if it is alive
    public GameObject prefab;
    public bool respawn;
    [HideInInspector] public bool alive = true;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombCollection : MonoBehaviour
{
    [SerializeField] private int bombValue = 1;
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.CompareTag("Player"))
        {
            GameManager.instance.ChangeBombs(bombValue);
            Destroy(gameObject);
        }
    }
}

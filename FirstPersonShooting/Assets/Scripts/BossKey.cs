using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossKey : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.CompareTag("Player"))
        {
            GameManager.instance.hasBK = true;
            GameManager.instance.Update();
            Destroy(gameObject);
        }
    }
}

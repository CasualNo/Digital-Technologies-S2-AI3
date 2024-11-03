using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUp : MonoBehaviour
{
    [SerializeField] private float containerAmt = 5f;
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().originalPlayerHealth += containerAmt * 5;
            other.GetComponent<PlayerHealth>().playerHealth = other.GetComponent<PlayerHealth>().originalPlayerHealth;
            for (int i = 0; i < containerAmt; i++) //Each function call only adds 1 container, so need to loop for as long as needed
            {
                ZeldaHealthScript.instance.AddContainer();
            }
            Destroy(gameObject);
        }
    }
}

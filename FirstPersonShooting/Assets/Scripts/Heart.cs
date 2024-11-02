using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    [SerializeField] private float healAmt = 5f;
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.CompareTag("Player"))
        {
            PlayerHealth pH = other.GetComponent<PlayerHealth>();
            if (pH.playerHealth < pH.originalPlayerHealth)
                pH.playerHealth += healAmt;
            if (pH.playerHealth > pH.originalPlayerHealth)
                pH.playerHealth = pH.originalPlayerHealth;
            ZeldaHealthScript.instance.SetCurrentHealth(pH.playerHealth / 5);
            Destroy(gameObject);
        }
    }
    void Update()
    {
        if (transform.rotation.eulerAngles.z > 360)
        {
            transform.Rotate(0, 0, -360);
        }
        transform.Rotate(0, 0, 180 * Time.deltaTime);
    }
}

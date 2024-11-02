using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chasm : MonoBehaviour
{
    public float falldamage = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            PlayerHealth health = other.GetComponent<PlayerHealth>();
            CharacterController control = other.GetComponent<CharacterController>();
            PlayerController player = other.GetComponent<PlayerController>();
            if (health.playerHealth - falldamage > 0)
            {
                control.enabled = false;
                control.transform.position = player.groundpos;
                control.enabled = true;
            }
            health.PlayerTakeDamage(falldamage);
        }
    }
}

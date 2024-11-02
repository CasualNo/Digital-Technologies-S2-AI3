using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class PlayerHealth : MonoBehaviour
{
    public float playerHealth = 100;
    [HideInInspector] public float originalPlayerHealth;
    Random rand = new Random();
    [HideInInspector] public CharacterController Player;
    [HideInInspector] public bool dmgInv = false;
    [SerializeField] int invTime;
    Vector3 startPos;
    public Shield shield;

    private void Awake()
    {
        originalPlayerHealth = playerHealth;
        startPos = transform.position;
        Player = gameObject.GetComponent("CharacterController") as CharacterController;
        shield = GetComponent<Shield>();
    }

    void InvEnd()
    {
        dmgInv = false;
    }

    public void PlayerTakeDamage(float amount)
    {
        if (!shield.blocking)
        {
            dmgInv = true;
            playerHealth -= amount;
            Invoke("InvEnd", invTime);
        }
        if (playerHealth <= 0)
        {
            playerHealth = originalPlayerHealth;
            Die();
        }
        ZeldaHealthScript.instance.SetCurrentHealth(playerHealth / 5);
    }

    void Die()
    {
        Player.enabled = false;
        Player.transform.position = startPos;
        Player.enabled = true;
    }
}

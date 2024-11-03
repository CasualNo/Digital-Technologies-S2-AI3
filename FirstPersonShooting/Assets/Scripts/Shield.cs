using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{

    private CharacterInput controls;
    private sword sword;
    [HideInInspector] public bool blocking = false;
    [SerializeField] GameObject shield;

    void Awake()
    {
        controls = new CharacterInput();
        sword = gameObject.GetComponentInChildren<sword>();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    void Block()
    {
        blocking = true;
        sword.enabled = false;
        shield.SetActive(true);
        Invoke("BlockEnd", 1);
    }

    void BlockEnd()
    {
        blocking = false;
        sword.enabled = true;
        shield.SetActive(false);
    }

    void Update()
    {
        if(controls.Player.Shield.triggered && !blocking)
            Block();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sword : MonoBehaviour
{
    private CharacterInput controls;
    public float damage = 10f;
    public float range = 10f;
    public Camera fpsCam;

    private void Awake()
    {
        controls = new CharacterInput();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    void Update()
    {
        if (controls.Player.Shoot.triggered)
            slash();
    }

    void slash()
    {

        //animation trigger here

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out RaycastHit hit, range))
        {
            if (hit.transform.TryGetComponent(out Target enemy))
                enemy.TakeDamage(damage);
        }
    }
}

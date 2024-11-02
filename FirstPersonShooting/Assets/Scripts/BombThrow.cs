using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombThrow : MonoBehaviour
{
    private CharacterInput controls;
    public GameObject bombProjec;
    public int throwSpd;
    private void Awake()
    {
        controls = new CharacterInput();
        bombProjec = Resources.Load("Prefabs/Bombs/Bomb projectile") as GameObject;
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
        if (controls.Player.Bomb.triggered && GameManager.instance.bombs > 0)
        {
            Throw();
            GameManager.instance.ChangeBombs(-1);
        }
    }

    private void Throw()
    {
        Vector3 initpos = transform.position + transform.forward;
        Quaternion initrot = new Quaternion();
        initrot.eulerAngles = transform.eulerAngles + new Vector3(0f, 0f, -90f);
        GameObject bomb = Instantiate(bombProjec, initpos, initrot);
        bomb.GetComponent<Rigidbody>().velocity = transform.forward * throwSpd;
        bomb.GetComponent<Explode>().ownerName = transform.root.name;
    }
}

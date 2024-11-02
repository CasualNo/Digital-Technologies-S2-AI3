using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DoorCollider : MonoBehaviour
{
    //for door script
    [HideInInspector] public bool isUp = true;
    public float speed = 7;

    bool unlock = false;
    public bool BossDoor = false;
    public TextMeshProUGUI text;
    public bool isLocked = false;
    private CharacterInput controls;

    private void Awake()
    {
        controls = new CharacterInput();
        text = GameObject.Find("Prompt (TMP)").GetComponent<TextMeshProUGUI>();
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
        if(unlock && controls.Player.Interact.triggered && !BossDoor && GameManager.instance.keys > 0)
        {
            GameManager.instance.ChangeKeys(-1);
            Unlock();
        } else if (unlock && controls.Player.Interact.triggered && BossDoor && GameManager.instance.hasBK)
        {
            GameManager.instance.hasBK = false;
            Unlock();
        }
    }
    void Unlock()
    {
        isLocked = false;
        text.text = ("");
        isUp = false;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == ("Player") && isLocked == true && !BossDoor)
        {
            text.text = ("Open Door?\n(1 Key)");
            unlock = true;
        } else if (other.gameObject.tag == "Player" && isLocked == true)
        {
            text.text = ("Open Boss Door?\n(Need Boss Key)");
            unlock = true;
        } else if (other.gameObject.tag == ("Player"))
        {
            isUp = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == ("Player"))
        {
            text.text = ("");
            unlock = false;
            isUp = true;
        }
    }
}

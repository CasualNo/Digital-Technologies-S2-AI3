using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	private CharacterInput controls;
    private Vector3 velocity;
    private Vector2 move;

    private CharacterController controller;

    public float moveSpeed = 6f;
    public float jumpHeight = 2.4f;
    public float gravity = -9.81f;

    [HideInInspector] public Vector3 groundpos;
    private bool mP;
    public Transform ground;
    public float distanceToGround = 0.4f;
    public LayerMask groundMask;
	
    void Awake()
    {
		controls = new CharacterInput();
		controller = GetComponent<CharacterController>();
    }

    void Update()
    {
		PlayerMovement();
		Grav();
		Jump();
    }

    private void Grav()
    {
		if (isGrounded() && velocity.y < 0){
			velocity.y = -2f;
            GroundSet();
		}
		velocity.y += gravity * Time.deltaTime;
		controller.Move(velocity * Time.deltaTime);
    }

    private void GroundSet()
    {
        foreach (Collider col in Physics.OverlapSphere(ground.position, distanceToGround, groundMask))
        {
            if (col.gameObject.GetComponent<MovingPlatform>())
            {
                mP = true;
                break;
            }
        }
        if (!mP)
            groundpos = transform.position;
        else
            mP = false;
    }

    private bool isGrounded()
    {            
        return Physics.CheckSphere(ground.position, distanceToGround, groundMask);
    }

    private void PlayerMovement()
    {
		move = controls.Player.Movement.ReadValue<Vector2>();
		Vector3 movement = (move.y * transform.forward) + (move.x * transform.right);
		controller.Move(movement * moveSpeed * Time.deltaTime);
    }

    private void Jump()
    {
		if (controls.Player.Jump.triggered && isGrounded()){
			velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
		}
    }

    void OnEnable()
    {
		controls.Enable();
    }

    void OnDisable()
    {
		controls.Disable();
    }
}

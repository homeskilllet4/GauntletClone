using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameObject projectilePrefab;
    [SerializeField]
    private float playerSpeed = 2.0f;

    private CharacterController controller;
    private PlayerInput playerInput;
    private Vector3 playerVelocity;


    private Vector2 movementInput = Vector2.zero;

    private void Awake()
    {
        playerInput = new PlayerInput();
        playerInput.Enable();
    }
    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        projectilePrefab = GetComponent<PlayerClass>()._charClass.projectilePrefab;
    }



    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
             
        }
    }


    void FixedUpdate()
    {
        if (playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(movementInput.x, 0, movementInput.y);
        controller.Move(move * Time.deltaTime * playerSpeed);
        controller.Move(playerVelocity * Time.deltaTime);
        Quaternion targetRotation = Quaternion.LookRotation(move);
        if(move != Vector3.zero)
            transform.rotation = targetRotation;
        //Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }

}
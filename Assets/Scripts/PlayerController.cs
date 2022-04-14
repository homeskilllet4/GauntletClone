using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerInput playerInputActions;
    public Vector3 speed;
    public Vector2 movementDirection;


    public void Awake()
    {
        playerInputActions = new PlayerInput();
        playerInputActions.Enable();
    }

    public void Update()
    {
        movementDirection = playerInputActions.PlayerMovement.Movement.ReadValue<Vector2>();
        transform.position = new Vector3(transform.position.x + movementDirection.x, transform.position.y, transform.position.z + movementDirection.y);
        Debug.Log(movementDirection);
    }
}

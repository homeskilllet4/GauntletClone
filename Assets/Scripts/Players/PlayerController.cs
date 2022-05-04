using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float gravityValue = -9.8f;

    private CharacterController controller;
    private PlayerInput playerInput;

    public GameObject playerProjectile;
    public GameObject projectileSpawn;

    private Vector3 playerVelocity;


    private Vector2 movementInput = Vector2.zero;

    private void Awake()
    {
        playerInput = new PlayerInput();
        playerInput.Enable();
    }
    private void Start()
    {
        StartCoroutine(SpawnWait());
        controller = gameObject.GetComponent<CharacterController>();
    }

    IEnumerator SpawnWait()
    {
        yield return new WaitForSeconds(.2f);
        playerProjectile = GetComponent<PlayerClass>().charClass.playerProjectile;
        playerProjectile.GetComponent<Renderer>().material = GetComponent<Renderer>().material;
    }


    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }


    public void PlayerAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Instantiate(playerProjectile, projectileSpawn.transform.position, transform.rotation);
        }
    }



    void Update()
    {
        if (playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(movementInput.x, 0, movementInput.y);
        controller.Move(move * Time.deltaTime * playerSpeed);
        controller.Move(playerVelocity * Time.deltaTime);
        if(move != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(move);


    }

}
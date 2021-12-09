using Sirenix.OdinInspector;
using System;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{

    [SerializeField, FoldoutGroup("Refrences"), ReadOnly] private InputManager input;
    [SerializeField, FoldoutGroup("Refrences"), ReadOnly] private Transform camTransform;
    [SerializeField, FoldoutGroup("Refrences"), ReadOnly] private CharacterController controller;
    [FoldoutGroup("Properties"), ReadOnly] public Vector3 playerVelocity;
    [SerializeField, FoldoutGroup("Properties"), ReadOnly] private bool groundedPlayer;
    [SerializeField, FoldoutGroup("Properties")] private float playerSpeed = 2.0f;
    [SerializeField, FoldoutGroup("Properties")] private float jumpHeight = 1.0f;
    [SerializeField, FoldoutGroup("Properties")] private float gravityValue = -9.81f;
    public static Transform playerTransform;
    public static bool canMove;
    private void Awake()
    {
        controller = gameObject.GetComponent<CharacterController>();
        canMove = true;

    }
    private void Start()
    {
        input = InputManager.Instance;
        camTransform = Camera.main.transform;
        playerTransform = transform;



    }
    void Update()
    {
        SetGrounded();
        if (canMove)
        {
            Move();
            Jump();
        }
        
    }

  
    private void Jump()
    {
        if (input.PlayerJumpedThisFrame() && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    private void Move()
    {
        Vector2 Inputmovement = input.GetPlayerMovement();
        Vector3 move = new Vector3(Inputmovement.x, 0, Inputmovement.y);
        move = camTransform.forward * move.z + camTransform.right * move.x;
        move.y = 0;
        controller.Move(move * Time.deltaTime * playerSpeed);
    }

    private void SetGrounded()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
    }
}

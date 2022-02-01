using Sirenix.OdinInspector;
using System;
using System.Collections;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{

    [SerializeField, FoldoutGroup("Refrences"), ReadOnly] private InputManager input;
    [SerializeField, FoldoutGroup("Refrences"), ReadOnly] private Transform camTransform;
    [SerializeField, FoldoutGroup("Refrences"), ReadOnly] private CharacterController controller;
    [SerializeField, FoldoutGroup("Refrences"), ReadOnly] private Breath m_breath;
    [SerializeField, FoldoutGroup("Refrences")] private CinemachinePOVExtension m_CinematicCamera;
    [FoldoutGroup("Properties")] public LayerMask groundedLayers;
    [FoldoutGroup("Properties"), ReadOnly] public Vector3 playerVelocity;
    [SerializeField, FoldoutGroup("Properties"), ReadOnly] private bool groundedPlayer;
    [SerializeField, FoldoutGroup("Properties")] private float playerSpeed = 2.0f;
    [SerializeField, FoldoutGroup("Properties")] private float jumpHeight = 1.0f;
    [SerializeField, FoldoutGroup("Properties")] private float gravityValue = -9.81f;
    [SerializeField, FoldoutGroup("Properties")] private float GroundCheckLength = -9.81f;
    [SerializeField, FoldoutGroup("Properties_Breath")] private float sprintSpeed = 1.5f;
    [SerializeField, FoldoutGroup("Properties_Breath"), ReadOnly] private bool doSprint = false;
    [SerializeField, FoldoutGroup("Properties_Breath"), ReadOnly] private float sprintMod = 1f;
    [SerializeField, FoldoutGroup("Properties_Breath")] private float sprintBreathCost = 20f;
    [SerializeField, FoldoutGroup("Properties_Breath")] private float jumpBreathCost = 15f;
    [SerializeField, FoldoutGroup("Properties_Breath")] private float SprintFOV = 55f;


    public static Transform playerTransform;
    public static bool canMove;
    private bool changeFOVNoInput;
    private void Awake()
    {
        m_breath = GetComponent<Breath>();
        controller = gameObject.GetComponent<CharacterController>();
        canMove = true;
     
    }
    private void Start()
    {
        input = InputManager.Instance;
        camTransform = Camera.main.transform;
        playerTransform = transform;
        input.OnPlayerStartedSprint.AddListener(FlipDoSprint);
        input.OnPlayerCanceledSprint.AddListener(FlipDoSprint);

    }
    void Update()
    {
        SetGrounded();
        if (canMove)
        {
            Jump();
            Move();
            BreathboundMovement();
        }
    }
    private void BreathboundMovement()
    {
        if (m_breath.current > 0)
        {
            DoSprint();
        }
        else
        {

            if (!changeFOVNoInput)
            {
                StartCoroutine(FOVScalingRoutine(m_CinematicCamera.StartingFov));
                changeFOVNoInput = true;

            }
            sprintMod = 1;
        }
    }
    private void DoSprint()
    {
        if (doSprint && input.GetPlayerMovement().y > 0)
        {
            sprintMod = sprintSpeed;
            m_breath.LoseBreath(sprintBreathCost * Time.deltaTime);
        }
        else
        {
            
            sprintMod = 1f;
        }
    }

    private void Jump()
    {
        if (input.PlayerJumpedThisFrame() && groundedPlayer)
        {
            playerVelocity.y = jumpHeight;
            m_breath.LoseBreath(jumpBreathCost);
        }

        Gravity();
    }

    private void Gravity()
    {
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    private void Move()
    {
        Vector2 Inputmovement = input.GetPlayerMovement();
        Vector3 move = new Vector3(Inputmovement.x, 0, Inputmovement.y);
        move = camTransform.forward * move.z + camTransform.right * move.x;
        move.y = 0;
        controller.Move(move * Time.deltaTime * playerSpeed * sprintMod);
    }

    private void SetGrounded()
    {
        groundedPlayer = Physics.Raycast(transform.position,Vector3.down, GroundCheckLength, groundedLayers);
        
      
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(transform.position, Vector3.down * GroundCheckLength);
    }
    private void FlipDoSprint()
    {
        doSprint = !doSprint;
        float FOVToSet = doSprint ? SprintFOV : m_CinematicCamera.StartingFov;

        if(m_CinematicCamera.FOV != FOVToSet)
            StartCoroutine(FOVScalingRoutine(FOVToSet));



    }
    IEnumerator FOVScalingRoutine(float goal)
    {
        while (m_CinematicCamera.FOV != goal)
        {
             m_CinematicCamera.FOV = Mathf.MoveTowards(m_CinematicCamera.FOV, goal, Time.deltaTime * 20);
             yield return new WaitForEndOfFrame();

        }
        changeFOVNoInput = false;
    }
}

using Sirenix.OdinInspector;
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
    [SerializeField, FoldoutGroup("Refrences")] private AudioSource m_audioSource;

    [FoldoutGroup("Properties")] public LayerMask groundedLayers;
    [FoldoutGroup("Properties"), ReadOnly] public Vector3 playerVelocity;
    [SerializeField, FoldoutGroup("Properties"), ReadOnly] private bool isGrounded;
    [SerializeField, FoldoutGroup("Properties"), ReadOnly] private bool isWalking;
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
    [SerializeField, FoldoutGroup("Properties_Leap"), ReadOnly] private bool canLeap;
    [SerializeField, FoldoutGroup("Properties_Leap")] private float leapForce;
    [SerializeField, FoldoutGroup("Properties_Leap")] public float LeapCD;

    public static Transform playerTransform;
    public static bool canMove;
    private bool OnBreathDepletedFlag;



    private void Awake()
    {
        m_breath = GetComponent<Breath>();
        controller = gameObject.GetComponent<CharacterController>();
        canMove = true;
        canLeap = true;
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
            Leap();
            Move(GetMoveInput());
            BreathboundMovement();
        }

        
    }

    private Vector3 GetMoveInput()
    {
        Vector2 Inputmovement = input.GetPlayerMovement();
        Vector3 move = new Vector3(Inputmovement.x, 0, Inputmovement.y);
        move = camTransform.forward * move.z + camTransform.right * move.x;
        move.y = 0;
        return move;
    }

    private void BreathboundMovement()
    {
        if (m_breath.current > 0)
        {
            DoSprint();
        }
        else
        {

            if (!OnBreathDepletedFlag)
            {
                StartCoroutine(m_CinematicCamera.FOVScalingRoutine(m_CinematicCamera.StartingFOV));
                OnBreathDepletedFlag = true;

            }
            sprintMod = 1;
        }
    }
    private void DoSprint()
    {
        if (doSprint && input.GetPlayerMovement().y > 0)
        {
            if (sprintMod != sprintSpeed)
            {
                m_audioSource.clip = SoundManager.GetAudioClip(SoundManager.Sound.PlayerSprint);
                m_audioSource.Play();
                PostProccessManipulator.SetLensDistortion();
                CinemachineCameraShaker.instance.ShakeCamera(60, 5f, 0.05f);
                sprintMod = sprintSpeed;
            }
            m_breath.LoseBreath(sprintBreathCost * Time.deltaTime);
        }
        else
        {
            if(sprintMod != 1)
            {
                m_audioSource.Stop();
                m_audioSource.clip = SoundManager.GetAudioClip(SoundManager.Sound.PlayerWalk);
                m_audioSource.Play();
            }
            sprintMod = 1f;
        }
    }

    private void Jump()
    {
        if (input.PlayerJumpedThisFrame() && isGrounded)
        {
            SoundManager.Play(SoundManager.Sound.PlayerJump, m_audioSource, 0.35f);
            playerVelocity.y = jumpHeight;
            m_breath.LoseBreath(jumpBreathCost);
        }

        Gravity();
    }
    private void Leap()
    {
        if (input.PlayerJumpedThisFrame() && !isGrounded && canLeap)
        {
            if (GetMoveInput() != Vector3.zero)
                playerVelocity = GetMoveInput() + (Vector3.up * 0.25f) * leapForce;
            else
                playerVelocity = camTransform.forward + (Vector3.up * 0.25f) * leapForce;

            SoundManager.Play(SoundManager.Sound.PlayerLeap, m_audioSource, 0.4f);
            canLeap = false;
            StartCoroutine(cooldown());
        }
        IEnumerator cooldown()
        {
            yield return new WaitForSecondsRealtime(LeapCD);
            canLeap = true;
        }
    }
    private void Gravity()
    {
        playerVelocity.y += gravityValue * Time.deltaTime;
        // controller.Move(playerVelocity * Time.deltaTime);
    }

    private void Move(Vector3 move)
    {
        if (move != Vector3.zero)
        {
            if (!isWalking)
            {
                isWalking = true;
                m_audioSource.clip = SoundManager.GetAudioClip(SoundManager.Sound.PlayerWalk);
                m_audioSource.Play();
            }

        }
        else
        {
            if (isWalking)
            {
                isWalking = false;
                m_audioSource.Stop();

            }
        }
        controller.Move(playerSpeed * sprintMod * Time.deltaTime * (move + playerVelocity));

    }

    private void SetGrounded()
    {

        if (Physics.Raycast(transform.position, Vector3.down, GroundCheckLength, groundedLayers))
        {
            if (!isGrounded)
            {
                SoundManager.Play(SoundManager.Sound.PlayerLand, m_audioSource, 0.05f);
                isGrounded = true;
                playerVelocity = Vector3.zero;
            }
        }
        else
        {
            if (isGrounded)
                isGrounded = false;
        }

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(transform.position, Vector3.down * GroundCheckLength);
    }
    private void FlipDoSprint()
    {
        doSprint = !doSprint;
        if (!doSprint)
        {
            CinemachineCameraShaker.instance.CameraReset();
            PostProccessManipulator.ResetLensDistortion();
        }
        float FOVToSet = doSprint ? SprintFOV : m_CinematicCamera.StartingFOV;
        if (m_CinematicCamera.FOV != FOVToSet)
        {
            StartCoroutine(m_CinematicCamera.FOVScalingRoutine(FOVToSet));
            OnBreathDepletedFlag = false;
        }




    }

   
}

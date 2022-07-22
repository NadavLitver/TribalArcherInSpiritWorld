using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{

    [SerializeField, FoldoutGroup("Refrences"), ReadOnly] private InputManager input;
    [SerializeField, FoldoutGroup("Refrences"), ReadOnly] private Transform camTransform;

    [SerializeField, FoldoutGroup("Refrences"), ReadOnly] private Camera cam;


    [SerializeField, FoldoutGroup("Refrences"), ReadOnly] private CharacterController controller;
    [SerializeField, FoldoutGroup("Refrences"), ReadOnly] private Breath m_breath;
    [SerializeField, FoldoutGroup("Refrences")] private CinemachinePOVExtension m_CinematicCamera;
    [SerializeField, FoldoutGroup("Refrences")] private AudioSource m_audioSource;
    [SerializeField, FoldoutGroup("Refrences")] private Transform camFollow;
    [SerializeField, FoldoutGroup("Refrences")] private Animator m_animator;
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
    [SerializeField, FoldoutGroup("Properties_Leap")] public UnityEvent leapEvent;

    [SerializeField, FoldoutGroup("Properties_Slide")] private float slopeSpeed = 8;
    private Vector3 hitPointNormal;
    private bool isSliding
    {
        get
        {
            if (controller.isGrounded && Physics.Raycast(transform.position, Vector3.down, out RaycastHit slopeHit, 2f, groundedLayers))
            {
                hitPointNormal = slopeHit.normal;
                return Vector3.Angle(hitPointNormal, Vector3.up) > controller.slopeLimit;
            }
            else
            {
                return false;
            }
        }
    }

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
        cam = Camera.main;
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
        camFollow.rotation = new Quaternion(0, camTransform.rotation.y, 0, camTransform.rotation.w);
        Vector2 Inputmovement = input.GetPlayerMovement();
        Vector3 move = new Vector3(Inputmovement.x, 0, Inputmovement.y);
        move = camFollow.forward * move.z + camFollow.right * move.x;
        move.y = 0;
        return move;
    }
    //private Vector3 Direction()
    //{

    //    Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
    //    Vector3 targetPoint;
    //    if (Physics.Raycast(ray, out RaycastHit hit))
    //    {
    //        targetPoint = hit.point;
    //    }
    //    else
    //    {
    //        targetPoint = ray.GetPoint(100);
    //    }
    //    Vector3 direction = (targetPoint - transform.position);
    //    return direction;
    //}
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
                m_audioSource.Stop();
                m_audioSource.clip = GetCurrentRunClip();
                m_audioSource.Play();

                PostProccessManipulator.SetLensDistortion();
                CinemachineCameraShaker.instance.ShakeCamera(60, 5f, 0.05f);
                sprintMod = sprintSpeed;
            }
            m_breath.LoseBreath(sprintBreathCost * Time.deltaTime);
        }
        else
        {
            if (sprintMod != 1)
            {
                m_audioSource.Stop();
                m_audioSource.clip = GetCurrentWalkClip();
                m_audioSource.Play();
            }
            sprintMod = 1f;
        }
    }
    public AudioClip GetCurrentWalkClip()
    {
        int ranNum = Randomizer.ReturnRandomNum(new Vector2Int(0, 4));
        switch (ranNum)
        {
            case 0:
                return SoundManager.GetAudioClip(SoundManager.Sound.PlayerWalk);
            case 1:
                return SoundManager.GetAudioClip(SoundManager.Sound.PlayerWalk2);
            case 2:
                return SoundManager.GetAudioClip(SoundManager.Sound.PlayerWalk3);
            case 3:
                return SoundManager.GetAudioClip(SoundManager.Sound.PlayerWalk4);
            default:
                return SoundManager.GetAudioClip(SoundManager.Sound.PlayerWalk);
        }
    }
    public AudioClip GetCurrentRunClip()
    {
        int ranNum = Randomizer.ReturnRandomNum(new Vector2Int(0, 4));
        switch (ranNum)
        {
            case 0:
                return SoundManager.GetAudioClip(SoundManager.Sound.PlayerSprint);
            case 1:
                return SoundManager.GetAudioClip(SoundManager.Sound.PlayerSprint2);
            case 2:
                return SoundManager.GetAudioClip(SoundManager.Sound.PlayerSprint3);
            case 3:
                return SoundManager.GetAudioClip(SoundManager.Sound.PlayerSprint4);
            default:
                return SoundManager.GetAudioClip(SoundManager.Sound.PlayerSprint);

        }
    }
    private void Jump()
    {
        if (input.PlayerJumpedThisFrame() && isGrounded)
        {
            SoundManager.Play(SoundManager.Sound.PlayerJump, m_audioSource, 0.35f);
            playerVelocity.y = jumpHeight;
            m_breath.LoseBreath(jumpBreathCost);
            m_animator.SetTrigger("Jump");
        }

        Gravity();
    }
    private void Leap()
    {
        if (input.PlayerJumpedThisFrame() && !isGrounded && canLeap)
        {
            leapEvent?.Invoke();
            if (GetMoveInput() != Vector3.zero)
                playerVelocity = GetMoveInput() + (Vector3.up * 0.2f) * leapForce;
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
        Vector3 moveDelta;
        moveDelta = playerSpeed * sprintMod * Time.deltaTime * (move + playerVelocity);
        if (isSliding)
        {
            moveDelta += new Vector3(hitPointNormal.x, -hitPointNormal.y, hitPointNormal.z) * slopeSpeed * Time.deltaTime;
        }
        m_animator.SetBool("NormalWalk", isWalking);
        controller.Move(moveDelta);
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
                m_animator.SetTrigger("Land");
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
    public void Stop(float duration)
    {
        StartCoroutine(StopRoutine(duration));
        IEnumerator StopRoutine(float duration)
        {
            canMove = false;
            yield return new WaitForSeconds(duration);
            canMove = true;

        }
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

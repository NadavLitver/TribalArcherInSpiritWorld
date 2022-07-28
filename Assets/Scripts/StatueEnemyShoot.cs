using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

public class StatueEnemyShoot : State
{
    [SerializeField] private LineRenderer aimLine;
    [SerializeField] private LaserBeam beamLine;
    private float timeInState;
    [SerializeField] private float aimTime;
    [SerializeField] private float shootTime;
    [SerializeField] private float BreathTimeAfterFinishShoot;
    [SerializeField] private float faceTargetSpeed;
    [SerializeField] private float rotateTowardsTargetSpeed;
    [SerializeField,ReadOnly] private bool startedAim, startedShoot, canAim, CanShoot;
    [SerializeField] private AudioSource m_audioSource;
    [SerializeField] private float faceTargetSpeedAttackMod = 0.5f; // facing target while attacking
    [SerializeField] private LookAtHandler lookAtObject;
    private Vector3 stopAimPos;
    [SerializeField] private float RotOffset = 25f;
    [SerializeField] private Vector3 axis;
    [SerializeField] private AnimationCurve aimEase;
    protected override void OnStateDisabled()
    {

        aimLine.SetPosition(1, Vector3.zero);
        aimLine.gameObject.SetActive(true);
    }

    protected override void OnStateEnabled()
    {
        agent.SetDestination(transform.position);
        agent.isStopped = true;
        timeInState = 0;
        aimLine.gameObject.SetActive(true);
        stopAimPos = Vector3.zero;
        startedAim = false;
        startedShoot = false;
        ResetRotations();
    }
    private void ResetRotations()
    {
        aimLine.transform.localRotation = Quaternion.identity;
        beamLine.transform.localRotation = Quaternion.identity;

    }
    private void Update()
    {
        timeInState += Time.deltaTime;
      
        if (timeInState < aimTime)
        {
            if (!startedAim)
            {
                startedAim = true;
                StartCoroutine(animationDelay(0.1f, false));
                FaceTarget(timeInState / aimTime);
            }
            if (canAim)
            {
               
                AimBeam();
            }

        }
        else
        {
            if ((timeInState - aimTime) < shootTime)
            {
                if (!startedShoot)
                {

                    StartCoroutine(animationDelay(1f, true));
                    startedShoot = true;
                }
                if (CanShoot)
                { 
                    FaceTarget(faceTargetSpeedAttackMod);
                    ShootBeam();
                }
                else
                {
                    FaceTarget(faceTargetSpeedAttackMod / 2);

                }
            }
            else
            {
                if ((timeInState - aimTime - shootTime) > BreathTimeAfterFinishShoot)
                {
                    foreach (Animator item in _animators)
                    {
                        item.SetBool("Shooting", false);
                    }
                    stateHandler.body.animator.SetBool("Shooting", false);
                    SwapToNextState();
                }
            }
        }
        //stopAimPos = PlayerController.playerTransform.position;
        //stopAimPos.x = 0;
        //stopAimPos.y = -30;

    }
    public void AimBeam()
    {
       

        aimLine.SetPosition(1, stopAimPos);
       
     
    }
    void FaceTarget(float speedMod)
    {
        var turnTowardNavSteeringTarget = PlayerController.playerTransform.position;


        Vector3 direction = (turnTowardNavSteeringTarget - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        stateHandler.body.transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotateTowardsTargetSpeed * speedMod);
        float targetRot = lookAtObject.transform.rotation.eulerAngles.x;

        beamLine.transform.localRotation = Quaternion.Euler(Mathf.MoveTowards(beamLine.transform.localRotation.eulerAngles.x, targetRot, faceTargetSpeed * speedMod * Time.deltaTime), 0, 0);
        aimLine.transform.localRotation = Quaternion.Euler(Mathf.MoveTowards(aimLine.transform.localRotation.eulerAngles.x, targetRot, faceTargetSpeed * speedMod * Time.deltaTime), 0, 0);
    }
    public void ShootBeam()
    {
        beamLine.Attack();
        //beamLine.shootDirection = stopAimPos.normalized;
        
        aimLine.SetPosition(1, Vector3.zero);
        //  beamLine.SetPosition(1, stopAimPos);

    }
    IEnumerator animationDelay(float time, bool isShooting)
    {
        
        if (isShooting)
        {
            foreach (Animator item in _animators)
            {
                item.SetBool("Shooting", true);
            }
            yield return new WaitForSeconds(time);
            CanShoot = true;
            SoundManager.Play(SoundManager.Sound.StatueAttack, m_audioSource,1);

        }
        else
        {
            foreach (Animator item in _animators)
            {
                item.SetTrigger("Aim");
            }
            yield return new WaitForSeconds(time);
            canAim = true;
            SoundManager.Play(SoundManager.Sound.StatueAim, m_audioSource,1);

        }


    }
}

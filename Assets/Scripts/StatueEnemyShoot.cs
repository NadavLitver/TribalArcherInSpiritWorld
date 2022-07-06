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
    [SerializeField,ReadOnly] private bool startedAim, startedShoot, canAim, CanShoot;
    [SerializeField] private AudioSource m_audioSource;


    private Vector3 stopAimPos;
    protected override void OnStateDisabled()
    {

        aimLine.SetPosition(1, Vector3.zero);
        aimLine.gameObject.SetActive(true);
        beamLine.gameObject.SetActive(false);
    }

    protected override void OnStateEnabled()
    {
        agent.SetDestination(transform.position);
        agent.isStopped = true;
        timeInState = 0;
        aimLine.gameObject.SetActive(true);
        beamLine.gameObject.SetActive(false);
        stopAimPos = Vector3.zero;
        startedAim = false;
        startedShoot = false;

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
                    ShootBeam();
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
        stopAimPos = PlayerController.playerTransform.position;
        stopAimPos.x = 0;
        stopAimPos.y = -30;

    }
    public void AimBeam()
    {
       

        FaceTarget();
        aimLine.SetPosition(1, stopAimPos);
       
     
    }
    void FaceTarget()
    {
        var turnTowardNavSteeringTarget = PlayerController.playerTransform.position;

        Vector3 direction = (turnTowardNavSteeringTarget - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        stateHandler.body.transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * faceTargetSpeed);
    }
    public void ShootBeam()
    {
        if (!beamLine.gameObject.activeInHierarchy)
        {
            beamLine.gameObject.SetActive(true);
            beamLine.shootDirection = stopAimPos.normalized;
            beamLine.Attack();
        }
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

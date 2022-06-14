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
                StartCoroutine(animationDelay(0.9f, false));

            }
            if (canAim)
            {
                stopAimPos = new Vector3(0, PlayerController.playerTransform.position.y - 30, PlayerController.playerTransform.position.z);
                AimBeam();
            }

        }
        else
        {
            if ((timeInState - aimTime) < shootTime)
            {
                if (!startedShoot)
                {

                    StartCoroutine(animationDelay(1.2f, true));
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
                    stateHandler.body.animator.SetBool("Shooting", false);
                    SwapToNextState();
                }
            }
        }

    }
    public void AimBeam()
    {
        aimLine.SetPosition(1, stopAimPos);

        FaceTarget();

        void FaceTarget()
        {
            var turnTowardNavSteeringTarget = PlayerController.playerTransform.position;

            Vector3 direction = (turnTowardNavSteeringTarget - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            stateHandler.body.transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * faceTargetSpeed);
        }
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
            stateHandler.body.animator.SetBool("Shooting", true);
            yield return new WaitForSeconds(time);
            CanShoot = true;
        }
        else
        {

            stateHandler.body.animator.SetTrigger("Aim");
            yield return new WaitForSeconds(time);
            canAim = true;

        }


    }
}

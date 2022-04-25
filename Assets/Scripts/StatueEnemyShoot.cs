using UnityEngine;

public class StatueEnemyShoot : State
{
    [SerializeField] private LineRenderer aimLine;
    [SerializeField] private LineRenderer beamLine;
    private float timeInState;
    [SerializeField] private float aimTime;
    [SerializeField] private float shootTime;
    [SerializeField] private float BreathTimeAfterFinishShoot;
    [SerializeField] private float faceTargetSpeed;


    private Vector3 stopAimPos;
    protected override void OnStateDisabled()
    {
    }

    protected override void OnStateEnabled()
    {
        agent.SetDestination(transform.position);
        agent.isStopped = true;
        timeInState = 0;
        aimLine.gameObject.SetActive(true);
        beamLine.gameObject.SetActive(false);
        stopAimPos = Vector3.zero;

    }
    private void Update()
    {
        timeInState += Time.deltaTime;
        if (timeInState < aimTime)
        {
            stopAimPos = new Vector3( 0, PlayerController.playerTransform.position.y -30, PlayerController.playerTransform.position.z);
            AimBeam();
        }
        else
        {
            if ((timeInState - aimTime) < shootTime)
            {
                ShootBeam();
            }
            else
            {
                if ((timeInState - aimTime - shootTime) > BreathTimeAfterFinishShoot)
                {
                    aimLine.SetPosition(1, Vector3.zero);
                    aimLine.gameObject.SetActive(true);
                    beamLine.gameObject.SetActive(false);
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
        }
        aimLine.SetPosition(1, Vector3.zero);
        beamLine.SetPosition(1, stopAimPos);

    }
}

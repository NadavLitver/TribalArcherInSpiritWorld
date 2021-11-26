using Cinemachine;
using UnityEngine;

public class CinemachinePOVExtension : CinemachineExtension
{
    private InputManager input;
    private Vector3 startingRotation;
    [SerializeField]
    private float clampAngle = 85;
    [SerializeField,Tooltip("Look Speed/Sensativity")]
    private float horizontalSpeed = 10;
    [SerializeField,Tooltip("Look Speed/Sensativity")]
    private float VerticalSpeed = 10;
    protected override void Awake()
    {
        input = InputManager.Instance;
        base.Awake();
    }
    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (!Application.isPlaying) return;

        if (vcam.Follow)
        {
            if(stage == CinemachineCore.Stage.Aim)
            {
                if (startingRotation == null) startingRotation = transform.localRotation.eulerAngles;
                Vector2 delta = input.GetMouseDelta();
                startingRotation.x += delta.x * VerticalSpeed * Time.deltaTime;
                startingRotation.y += delta.y * horizontalSpeed * Time.deltaTime;
                startingRotation.y = Mathf.Clamp(startingRotation.y, -clampAngle, clampAngle);
                state.RawOrientation = Quaternion.Euler(-startingRotation.y,startingRotation.x,0f);

            }
        }       
    }
}

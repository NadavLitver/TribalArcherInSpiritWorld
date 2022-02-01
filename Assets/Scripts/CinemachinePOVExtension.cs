using Cinemachine;
using Sirenix.OdinInspector;
using UnityEngine;

public class CinemachinePOVExtension : CinemachineExtension
{
    private InputManager input;
    private Vector3 startingRotation;
    [SerializeField]
    private float clampAngle = 85;
    //[SerializeField, Tooltip("Look Speed/Sensativity"), ReadOnly]
    //private float horizontalSpeed = 10;
    //[SerializeField, Tooltip("Look Speed/Sensativity"), ReadOnly]
    //private float verticalSpeed = 10;
    [Range(0, 100),SerializeField]
    private float MouseSensitivity;
    [Range(0, 179), ReadOnly]
    public float FOV;
    [Range(0, 179), ReadOnly]
    public float StartingFov;
    private bool firstCameraFrame;
    //[ReadOnly]
    //public float FOVScalingSpeed = 0.0000001f;

    // public float MouseSensitivity { get => MouseSensitivity; set => horizontalSpeed = value; }

    protected override void Awake()
    {
        input = InputManager.Instance;
        base.Awake();
      
        //horizontalSpeed = MouseSensitivity;
        //verticalSpeed = horizontalSpeed;
    }
    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (!Application.isPlaying) return;
        if (!firstCameraFrame)
        {
          FOV = state.Lens.FieldOfView;
          StartingFov = FOV;
          firstCameraFrame = true;
        }
        if (vcam.Follow)
        {
            if (stage == CinemachineCore.Stage.Aim)
            {
                if (startingRotation == null) startingRotation = transform.localRotation.eulerAngles;
                Vector2 delta = input.GetMouseDelta();
                startingRotation.x += delta.x * MouseSensitivity * Time.deltaTime;
                startingRotation.y += delta.y * MouseSensitivity * Time.deltaTime;
                startingRotation.y = Mathf.Clamp(startingRotation.y, -clampAngle, clampAngle);
                state.RawOrientation = Quaternion.Euler(-startingRotation.y, startingRotation.x, 0f);
                if(state.Lens.FieldOfView != FOV)
                 state.Lens = new LensSettings(FOV, 0, 0.3f, 1000, 0);

            }
        }
    }
}

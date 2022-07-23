using Cinemachine;
using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

public class CinemachinePOVExtension : CinemachineExtension
{
    private InputManager input;
    private Vector3 startingRotation;
    [SerializeField]
    private float clampAngle = 85f;
    [Range(0f, 100f), SerializeField]
    private float MouseSensitivity;
    [Range(0f, 179f), ReadOnly]
    public float FOV;
    [Range(0f, 179f), ReadOnly]
    public float StartingFOV;
    private bool firstCameraFrame;
    [SerializeField]
    private float lerpSpeed = 50f;

    [SerializeField] private Camera overlayCam;

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
            StartingFOV = FOV;
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
                if (state.Lens.FieldOfView != FOV)
                {
                    state.Lens = new LensSettings(FOV, 0, 0.3f, 1500, 0);
                    overlayCam.fieldOfView = FOV;
                }

                MouseSensitivity += InputManager.Instance.GetPgUpPgDwnAxis();
                MouseSensitivity = Mathf.Clamp(MouseSensitivity, 0, 100);
            }
        }
    }
    public IEnumerator FOVScalingRoutine(float goal)
    {
        while (FOV != goal)
        {
            FOV = Mathf.MoveTowards(FOV, goal, Time.deltaTime * lerpSpeed);
            yield return null;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class NaturalBowMovement : MonoBehaviour
{
    [FoldoutGroup("References"), SerializeField]
    private BowHandler bowRef;
    [FoldoutGroup("References"), SerializeField]
    private PlayerController playerRef;

    [FoldoutGroup("Noise"), SerializeField]
    private Vector3 noiseMovement;
    [FoldoutGroup("Noise"), SerializeField]
    private Vector3 noiseSpeed;

    [FoldoutGroup("Properties"), ReadOnly, ShowInInspector]
    private Vector3 startRot;
    [FoldoutGroup("Properties"), SerializeField]
    private Vector3 rotClampValues;
    [FoldoutGroup("Properties"), SerializeField]
    private float MouseClampValue;
    [FoldoutGroup("Properties"), SerializeField]
    private float rotLerpMax;
    [FoldoutGroup("Properties"), SerializeField]
    private AnimationCurve rotLerpEase;

    [FoldoutGroup("Properties"), ReadOnly, ShowInInspector]
    private Vector3 startPos;
    [FoldoutGroup("Properties"), SerializeField]
    private Vector3 posClampValues;
    [FoldoutGroup("Properties"), SerializeField]
    private float posLerpMax;
    [FoldoutGroup("Properties"), SerializeField]
    private AnimationCurve posLerpEase;

    [FoldoutGroup("Properties"), SerializeField]
    private float leapRotModifier;
    [FoldoutGroup("Properties"), SerializeField]
    private float leapPosModifier;
    [FoldoutGroup("Properties"), SerializeField]
    private float leapSidePosModifier;
    [FoldoutGroup("Properties"), SerializeField]
    private AnimationCurve LeapDecay;
    [FoldoutGroup("Properties"), SerializeField]
    private float LeapDuration;
    private bool isLeap;


    private void Start()
    {
        startRot = transform.localRotation.eulerAngles;
        startPos = transform.localPosition;
        isLeap = false;
    }
    void Update()
    {
        Vector3 currMoveInput = InputManager.Instance.GetPlayerMovement();
        Vector2 _mouseDelta;
        _mouseDelta = InputManager.Instance.GetMouseDelta();

        float calcMoveDeltaY = playerRef.playerVelocity.y > 0.75f ? playerRef.playerVelocity.y : 0;
        Vector3 _moveDelta;
        _moveDelta = new Vector3(
            currMoveInput.x,
            calcMoveDeltaY, 
            currMoveInput.y); // returns player io velocity

        //calculate lerp ease with updated deltas
        float rotLerpSpeed = rotLerpMax * rotLerpEase.Evaluate(_mouseDelta.normalized.magnitude);
        float posLerpSpeed = posLerpMax * posLerpEase.Evaluate(currMoveInput.magnitude);

        /* Rotation */
        //value seperation
        float xR = startRot.x
            + Mathf.Clamp(_mouseDelta.y, -rotClampValues.x, rotClampValues.x) // + mouse delta y
            - Mathf.Clamp(_moveDelta.y, -posClampValues.y, posClampValues.y); // move delta y
            
        float yR = startRot.y
            + Mathf.Clamp(_mouseDelta.x, -rotClampValues.y, rotClampValues.y) // + mouse delta x
            + Mathf.Clamp(_moveDelta.x, -rotClampValues.x, rotClampValues.x); // move delta x
        float zR = startRot.z
            + Mathf.Clamp(_mouseDelta.x, -rotClampValues.z, rotClampValues.z) // + mouse delta x
            + Mathf.Clamp(_moveDelta.x, -rotClampValues.z, rotClampValues.z); // + move delta x 
        Quaternion targetRot = Quaternion.Euler(xR,yR,zR);
        //implementation
        if (!isLeap)
        {
            transform.localRotation = Quaternion.RotateTowards
                (transform.localRotation, targetRot, rotLerpSpeed * Time.deltaTime);
        }
        
        
        //transform.Rotate(noiseMovement);

        /* Position */

        //value seperation
        float xP = startPos.x
            - Mathf.Clamp(_mouseDelta.x, -posClampValues.x, posClampValues.x) // - mouse delta x
            - Mathf.Clamp(_moveDelta.x, -posClampValues.x, posClampValues.x); // - move delta x
        float yP = startPos.y 
            - Mathf.Clamp(_moveDelta.y, -posClampValues.y, posClampValues.y);// + move delta y
        float zP = startPos.z
            - Mathf.Clamp(_moveDelta.y, -posClampValues.z, posClampValues.z); // - move delta z
            
        Vector3 targetPos = new Vector3(xP, yP, zP);
        //implementation
        
        if (!isLeap)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition ,targetPos,posLerpSpeed * Time.deltaTime);
        }
    }
    public void LeapApplication()
    {
        StartCoroutine(LeapApplicationCoru());
    }
    private IEnumerator LeapApplicationCoru()
    {
        isLeap = true;
        Debug.Log("Leap");
        Vector2 dir = InputManager.Instance.GetPlayerMovement();
        Debug.Log("Dir x: " + dir.x + "; Dir y: " + dir.y);
        Vector3 curPos = transform.localPosition;
        Vector3 curRot = transform.localRotation.eulerAngles;
        float curDur = 0;
        while (curDur < 1)
        {
            //appliedLeapRotModifier = LeapRotDecay.Evaluate()
            curDur += Time.deltaTime / LeapDuration;
            float evaluation = LeapDecay.Evaluate(curDur);
            transform.localPosition = curPos + (Vector3.forward * evaluation * dir.y * leapPosModifier) + Vector3.right * evaluation * dir.x * leapSidePosModifier;
            transform.localRotation = Quaternion.Euler(curRot + (Vector3.forward * evaluation * -dir.x * leapRotModifier));
            yield return new WaitForEndOfFrame();
        }
        curDur = 1;
        isLeap = false;
    }
}

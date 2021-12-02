using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Sirenix.OdinInspector;

public class CameraFovByStretch : MonoBehaviour
{
    [SerializeField]
    private BowHandler bowRef;
    [SerializeField]
    private CinemachineVirtualCamera m_cam;
    [SerializeField]
    private AnimationCurve shootingCurve;
    [SerializeField] private float lerpSpeed;
    
    [ShowInInspector, ReadOnly]
    private float idleFov;
    [SerializeField]
    private float activeFov;
    private void Start()
    {
        idleFov = m_cam.m_Lens.FieldOfView;
    }
    void Update()
    {
        //float targetFov = Mathf.Lerp(idleFov, activeFov, bowRef.shootHoldTime);

        if (bowRef.isShooting)
        {
            m_cam.m_Lens.FieldOfView = Mathf.Lerp(idleFov, activeFov, shootingCurve.Evaluate(bowRef.shootHoldTime)) ;
        }
        else
        {
            m_cam.m_Lens.FieldOfView = Mathf.MoveTowards(m_cam.m_Lens.FieldOfView, idleFov, Time.deltaTime * lerpSpeed);
        }


    }
}

using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    [SerializeField] private LineRenderer[] m_lines;
    [SerializeField] private GameObject m_charge;
    [SerializeField] private GameObject m_beamEnd;
    public Vector3 shootDirection;
    [SerializeField] private float chargeBeamDuration;
    [SerializeField] private float beamStretchSpeed = 50f;
    [SerializeField] private float beamDuration = 4f;
    [ReadOnly] private float lineLength;
    [SerializeField] private float maxLineLength = 150f;
    [SerializeField] private float closeBeamDuration = 0.25f;
    bool doCalculateLength = false;
    [SerializeField] private LayerMask lm;
    [SerializeField] private Color startColor;
    [SerializeField] private Color endColor;
    [SerializeField] private AnimationCurve colorEase;
    public bool isAttacking { get; private set; }
    [SerializeField] private Transform endPos;
    [SerializeField] private float endPosYOffset;
    private Vector3 hitPos;
    [Button]
    private void OnEnable()
    {
        endPos.localPosition = Vector3.forward * maxLineLength + Vector3.up * endPosYOffset;
    }
    public void Attack()
    {
        if (isAttacking)
        {
            return;
        }
        StartCoroutine(AttackRoutine());
    }
    private void FixedUpdate()
    {
        CalculateLength();
    }
    private void CalculateLength()
    {
        if (doCalculateLength)
        {
            if (Physics.Linecast(transform.position, endPos.position ,out RaycastHit _hit, lm))
            {
                hitPos = _hit.point;
                lineLength = _hit.distance;
                Debug.Log("hit name: " + _hit.transform.name);
            }
            else
            {
                lineLength = maxLineLength;
                hitPos = transform.forward * maxLineLength;
            }

        }
    }
    private void ColorOverLife(float curDur)
    {

        foreach (LineRenderer line in m_lines)
        {
            line.startColor = Color.Lerp(startColor, endColor, colorEase.Evaluate(curDur));
            line.endColor = Color.Lerp(startColor, endColor, colorEase.Evaluate(curDur));
        }
    }
    private void BeamEnd(Vector3 pos)
    {
        m_beamEnd.transform.position = pos;
    }
    private void SetLineEndPositionsToTargetOverTime(Vector3 target)
    {
        foreach (LineRenderer line in m_lines)
        {
            line.SetPosition(1, Vector3.MoveTowards(line.GetPosition(1), target, beamStretchSpeed * Time.deltaTime));
        }
    }
    private void SetLineEndPositionsToTargetOverRatio(float curDur, Vector3 target)
    {
        foreach (LineRenderer line in m_lines)
        {
            line.SetPosition(1, Vector3.Lerp(line.GetPosition(1), target, curDur));
        }
    }
    public IEnumerator AttackRoutine()
    {
        isAttacking = true;
        doCalculateLength = true;
        m_charge.SetActive(true);
        foreach (LineRenderer line in m_lines)
        {
            line.gameObject.SetActive(true);
            line.SetPosition(0, Vector3.zero);
            line.SetPosition(1, Vector3.zero);
        }
        yield return new WaitForSeconds(chargeBeamDuration);
        m_beamEnd.SetActive(true);

        float curDur = 0;
        while (curDur < 1)
        {
            curDur += Time.deltaTime / beamDuration;
            BeamEnd(hitPos);
            ColorOverLife(curDur);
            SetLineEndPositionsToTargetOverTime(lineLength * Vector3.forward);
            yield return null;
        }
        doCalculateLength = false;
        //closing beam
        BeamEnd(Vector3.zero);
        m_beamEnd.SetActive(false);
        while (curDur < 1)
        {
            curDur += Time.deltaTime / closeBeamDuration;
            SetLineEndPositionsToTargetOverRatio(curDur ,Vector3.zero);
            yield return null;
        }
        m_charge.SetActive(false);
        foreach (LineRenderer line in m_lines)
        {
            line.SetPosition(0, Vector3.zero);
            line.SetPosition(1, Vector3.zero);
            line.gameObject.SetActive(false);
        }
        isAttacking = false; 
    }
}

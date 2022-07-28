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
    Vector3 targetPos => shootDirection * lineLength;
    [Button]
    public void Attack()
    {
        StartCoroutine(AttackRoutine());
    }
    private void FixedUpdate()
    {
        if (doCalculateLength)
        {
            Ray ray = new Ray(transform.position, shootDirection);
            if (Physics.Raycast(ray, out RaycastHit hit,maxLineLength,lm))
            {
                lineLength = hit.distance;
            }
            else
            {
                lineLength = maxLineLength;
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
    private void BeamEnd(Vector3 target)
    {
        m_beamEnd.transform.localPosition = target;
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
        doCalculateLength = true;
        m_charge.SetActive(true);
        foreach (LineRenderer line in m_lines)
        {
            line.gameObject.SetActive(true);
            line.SetPosition(0, Vector3.zero);
            line.SetPosition(1, Vector3.zero);
        }
        yield return new WaitForSeconds(chargeBeamDuration);
        float curDur = 0;
        m_beamEnd.SetActive(true);
        while (curDur < 1)
        {
            curDur += Time.deltaTime / beamDuration;
            
            ColorOverLife(curDur);
            SetLineEndPositionsToTargetOverTime(lineLength * Vector3.forward);
            BeamEnd(lineLength * Vector3.forward);
            yield return null;
        }

        doCalculateLength = false;
        //closing beam

        while (curDur < 1)
        {
            curDur += Time.deltaTime / closeBeamDuration;

            SetLineEndPositionsToTargetOverRatio(curDur ,Vector3.zero);
            BeamEnd(m_lines[0].GetPosition(1));
            yield return null;
        }
        m_beamEnd.SetActive(false);
        foreach (LineRenderer line in m_lines)
        {
            line.gameObject.SetActive(false);
            line.SetPosition(0, Vector3.zero);
            line.SetPosition(1, Vector3.zero);
        }
    }
}

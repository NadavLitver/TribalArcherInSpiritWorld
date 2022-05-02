using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    [SerializeField] private LineRenderer[] m_lines;
    [SerializeField] private GameObject[] m_particles;
    [SerializeField] private GameObject m_charge;
    [SerializeField] private GameObject m_beamEnd;
    [SerializeField] private float chargeTime;
    [SerializeField] private float beamStretchDuration;
    [SerializeField] private float lineLength;
    [SerializeField] private float beamDuration;
    public void Attack()
    {
        StartCoroutine(AttackRoutine());
    }
    public IEnumerator AttackRoutine()
    {
        foreach (LineRenderer line in m_lines)
        {
            line.SetPosition(0, Vector3.zero);
            line.SetPosition(1, Vector3.zero);
        }
        m_charge.SetActive(true);
        yield return new WaitForSeconds(chargeTime);
        float curDur = 0;
        m_beamEnd.SetActive(true);
        foreach (GameObject go in m_particles)
        {
            go.SetActive(true);
        }
        while (curDur < 1)
        {
            foreach (LineRenderer line in m_lines)
            {
                line.SetPosition(1, Vector3.up * Mathf.Lerp(0, lineLength, curDur));
            }
            m_beamEnd.transform.position = Vector3.up * Mathf.Lerp(0, lineLength, curDur);
            curDur += Time.deltaTime / beamStretchDuration;
            yield return new WaitForEndOfFrame();
        }
        curDur = 1;
        foreach (LineRenderer line in m_lines)
        {
            line.SetPosition(1, Vector3.up * Mathf.Lerp(0, lineLength, curDur));
        }
        m_beamEnd.transform.position = Vector3.up * Mathf.Lerp(0, lineLength, curDur);

        yield return new WaitForSeconds(beamDuration);
        while (curDur > 0)
        {
            foreach (LineRenderer line in m_lines)
            {
                line.SetPosition(1, Vector3.up * Mathf.Lerp(0, lineLength, curDur));
            }
            m_beamEnd.transform.position = Vector3.up * Mathf.Lerp(0, lineLength, curDur);
            curDur -= Time.deltaTime / beamStretchDuration;
            yield return new WaitForEndOfFrame();
        }


    }
}

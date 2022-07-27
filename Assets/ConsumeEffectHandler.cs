using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumeEffectHandler : MonoBehaviour
{
    [SerializeField] private Transform relic;
    [SerializeField] private ParticleSystem m_sparks;
    [SerializeField] private float distance = 5f;
    void Update()
    {
        m_sparks.transform.position = Camera.main.transform.position - Camera.main.transform.forward * distance;
        Vector3 relativeRelicPos;
        relativeRelicPos = relic.position - m_sparks.transform.position;
        ParticleSystem.ShapeModule _editableShape = m_sparks.shape;
        _editableShape.position = relativeRelicPos;
    }
}

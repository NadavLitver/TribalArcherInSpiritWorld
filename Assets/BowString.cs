using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowString : MonoBehaviour
{
    [SerializeField]  private LineRenderer m_renderer;
    [SerializeField] private ParticleSystem m_ReleaseVFX;

    [SerializeField, ReadOnly] private Vector3 targetPosition;
    public void SetBowStringPos(float lerpT)
    {
        targetPosition = new Vector3(Mathf.Lerp(0f, 0.35f, lerpT),0 , 0f);
        m_renderer.SetPosition(1, targetPosition);
    }
    [Button]
    public void setStringBack() =>  m_renderer.SetPosition(1, new Vector3(0.35f,0f,0f));
    public void ResetBowStringPos()
    {
        targetPosition = Vector3.zero;
        m_renderer.SetPosition(1, targetPosition);
    }
    public void PlayStringVFX ()=> m_ReleaseVFX.Play();
}

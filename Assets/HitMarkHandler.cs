using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitMarkHandler : MonoBehaviour
{
    public static HitMarkHandler instance;
    [FoldoutGroup("Refrences"),SerializeField]
    private Animator m_animator;
    private void Awake()
    {
       if(instance != this || instance == null)
        {
            instance = this;
        }
    }
    public void PlayNormalHitMark()
    {
        m_animator.SetTrigger("BodyShot");
    }
    public void PlayHeadShotHitMark()
    {
        m_animator.SetTrigger("HeadShot");

    }
}

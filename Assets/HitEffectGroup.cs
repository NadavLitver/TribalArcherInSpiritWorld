using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffectGroup : MonoBehaviour
{
    public List<HitEffectHandler> m_hitEffects;
    void OnEnable()
    {
        m_hitEffects.AddRange(GetComponentsInChildren<HitEffectHandler>());
        if (m_hitEffects.Count == 0)
        {
            Debug.Log("no hit effects found");
            this.enabled = false;
        }
    }
    public void Hit()
    {
        foreach (HitEffectHandler item in m_hitEffects)
        {
            item.Hit();
        }
    }
}

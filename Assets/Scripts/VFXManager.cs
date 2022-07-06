using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    private static VFXManager _instance;
    public VFXData[] VFXArray;
    public enum Effect
    {
        EnemyHit,
        HeadshotEffect,
        TerrainHitEffect,
        FlyingEnemyDead,
        FloatingNumber
    }
    public static VFXManager instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }


    }
    public static void Play(Effect effect)
    {
        Instantiate(GetVFXGameObject(effect));
    }
    public static void Play(Effect effect, Transform _tranform)
    {
        GameObject currenteffect = Instantiate(GetVFXGameObject(effect), _tranform.position, Quaternion.identity, _tranform);
    }
    public static void Play(Effect effect, Transform _tranform, Vector3 worldPos)
    {
        GameObject currenteffect = Instantiate(GetVFXGameObject(effect), worldPos, Quaternion.identity, _tranform);
    }
    public static void Play(Effect effect, Transform _tranform, Vector3 worldPos, Quaternion rotation)
    {
        GameObject currenteffect = Instantiate(GetVFXGameObject(effect), worldPos, rotation, _tranform);
    }
    public static void Play(Effect effect, Vector3 worldPos)
    {
        GameObject currenteffect = Instantiate(GetVFXGameObject(effect));
        currenteffect.transform.position = worldPos;
    }
    public static void Play(Effect effect, Vector3 worldPos, Quaternion rotation)
    {
        GameObject currenteffect = Instantiate(GetVFXGameObject(effect));
        currenteffect.transform.position = worldPos;
        currenteffect.transform.rotation = rotation;
    }
    public static void PlayRepeat(Effect effect, Vector3 worldPos, int amountToRepeat, float timeIntervals)
    {

        _instance.StartCoroutine(RepeatCouroutine(effect, worldPos, amountToRepeat, timeIntervals));
    }
    public static void PlayFloatingNumber(Vector3 worldPos, int num, float heightMod)
    {
        GameObject currenteffect = Instantiate(GetVFXGameObject(Effect.FloatingNumber), worldPos, Quaternion.identity, null);
        currenteffect.GetComponent<FloatingNumberHandler>().Float(num, heightMod);
    }
    private static IEnumerator RepeatCouroutine(Effect effect, Vector3 worldPos, int amountToRepeat, float timeIntervals)
    {
        for (int i = 0; i < amountToRepeat; i++)
        {
            yield return new WaitForSeconds(timeIntervals);
            GameObject currenteffect = Instantiate(GetVFXGameObject(effect));
            currenteffect.transform.position = worldPos;
        }
    }
    private static GameObject GetVFXGameObject(Effect effect)
    {
        foreach (VFXData vfx in instance.VFXArray)
        {
            if (vfx.m_effect == effect)
            {
                return vfx.m_prefab;
            }
        }
       
        return null;
    }
}
[Serializable]
public class VFXData
{
    public VFXManager.Effect m_effect;
    public GameObject m_prefab;
}
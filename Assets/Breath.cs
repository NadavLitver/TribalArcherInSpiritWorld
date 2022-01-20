using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Breath : MonoBehaviour
{
    [SerializeField] public float maxBreath = 100;
    [SerializeField] public float current = 100; // running breath
    [SerializeField] public float gain; // per second
    [SerializeField] public float weakerGain; // per second
    [SerializeField] public float ReBreathThreshHold = 0.33f; // precent between 0-1
    [SerializeField] public bool isOutOfBreath = false;
    [SerializeField] public bool doGain = false;
    [SerializeField] public float disableDuration = 1.5f;


    public UnityEvent OnEnterOutOfBreath;
    public UnityEvent OnExitOutOfBreath;

    [SerializeField] public float timeSinceLastBreath;
    private void OnEnable()
    {
        current = maxBreath;
        isOutOfBreath = false;
    }
    void Update()
    {
        if (isOutOfBreath && current >= ReBreathThreshHold)
        {
            ExitOutOfBreath();
        }
        timeSinceLastBreath += Time.deltaTime;
        if (timeSinceLastBreath > disableDuration)
        {
            GainBreath();
        }
    }
    public void LoseBreath(float breathLost)
    {
        current -= breathLost;
        timeSinceLastBreath = 0;
        if (current < 0)
        {
                current = 0.01f;
            if (!isOutOfBreath)
            {
                EnterOutOfBreath();
            }
        }
        
    }
    public void GainBreath()
    {
        if (current < maxBreath)
        {
            Debug.Log("cur: " + (current < maxBreath) + "; IsOOB: " + isOutOfBreath);
            if (isOutOfBreath)
            {
                current += weakerGain * Time.deltaTime; // out of breath gain
            }
            else
            {
                current += gain * Time.deltaTime; // regular gain
            }
        }
        if (current >= maxBreath)
        {
            current = maxBreath; // clamp val
        }
    }
    private void EnterOutOfBreath()
    {
        isOutOfBreath = true;
        OnEnterOutOfBreath?.Invoke();
    }
    private void ExitOutOfBreath()
    {
        isOutOfBreath = false;
        OnExitOutOfBreath?.Invoke();
    }
}

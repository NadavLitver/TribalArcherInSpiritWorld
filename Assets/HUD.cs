using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [FoldoutGroup("Refrences"), SerializeField]
    private PlayerLivebody playerBodyRef;
    [FoldoutGroup("Refrences"), SerializeField]
    private Slider healthBar;
    [FoldoutGroup("Refrences"), SerializeField]
    private Slider staminaBar;
    [FoldoutGroup("Refrences"), SerializeField]
    private Button[] PowerPoints;
    [FoldoutGroup("Refrences"), SerializeField]
    private TextMeshProUGUI counter;
    [FoldoutGroup("Refrences"), SerializeField]
    private Slider powerBuffer;
    [FoldoutGroup("Refrences"), SerializeField]
    private PowersHandler powersHandlerRef;

    [FoldoutGroup("Parameters"), SerializeField]
    private float healthUpdateSpeed;
    [FoldoutGroup("Parameters"), SerializeField]
    private float powerDecay;

    private void LateUpdate()
    {
        healthBar.value = Mathf.MoveTowards(healthBar.value, playerBodyRef.health / playerBodyRef.maxHealth, Time.deltaTime * healthUpdateSpeed);
        powerBuffer.value -= powerDecay * Time.deltaTime; 
    }
    public void PowerBufferOnUpdate()
    {
        if (powerBuffer.value >= 1)
        {
            powerBuffer.value = 0;
            powersHandlerRef.AddPower();
            counter.text = powersHandlerRef.CurrPower.ToString();
            updatePowerPoints();
        }
    }
    private void updatePowerPoints()
    {
        for (int i = 0; i < PowerPoints.Length; i++)
        {
            if (powersHandlerRef.CurrPower >= i)
            {
                PowerPoints[i].interactable = true;
            }
            else
            {
                PowerPoints[i].interactable = false;
            }
        }
    }
}

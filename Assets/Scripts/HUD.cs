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
    private Breath breathRef;
    [FoldoutGroup("Refrences"), SerializeField]
    private Slider healthBar;
    [FoldoutGroup("Refrences"), SerializeField]
    private Slider breathBar;
    [FoldoutGroup("Refrences"), SerializeField]
    private Button[] PowerPoints;
    [FoldoutGroup("Refrences"), SerializeField]
    private TextMeshProUGUI counter;
    [FoldoutGroup("Refrences"), SerializeField]
    private Slider powerBuffer;
    [FoldoutGroup("Refrences"), SerializeField]
    private PowersHandler powersHandlerRef;

    [FoldoutGroup("Parameters"), SerializeField]
    private float slidersFillSpeed;
    [FoldoutGroup("Parameters"), SerializeField]
    private float powerDecay;
    private void Start()
    {
        healthBar.maxValue = playerBodyRef.maxHealth;
        breathBar.maxValue = breathRef.maxBreath;
    }

    private void LateUpdate()
    {
        UpdateHealth();
        UpdateBreath();
        powerBuffer.value -= powerDecay * Time.deltaTime;
    }

    private void UpdateHealth()
    {
        healthBar.value = Mathf.MoveTowards(healthBar.value, playerBodyRef.health, slidersFillSpeed * Time.deltaTime);
    }
    private void UpdateBreath()
    {
        breathBar.value = Mathf.MoveTowards(breathBar.value, breathRef.current, slidersFillSpeed * Time.deltaTime);
    }
  
   
}

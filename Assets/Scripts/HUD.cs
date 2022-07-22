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
    [FoldoutGroup("Parameters"), SerializeField]
    private float slidersFillSpeed;
    [FoldoutGroup("Parameters"), SerializeField]
    private float powerDecay;

    [FoldoutGroup("Icons"), SerializeField]
    private Image scatterIcon;
    [FoldoutGroup("Icons"), SerializeField]
    private Image scatterCover;
    [FoldoutGroup("Icons"), SerializeField]
    private Image boltIcon;
    [FoldoutGroup("Icons"), SerializeField]
    private Image boltCover;
    [FoldoutGroup("Icons"), SerializeField]
    private Image strikeIcon;
    [FoldoutGroup("Icons"), SerializeField]
    private Image strikeCover;
    [FoldoutGroup("Icons"), SerializeField]
    private Color idleColor;
    [FoldoutGroup("Icons"), SerializeField]
    private Color activeColor;

    private int index = 0;

    private void Start()
    {
        healthBar.maxValue = playerBodyRef.maxHealth;
        breathBar.maxValue = breathRef.maxBreath;
    }
    public void ToggleScatter()
    {
        if (index == 1)
        {
            scatterCover.color = activeColor;
            scatterIcon.color = idleColor;
            index = 0;
        }
        else
        {
            scatterCover.color = idleColor;
            scatterIcon.color = activeColor;
            index = 1;

        }


        boltCover.color = activeColor;
        boltIcon.color = idleColor;
        strikeCover.color = activeColor;
        strikeIcon.color = idleColor;
    }
    public void ToggleBolt()
    {
        if (index == 2)
        {
            boltCover.color = activeColor;
            boltIcon.color = idleColor;
            index = 0;
        }
        else
        {
            boltCover.color = idleColor;
            boltIcon.color = activeColor;
            index = 2;
        }

        scatterCover.color = activeColor;
        scatterIcon.color = idleColor;
        strikeCover.color = activeColor;
        strikeIcon.color = idleColor;
    }
    public void ToggleStrike()
    {
        if (index == 3)
        {
            strikeCover.color = activeColor;
            strikeIcon.color = idleColor;
            index = 0;
        }
        else
        {
            strikeCover.color = idleColor;
            strikeIcon.color = activeColor;
            index = 3;
        }

        boltCover.color = activeColor;
        boltIcon.color = idleColor;
        scatterCover.color = activeColor;
        scatterIcon.color = idleColor;
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

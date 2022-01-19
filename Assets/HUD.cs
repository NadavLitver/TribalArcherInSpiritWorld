using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    private Slider[] PowerBars;
    public int currPowerBar { private get; set; }

    void Start()
    {
        currPowerBar = 0;
    }
    private void LateUpdate()
    {
        healthBar.value = playerBodyRef.health / playerBodyRef.maxHealth;
        for (int i = 0; i < PowerBars.Length; i++)
        {
            if (currPowerBar == i)
            {
                //PowerBars[i].value = currVal
            }
            else if (currPowerBar < i)
            {
                PowerBars[i].value = 0;
            }
            else if (currPowerBar > i)
            {
                PowerBars[i].value = 1;
            }
        }
    }
}

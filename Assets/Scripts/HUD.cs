using Sirenix.OdinInspector;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HUD : MonoBehaviour
{
    public static HUD instance;

    [FoldoutGroup("Refrences"), SerializeField]
    public PlayerLivebody playerBodyRef;
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
    private Image scatterCover;
    [FoldoutGroup("Icons"), SerializeField]
    private Image scatterIcon;
    [FoldoutGroup("Icons"), SerializeField]
    private Image boltCover;
    [FoldoutGroup("Icons"), SerializeField]
    private Image boltIcon;
    [FoldoutGroup("Icons"), SerializeField]
    private Image strikeCover;
    [FoldoutGroup("Icons"), SerializeField]
    private Image strikeIcon;
    [FoldoutGroup("Icons"), SerializeField]
    private Color idleColor;
    [FoldoutGroup("Icons"), SerializeField]
    private Color activeColor;
    [FoldoutGroup("Icons"), SerializeField]
    private Color idleIcon;
    [FoldoutGroup("Icons"), SerializeField]
    private Color activeIcon;

    [FoldoutGroup("Icons"), SerializeField]
    private float selectDuration = 0.25f;
    [FoldoutGroup("Icons"), SerializeField]
    private float deselectDuration = 0.25f;

    public HealthPower healthPower;
    private int index = 0;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        healthBar.maxValue = playerBodyRef.maxHealth;
        breathBar.maxValue = breathRef.maxBreath;
    }
    public void OnRelease()
    {
        switch (index)
        {
            case 1:
                scatterCover.color = activeColor;
                scatterIcon.color = idleIcon;
                index = 0;
                break;
            case 2:
                boltCover.color = activeColor;
                boltIcon.color = idleIcon;
                index = 0;
                break;
            case 3:
                strikeCover.color = activeColor;
                strikeIcon.color = idleColor;
                index = 0;
                break;
            default: Debug.Log("just a regular arrow");
                break;
        }
    }
    public void ToggleScatter()
    {
        Ability ability = AbilityStackHandler.instance.GetAbility(AbilityEnum.Scatter); // get ability ref
        if (!ability.gameObject.activeInHierarchy || AbilityStackHandler.instance.currentStackAmount < ability.stackCost)
        {
            return;
        }
        if (index == 1)
        {
            scatterCover.color = activeColor;
            scatterIcon.color = idleIcon;
            index = 0;
        }
        else
        {
            scatterCover.color = idleColor;
            scatterIcon.color = activeIcon;
            index = 1;
        }
        if (AbilityStackHandler.instance.GetAbility(AbilityEnum.QuickShot).gameObject.activeInHierarchy)
        {
            boltCover.color = activeColor;
            boltIcon.color = idleIcon;
        }
        if (AbilityStackHandler.instance.GetAbility(AbilityEnum.LightingStrike).gameObject.activeInHierarchy)
        {
            strikeCover.color = activeColor;
            strikeIcon.color = idleIcon;
        }
        AbilityStackHandler.instance.UsingStacksEffect(index);
    }
    public void ToggleBolt()
    {
        Ability ability = AbilityStackHandler.instance.GetAbility(AbilityEnum.QuickShot);
        if (!ability.gameObject.activeInHierarchy || AbilityStackHandler.instance.currentStackAmount < ability.stackCost)
        {
            return;
        }
        if (index == 2)
        {
            boltCover.color = activeColor;
            boltIcon.color = idleIcon;
            index = 0;
        }
        else
        {
            boltCover.color = idleColor;
            boltIcon.color = activeIcon;
            index = 2;
        }
        if (AbilityStackHandler.instance.GetAbility(AbilityEnum.Scatter).gameObject.activeInHierarchy)
        {
            scatterCover.color = activeColor;
            scatterIcon.color = idleIcon;
        }
        if (AbilityStackHandler.instance.GetAbility(AbilityEnum.LightingStrike).gameObject.activeInHierarchy)
        {
            strikeCover.color = activeColor;
            strikeIcon.color = idleIcon;
        }
        AbilityStackHandler.instance.UsingStacksEffect(index);
    }
    public void ToggleStrike()
    {
        Ability ability = AbilityStackHandler.instance.GetAbility(AbilityEnum.LightingStrike);
        if (!ability.gameObject.activeInHierarchy || AbilityStackHandler.instance.currentStackAmount < ability.stackCost)
        {
            return;
        }
        if (index == 3)
        {
            strikeCover.color = activeColor;
            strikeIcon.color = idleColor;
            index = 0;
        }
        else
        {
            strikeCover.color = idleColor;
            strikeIcon.color = activeIcon;
            index = 3;
        }
        if (AbilityStackHandler.instance.GetAbility(AbilityEnum.Scatter).gameObject.activeInHierarchy)
        {
            scatterCover.color = activeColor;
            scatterIcon.color = idleIcon;
        }
        if (AbilityStackHandler.instance.GetAbility(AbilityEnum.QuickShot).gameObject.activeInHierarchy)
        {
            boltCover.color = activeColor;
            boltIcon.color = idleIcon;
        }
        AbilityStackHandler.instance.UsingStacksEffect(index);
    }
    private IEnumerator SelectAbility(AbilityEnum ability)
    {
        bool hasScatter = AbilityStackHandler.instance.GetAbility(AbilityEnum.Scatter).gameObject.activeInHierarchy;
        bool hasBolt = AbilityStackHandler.instance.GetAbility(AbilityEnum.QuickShot).gameObject.activeInHierarchy;
        bool hasStrike = AbilityStackHandler.instance.GetAbility(AbilityEnum.LightingStrike).gameObject.activeInHierarchy;
        Debug.Log("scatter: " + hasScatter + "; bolt: " + hasBolt + "; strike: " + hasStrike);
        float curDur = 0;
        while (curDur < 1)
        {
            curDur += Time.deltaTime / selectDuration;
            switch (ability)
            {
                case AbilityEnum.Scatter:
                    scatterCover.color = Color.Lerp(activeColor, idleColor, curDur);
                    if (hasBolt)
                    {
                        boltCover.color = Color.Lerp(idleColor, activeColor, curDur);
                    }
                    if (hasStrike)
                    {
                        strikeCover.color = Color.Lerp(idleColor, activeColor, curDur);
                    }
                    break;
                case AbilityEnum.QuickShot:
                    boltCover.color = Color.Lerp(activeColor, idleColor, curDur);
                    if (hasScatter)
                    {
                        scatterCover.color = Color.Lerp(idleColor, activeColor, curDur);
                    }
                    if (hasStrike)
                    {
                        strikeCover.color = Color.Lerp(idleColor, activeColor, curDur);
                    }
                    break;
                case AbilityEnum.LightingStrike:
                    strikeCover.color = Color.Lerp(activeColor, idleColor, curDur);
                    if (hasScatter)
                    {
                        scatterCover.color = Color.Lerp(idleColor, activeColor, curDur);
                    }
                    {
                        boltCover.color = Color.Lerp(idleColor, activeColor, curDur);
                    }
                    break;
                default:
                    break;
            }
            yield return null;
        }
    }
    private IEnumerator DeselectAbility(AbilityEnum ability)
    {
        float curDur = 0;
        while (curDur < 1)
        {
            curDur += Time.deltaTime / deselectDuration;
            switch (ability)
            {
                case AbilityEnum.Scatter: scatterCover.color = Color.Lerp(idleColor, activeColor, curDur);
                    break;
                case AbilityEnum.QuickShot: boltCover.color = Color.Lerp(idleColor, activeColor, curDur);
                    break;
                case AbilityEnum.LightingStrike: strikeCover.color = Color.Lerp(idleColor, activeColor, curDur);
                    break;
                default:
                    break;
            }
            yield return null;
        }
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

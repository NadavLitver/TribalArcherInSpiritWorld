using Sirenix.OdinInspector;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public enum AbilityEnum
{
    Scatter,
    QuickShot,
    LightingStrike,
}
public class AbilityStackHandler : MonoBehaviour
{
    public static AbilityStackHandler instance;
    [FoldoutGroup("Properties"), ShowInInspector, ReadOnly]
    const int MAX_STACKS = 3;
    [FoldoutGroup("Properties"), ReadOnly]
    public int currentStackAmount;
    [FoldoutGroup("Events")]
    public UnityEvent onStackChange;
    [FoldoutGroup("Refrences")]
    public Button[] UIStackObjects;
    [FoldoutGroup("Refrences")]
    public GameObject[] StackEffects;
    private Button[] UIEffectButtons;
    private ParticleSystem[] DeathParticles;
    [FoldoutGroup("Refrences")]
    public ParticleSystem[] SpawnParticles;
    [FoldoutGroup("Refrences")]
    public Slider PowerBar;
    [FoldoutGroup("Refrences"), ReadOnly, SerializeField]
    private float PowerBarMaxValue = 100;
    [FoldoutGroup("Refrences"), ReadOnly, SerializeField]
    private float PowerBarConstantDecreaseSpeed = 100;
    [FoldoutGroup("Refrences"), ReadOnly, SerializeField]
    private float PowerBarFillSpeed = 5f;
    
    private bool canPowerBarChange = false;
    [FoldoutGroup("Properties"), SerializeField]
    private float BufferCombatDelay = 2;
    private float powerBarTarget;

    [FoldoutGroup("Refrences"), SerializeField]
    private TextMeshProUGUI addedStackValueText;
    [FoldoutGroup("Refrences")]
    public PlayerLivebody playerBody;
    [SerializeField] Ability[] abilities;
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;

        }
        else
        {
            Debug.LogError("singelton instance populated");
        }

        currentStackAmount = MAX_STACKS;
        PowerBar.maxValue = PowerBarMaxValue;
        playerBody.OnDeath.AddListener(ResetStacks);

    }
    private void Start()
    {
        UIEffectButtons = new Button[StackEffects.Length];
        DeathParticles = new ParticleSystem[StackEffects.Length];
        for (int i = 0; i < UIEffectButtons.Length; i++)
        {
            UIEffectButtons[i] = StackEffects[i].GetComponent<Button>();
            DeathParticles[i] = StackEffects[i].GetComponentInChildren<ParticleSystem>();
        }
        UpdateUIElements();
    }
    private IEnumerator changePowerBarValue()
    {
        while (PowerBar.value != powerBarTarget)
        {
            PowerBar.value = Mathf.MoveTowards(PowerBar.value, powerBarTarget, PowerBarFillSpeed * Time.deltaTime);
            yield return null;
        }
    }
    public void IncreaseBufferValue(float _amountToIncrease)
    {
        PowerBar.value += _amountToIncrease;
        addedStackValueText.text = "+" + " " + _amountToIncrease;
        addedStackValueText.color = new Color(1, 1, 1, 1);
        if (PowerBar.value >= PowerBar.maxValue)
        {
            IncreaseStackCount();
        }
        StopAllCoroutines();
        StartCoroutine(CanBufferRoutine());
    }
    private void LateUpdate()
    {
        ConstantlyDecreaseBuffer();
        DecreasePopUpTextAlpha();
    }

    private void DecreasePopUpTextAlpha()
    {
        if (addedStackValueText.color.a != 0)
            addedStackValueText.color = new Color(1, 1, 1, Mathf.MoveTowards(addedStackValueText.color.a, 0, Time.deltaTime));
    }

    private void ConstantlyDecreaseBuffer()
    {

        if (canPowerBarChange && PowerBar.value > 0)
            PowerBar.value -= PowerBarConstantDecreaseSpeed * Time.deltaTime;
    }
    private void IncreaseStackCount()
    {
        currentStackAmount++;
        if (currentStackAmount > MAX_STACKS)
            currentStackAmount = MAX_STACKS;
        PowerBar.value = 0;
        onStackChange?.Invoke();
        UpdateUIElements();

    }
    [Button]
    private void IncreaseStackCount(int _amountToIncrease)
    {
        currentStackAmount += _amountToIncrease;
        if (currentStackAmount > MAX_STACKS)
            currentStackAmount = MAX_STACKS;

        onStackChange?.Invoke();
        UpdateUIElements();

    }
    private void ResetStacks()
    {

        currentStackAmount = MAX_STACKS;

        onStackChange?.Invoke();
        UpdateUIElements();

    }
    public void DecreaseStackCount()
    {
        currentStackAmount--;
        if (currentStackAmount < 0)
            currentStackAmount = 0;

        onStackChange?.Invoke();
        UpdateUIElements();

    }
    public void DecreaseStackCount(int _amountToDecrease)
    {
        currentStackAmount -= _amountToDecrease;
        if (currentStackAmount < 0)
            currentStackAmount = 0;
        onStackChange?.Invoke();
        UpdateUIElements();

    }
    public void UsingStacksEffect(int count)
    {
        for (int i = 0; i < UIEffectButtons.Length; i++)
        {
            if (UIEffectButtons[i].interactable)
            {
                UIEffectButtons[i].interactable = false;
            }
        }
        int counter;
        counter = count;
        for (int i = currentStackAmount - 1; i > -1; i--)
        {
            if (counter == 0)
            {
                i = 0;
            }
            else
            {
                UIEffectButtons[i].interactable = true;
            }
            counter--;
        }
        Debug.Log("activated " + count + " button effects");
    }
    public void UpdateUIElements()
    {
        for (int i = 0; i < UIStackObjects.Length; i++)
        {
            if (i + 1 > currentStackAmount)
            {
                if (UIEffectButtons[i].interactable)
                {
                    DeathParticles[i].Play();
                    UIEffectButtons[i].interactable = false;
                }
                UIStackObjects[i].interactable = false;
            }
            else
            {
                UIEffectButtons[i].interactable = false;
                UIStackObjects[i].interactable = true;
                SpawnParticles[i].Play();
            }
        }
    }
    IEnumerator CanBufferRoutine()
    {

        canPowerBarChange = false;
        yield return new WaitForSeconds(BufferCombatDelay);
        canPowerBarChange = true;
    }
    public Ability GetAbility(AbilityEnum ability)
    {
        switch (ability)
        {
            case AbilityEnum.Scatter:
                return abilities[0];
            case AbilityEnum.QuickShot:
                return abilities[1];
            case AbilityEnum.LightingStrike:
                return abilities[2];

        }
        Debug.Log("No ability Found");
        return abilities[0];
    }
}

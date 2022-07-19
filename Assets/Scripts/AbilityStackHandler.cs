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
    public Slider Buffer;
    [FoldoutGroup("Refrences"), ReadOnly, SerializeField]
    private float BufferMaxValue = 100;
    [FoldoutGroup("Refrences"), ReadOnly, SerializeField]
    private float BufferConstantDecreaseSpeed = 100;
    private bool canBufferChange = false;
    [FoldoutGroup("Properties"), SerializeField]
    private float BufferCombatDelay = 2;
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
        Buffer.maxValue = BufferMaxValue;
        UpdateUIElements();
        playerBody.OnDeath.AddListener(ResetStacks);
        
    }
    public void IncreaseBufferValue(float _amountToIncrease)
    {
        Buffer.value += _amountToIncrease;
        addedStackValueText.text = "+" + " " + _amountToIncrease;
        addedStackValueText.color = new Color(1, 1, 1, 1);
        if (Buffer.value >= Buffer.maxValue)
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
        
        if (canBufferChange && Buffer.value > 0)
            Buffer.value -= BufferConstantDecreaseSpeed * Time.deltaTime;
    }
    private void IncreaseStackCount()
    {
        currentStackAmount++;
        if (currentStackAmount > MAX_STACKS)
            currentStackAmount = MAX_STACKS;
        Buffer.value = 0;
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
    public void UpdateUIElements()
    {
        for (int i = 0; i < UIStackObjects.Length; i++)
        {
            if (i + 1 > currentStackAmount)
            {
                UIStackObjects[i].interactable = false;
            }
            else
            {
                UIStackObjects[i].interactable = true;

            }
        }
    }
    IEnumerator CanBufferRoutine()
    {

        canBufferChange = false;
        yield return new WaitForSeconds(BufferCombatDelay);
        canBufferChange = true;
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

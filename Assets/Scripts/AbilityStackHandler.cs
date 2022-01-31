using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AbilityStackHandler : MonoBehaviour
{
    [FoldoutGroup("Properties"), ShowInInspector, ReadOnly]
    const int MAX_STACKS = 3;
    [FoldoutGroup("Properties"), ReadOnly]
    public int currentStackAmount;
    public static AbilityStackHandler instance;
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
    private float BufferCombatDelay = 1;

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
    }
    public void IncreaseBufferValue(float _amountToIncrease)
    {
        Buffer.value += _amountToIncrease;
        if(Buffer.value >= Buffer.maxValue)
        {
            IncreaseStackCount(); 
        }
        StartCoroutine(CanBufferRoutine());
    }
    private void LateUpdate()
    {
        ConstantlyDecreaseBuffer();
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

        onStackChange?.Invoke();
        UpdateUIElements();

    }
    private void IncreaseStackCount(int _amountToIncrease)
    {
        currentStackAmount += _amountToIncrease;
        if (currentStackAmount > MAX_STACKS)
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
}

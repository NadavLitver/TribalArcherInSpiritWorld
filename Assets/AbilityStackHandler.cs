using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class AbilityStackHandler : MonoBehaviour
{
    [FoldoutGroup("Properties"),ShowInInspector,ReadOnly]
    const int maxStacks = 3;
    [FoldoutGroup("Properties"), SerializeField, ReadOnly]
    int currentStackAmount;
    public static AbilityStackHandler instance;
    [FoldoutGroup("Events")]
    public UnityEvent onStackChange;
    [FoldoutGroup("Refrences")]
    public GameObject[] UIStackObjects;
    public void Awake()
    {
        if(instance == null)
        {
            instance = this;

        }
        else
        {
            Debug.LogError("singelton instance populated");
        }
        currentStackAmount = maxStacks;
    }
    public  void IncreaseStackCount()
    {
        currentStackAmount++;
        if (currentStackAmount > maxStacks)
            currentStackAmount = maxStacks;

        onStackChange?.Invoke();
        UpdateUIElements();

    }
    public  void IncreaseStackCount(int _amountToIncrease)
    {
        currentStackAmount += _amountToIncrease;
        if (currentStackAmount > maxStacks)
            currentStackAmount = maxStacks;

        onStackChange?.Invoke();
        UpdateUIElements();

    }
    public  void DecreaseStackCount()
    {
        currentStackAmount--;
        if (currentStackAmount < 0)
            currentStackAmount = 0;

        onStackChange?.Invoke();
        UpdateUIElements();

    }
    public  void DecreaseStackCount(int _amountToDecrease)
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
            if(i+1 > currentStackAmount)
            {
                UIStackObjects[i].SetActive(false);
            }
            else
            {
                UIStackObjects[i].SetActive(true);

            }
        }
    }
}

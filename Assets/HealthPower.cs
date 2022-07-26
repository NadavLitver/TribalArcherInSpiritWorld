using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPower : MonoBehaviour
{
    const int maxStacks = 2;
    private int CurrentStacks = 1;
    //refs
    [SerializeField] private ParticleSystem[] m_spawn;
    [SerializeField] private ParticleSystem[] m_death;
    [SerializeField] private Button[] buttons;

    [SerializeField] private HealthBarFlash flashRef;

    public bool isFull => (CurrentStacks >= maxStacks);

    private void Start()
    {
        UpdateButtons();
    }
    private void UpdateButtons()
    {
        for (int i = 0; i < maxStacks; i++)
        {
            buttons[i].interactable = i <= CurrentStacks - 1;
        }
    }
    public void UseOrb()
    {
        if (CurrentStacks > 0)
        {
            HUD.instance.playerBodyRef.RecieveHealth(25);
            CurrentStacks--;
            m_death[CurrentStacks].Play();
            UpdateButtons();
        }
    }
    public void GainOrb()
    {
        if (CurrentStacks < maxStacks)
        {
            m_spawn[CurrentStacks].Play();
            CurrentStacks++;
            UpdateButtons();
        }
    }

}

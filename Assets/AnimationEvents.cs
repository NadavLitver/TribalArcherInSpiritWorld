using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public GameObject[] arrows;
    public PlayerController m_player;
    public BowHandler m_bow;

    public void CanShootOff()
    {
        m_bow.canShoot = false;
    }
    public void CanShootOn()
    {
        m_bow.canShoot = true;
    }
    public void TurnOffArrow()
    {
        arrows[0].SetActive(false);
    }
    public void TurnOnArrow()
    {
        arrows[0].SetActive(true);

    }
    public void ScatterArrowsOn()
    {
        for (int i = 1; i < arrows.Length; i++)
        {
            arrows[i].SetActive(true);
        }
    }
    public void ScatterArrowsOff()
    {
        for (int i = 1; i < arrows.Length; i++)
        {
            arrows[i].SetActive(false);
        }
    }
}

using System.Collections;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public GameObject[] arrows;
    public PlayerController m_player;
    public BowHandler m_bow;

    public void CallLoadArrow()
    {
        StartCoroutine(Load());
    }

    IEnumerator Load()
    {
        m_bow.isLoaded = false;
        yield return new WaitForSeconds(0.2f);
        m_bow.isLoaded = true;

    }
    IEnumerator ArrowOnDelay()
    {
        yield return new WaitForSeconds(0.05f);
        TurnOffArrow();
        yield return new WaitForSeconds(0.2f);
        TurnOnArrow();

    }
    public void CallArrowOffOn()
    {
        StartCoroutine(ArrowOnDelay());
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

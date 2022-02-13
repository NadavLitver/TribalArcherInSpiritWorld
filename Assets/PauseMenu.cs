using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Animator m_anim;
    private bool Active = false;

    void OnEnable()
    {
        InputManager.Instance.OnPause.AddListener(Toggle);
    }
    private void Toggle()
    {
        Active = !Active;
        m_anim.SetBool("Active", Active);
        Time.timeScale = Active ? 0 : 1; //set pause game
    }
}

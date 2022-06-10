using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class FadeToBlack : MonoBehaviour
{
    Animator m_animator;
    [ReadOnly]public Image m_image;

    public void Start()
    {
        m_animator = GetComponent<Animator>();
        m_image = GetComponent<Image>();
    }
    public void PlayFade()
    {
        m_animator.Play("ToBlack");
    }
    public void PlayFromFade()
    {
        m_animator.Play("FromBlack");
    }
}

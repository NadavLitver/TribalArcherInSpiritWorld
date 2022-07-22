using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextBox : MonoBehaviour
{
    public static TextBox instance;
    private TextMeshProUGUI m_text;
    private Animator m_animator;
    private const string activeRef = "Activate"; // trigger
    private float defaultDelay = 3f;

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

    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Activate(string text)
    {
        StartCoroutine(ActivateRoutine(text, defaultDelay));
        
    }
    public void Activate(string text, float delay)
    {
        StartCoroutine(ActivateRoutine(text, delay));
    }
    private IEnumerator ActivateRoutine(string text, float delay)
    {
        yield return new WaitForSeconds(delay);
        m_text.text = text;
        m_animator.SetTrigger(activeRef);
    }
}

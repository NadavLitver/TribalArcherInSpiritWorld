using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private float ButtonPressDelay = 0.2f;

    [SerializeField] private Animator m_animator;
    private const string secondaryRef = "SecondaryOpen";
    private bool isSecondary = false;
    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void Button_Start()
    {
      
        StopAllCoroutines();
        StartCoroutine(StartRoutine());
    }
    private IEnumerator StartRoutine()
    {
        yield return new WaitForSeconds(ButtonPressDelay);
        SceneManager.LoadScene(1);
    }
    public void Button_Continue()
    {
        StopAllCoroutines();
        StartCoroutine(ContinueRoutine());
    }
    private IEnumerator ContinueRoutine()
    {
        yield return new WaitForSeconds(ButtonPressDelay);
        SceneManager.LoadScene(2);
    }
    public void Button_Exit()
    {
        StopAllCoroutines();
        StartCoroutine(ExitRoutine());
    }

    private IEnumerator ExitRoutine()
    {
        yield return new WaitForSeconds(ButtonPressDelay);
        Application.Quit();
    }
    public void OpenSecondary()
    {
        isSecondary = true;
        m_animator.SetBool(secondaryRef, true);
    }
    public void CloseSecondary()
    {
        isSecondary = false;
        m_animator.SetBool(secondaryRef, false);
    }
    public void Button_Options()
    {

    }
    public void Button_Credits()
    {

    }
}

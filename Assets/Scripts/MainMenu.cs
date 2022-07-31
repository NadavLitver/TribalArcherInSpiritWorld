using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private float ButtonPressDelay = 0.2f;
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
}

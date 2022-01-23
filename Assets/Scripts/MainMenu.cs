using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private float ButtonPressDelay = 0.2f;
    public void Button_Start()
    {
        StartCoroutine(StartRoutine());
    }
    private IEnumerator StartRoutine()
    {
        StopAllCoroutines();
        yield return new WaitForSeconds(ButtonPressDelay);
        SceneManager.LoadScene(1);
    }
    public void Button_Exit()
    {
        StartCoroutine(ExitRoutine());
    }
    private IEnumerator ExitRoutine()
    {
        StopAllCoroutines();
        yield return new WaitForSeconds(ButtonPressDelay);
        Application.Quit();
    }
}

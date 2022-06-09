using Sirenix.OdinInspector;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneMaster : MonoBehaviour
{
    public static SceneMaster instance;
    //[FoldoutGroup("Refrences")]
    //public Slider slider;
    //[FoldoutGroup("Refrences")]
    //public TextMeshProUGUI text;
    //[SerializeField, FoldoutGroup("Refrences")]
    //private GameObject loadingScreen;

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

        //DontDestroyOnLoad(gameObject);
        //loadingScreen.SetActive(false);
    }
    private void Start()
    {
    }

    public void LoadLevel(int index)
    {
        StartCoroutine(LoadAsync(index));
    }
    IEnumerator LoadAsync(int index)
    {
        //loadingScreen.SetActive(true);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(index);
        while (!asyncOperation.isDone)
        {
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            //slider.value = progress;
            //text.text = (progress * 100f) + "%";
            yield return null;
        }
        //loadingScreen.SetActive(false);

    }
    public int GetCurrentIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }
    public void LoadLevel(string scene)
    {
        StartCoroutine(LoadAsync(scene));
    }
    IEnumerator LoadAsync(string scene)
    {
        //loadingScreen.SetActive(true);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(scene);
        while (!asyncOperation.isDone)
        {
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            //slider.value = progress;
            //text.text = (progress * 100f) + "%";
            yield return null;
        }
        //loadingScreen.SetActive(false);

    }
    public void QuitApp()
    {
        Application.Quit();
    }

}

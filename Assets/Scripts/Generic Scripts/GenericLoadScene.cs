using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

//ATW 5/23/24
public class GenericLoadScene : MonoBehaviour
{
    [SerializeField] private int sceneIndex = -1;
    //[SerializeField] private bool shouldFakeLoad = false;

    public void LoadScene()
    {
        //if (shouldFakeLoad)
        //    StartCoroutine(nameof(LoadSceneRoutine));
        //else
            ActualLoadingSequence();
    }

    private IEnumerator LoadSceneRoutine()
    {
        yield return new WaitForSeconds(0.2f);

        //var loadingScreen = FindObjectOfType<FakeLoadingScreen>();

        //loadingScreen.StartLoading();
        //yield return new WaitUntil(loadingScreen.DoneLoading);

        ActualLoadingSequence();
    }

    private void ActualLoadingSequence()
    {
        if (sceneIndex < 0 || sceneIndex >= SceneManager.sceneCountInBuildSettings)
            DefaultToNextBuildIndex();
        else
            SceneManager.LoadSceneAsync(sceneIndex);
    }

    public void DefaultToNextBuildIndex()
    {
        if(SceneManager.GetActiveScene().buildIndex + 1 >= SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadSceneAsync(0);
        else
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    static public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#endif
    }
}

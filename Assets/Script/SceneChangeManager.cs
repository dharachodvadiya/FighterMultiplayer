using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class SceneChangeManager : Singleton<SceneChangeManager>
{
    public enum EnumScene
    {
        play,
        home
    }

    string currentScene = null;
    private GameObject m_LoadingScreenPrefab1 = null;

    //private LoadingScreen m_CurrentLoadingScreen1;

    public GameObject currentLoadingScreen1;

    protected override void OnAwake()
    {
        _persistent = true;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    private void Start()
    {
        m_LoadingScreenPrefab1 = Resources.Load("LoadingScreen") as GameObject;
    }

    private static IEnumerator LoadNextScreenCoroutine(string currScreen, string NextScreen)
    {


        //yield return UnloadScene(currScreen, 0.0f, 0.5f);
        //yield return SetActiveScene(EnumScene.Init.ToString());

        yield return LoadScene(NextScreen, 0.5f, 1f);
        //yield return SetActiveScene(NextScreen);


    }
    private static IEnumerator ReLoadCoroutine(string currScreen)
    {


        //yield return UnloadScene(currScreen, 0.0f, 0.5f);
        //yield return SetActiveScene(EnumScene.Init.ToString());

        yield return LoadScene(currScreen, 0.5f, 1f);
        // yield return SetActiveScene(currScreen);


    }

    #region Scene Management Utils

    private static IEnumerator LoadScene(string name, float loaderMin, float loaderMax)
    {
        var asyncOp = SceneManager.LoadSceneAsync(name);
        while (!asyncOp.isDone)
        {

            yield return null;
        }
    }
    private static IEnumerator SetActiveScene(string name)
    {
        var scene = SceneManager.GetSceneByName(name);
        Instance.currentScene = name;
        while (!scene.isLoaded)
        {
            yield return null;
        }

        SceneManager.SetActiveScene(scene);
    }

    #endregion


    public void LoadNextScreen(EnumScene nextScreen)
    {
        StartCoroutine(LoadNextScreenCoroutine(Instance.currentScene, nextScreen.ToString()));
    }
    public void ReloadScreen()
    {
        StartCoroutine(ReLoadCoroutine(Instance.currentScene));
    }

    public void StartLoading()
    {
        currentLoadingScreen1 = Instantiate(Instance.m_LoadingScreenPrefab1);
    }
    public void stopLoading()
    {
        if (currentLoadingScreen1 != null)
        {
            Destroy(currentLoadingScreen1.gameObject);
            currentLoadingScreen1 = null;
        }
    }

    protected void OnDisable()
    {
        StopAllCoroutines();
    }
    protected void OnDestroy()
    {
        StopAllCoroutines();
    }
}
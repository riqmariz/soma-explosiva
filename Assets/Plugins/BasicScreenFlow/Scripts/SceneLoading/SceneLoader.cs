using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    //Your loading scene's build name goes here
    private static string _loadingSceneName = "Loading";
    private static Action _onLoaderCallback;
    private static AsyncOperation _loadingaAsyncOperation;

    private class Dummy : MonoBehaviour { }


    public static void LoadAdditiveScene(int buildIndex)
    {
        SceneManager.LoadSceneAsync(buildIndex, LoadSceneMode.Additive);
    }

    public static AsyncOperation LoadAdditiveScene(string buildName)
    {;
        return SceneManager.LoadSceneAsync(buildName, LoadSceneMode.Additive);
    }

    /// <summary>
    /// Use this method to load scenes asyncronously using your loading scene.
    /// </summary>
    /// <param name="buildIndex"></param>
    public static void LoadUsingLoadingScene(int buildIndex)
    {
        //Setting the loader callback action
        _onLoaderCallback = () =>
        {
            var dummyGO = new GameObject("Dummy");
            dummyGO.AddComponent<Dummy>().StartCoroutine(LoadSceneAsync(buildIndex));
        };

        Debug.Log("load");
        SceneManager.LoadScene(_loadingSceneName);
    }

    /// <summary>
    /// Use this method to load scenes asyncronously using your loading scene.
    /// </summary>
    /// <param name="buildName"></param>
    public static void LoadUsingLoadingScene(string buildName)
    {
        //Setting the loader callback action
        _onLoaderCallback = () =>
        {
            var dummyGO = new GameObject("Dummy");
            dummyGO.AddComponent<Dummy>().StartCoroutine(LoadSceneAsync(buildName));
        };

        Debug.Log("load");
        SceneManager.LoadScene(_loadingSceneName);
    }

    private static IEnumerator LoadSceneAsync(int buildIndex)
    {
        yield return null;

        _loadingaAsyncOperation = SceneManager.LoadSceneAsync(buildIndex);

        while (!_loadingaAsyncOperation.isDone) { yield return null; }
    }

    private static IEnumerator LoadSceneAsync(string buildName)
    {
        yield return null;

        _loadingaAsyncOperation = SceneManager.LoadSceneAsync(buildName);

        while (!_loadingaAsyncOperation.isDone) { yield return null; }
    }

    /// <summary>
    /// Returns the loading progress of the scene currently loading or 0f if there isn't one.
    /// </summary>
    /// <returns></returns>
    public static float GetLoadingProgress()
    {
        if (_loadingaAsyncOperation != null)
        {
            return Mathf.Clamp01(_loadingaAsyncOperation.progress / .9f);
        }
        else
            return 0f;
    }

    public static void LoaderCallback()
    {
        if(_onLoaderCallback != null)
        {
            _onLoaderCallback();
            _onLoaderCallback = null;
        }
    }

    public static void UnloadScene(int buildIndex)
    {
        SceneManager.UnloadSceneAsync(buildIndex);
    }

    public static void UnloadScene(string buildName)
    {
        SceneManager.UnloadSceneAsync(buildName);
    }

    public static void SetActiveScene(String sceneName) 
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
    }

    /// <summary>
    /// Sets the scene to be used as loading.
    /// </summary>
    /// <param name="loadingSceneName"></param>
    public static void SetLoadingScene(string loadingSceneName)
    {
        _loadingSceneName = loadingSceneName;
    }
}

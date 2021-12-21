﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public string menuSceneName = "MainMenu";

    [SerializeField]
    private List<string> levelSceneNames;
    public List<string> LevelSceneNames => levelSceneNames;

    public int currentLevelSceneNameIndex;

    public void Pause()
    {
        Time.timeScale = 0f;
    }

    public void Resume() 
    {
        Time.timeScale = 1f;
    }

    public void Home()
    {
        SceneLoader.LoadUsingLoadingScene(menuSceneName);
        Resume();
    }

    public void Reset()
    {
        var currScene = SceneManager.GetActiveScene().name;
        SceneLoader.LoadUsingLoadingScene(currScene);
        Resume();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] 
    private string levelSelectScene;


    public void GoToLevelSelect()
    {
        SceneLoader.LoadUsingLoadingScene(levelSelectScene);
    }
    
}

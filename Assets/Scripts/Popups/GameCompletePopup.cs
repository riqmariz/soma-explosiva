using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCompletePopup : GenericPopup
{
    public override void Show()
    {
        GameManager.GetInstance().Pause();
        base.Show();
    }
    public void RequestShow() => PopupManager.GetInstance().ShowPopup<GameCompletePopup>();
    public override void RequestHide() => PopupManager.GetInstance().ClosePopup<GameCompletePopup>();

    public void Home() => GameManager.GetInstance().Home();
    public void Reset() => GameManager.GetInstance().Reset();

    public void NextLevel() 
    {
        string scene = SceneManager.GetActiveScene().name;
        int level = int.Parse(""+scene[scene.Length-1]);
        var sceneName = scene.Substring(scene.Length-2) + (level+1);
        var hasScene = SceneManager.GetSceneByName(sceneName) != null;
        if (hasScene)
        {
            SceneLoader.LoadUsingLoadingScene(sceneName);
        }
        else 
        {
            Home();
        }
    }
}

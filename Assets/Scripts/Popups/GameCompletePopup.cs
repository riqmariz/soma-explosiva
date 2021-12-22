using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCompletePopup : GenericPopup
{
    public override void Show()
    {
        GameManager.GetInstance().Pause();
        GameManager.GetInstance().CompleteCurrentLevel();
        base.Show();
    }
    public void RequestShow() => PopupManager.GetInstance().ShowPopup<GameCompletePopup>();
    public override void RequestHide()
    {
        GameManager.GetInstance().Resume();
        PopupManager.GetInstance().ClosePopup<GameCompletePopup>();
    }

    public void Home()
    {
        GameManager.GetInstance().Resume();
        GameManager.GetInstance().Home();
    }

    public void Reset()
    {
        GameManager.GetInstance().Resume();
        GameManager.GetInstance().Reset();
    }

    public void NextLevel() 
    {
        if (!GameManager.GetInstance().NextLevel())
        {
            GameManager.GetInstance().Home();
        }
    }
}

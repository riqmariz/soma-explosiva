using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPopup : GenericPopup
{
    public override void Show()
    {
        GameManager.GetInstance().Pause();
        base.Show();
    }
    public void RequestShow()
    {
        PopupManager.GetInstance().ShowPopup<GameOverPopup>();
    }

    public override void RequestHide()
    {
        GameManager.GetInstance().Resume();
        PopupManager.GetInstance().ClosePopup<GameOverPopup>();
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
}

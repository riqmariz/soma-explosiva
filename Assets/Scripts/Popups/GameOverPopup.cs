using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPopup : GenericPopup
{
    public void RequestShow() => PopupManager.GetInstance().ShowPopup<GameOverPopup>();
    public override void RequestHide() => PopupManager.GetInstance().ClosePopup<GameOverPopup>();

    public void Home() => GameManager.GetInstance().Home();
    public void Reset() => GameManager.GetInstance().Reset();
}

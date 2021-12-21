using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContextMenu : GenericPopup
{
    public Image sfxButton;
    public Sprite sfxOff;
    public Sprite sfxOn;
    
    public Image bgmButton;
    public Sprite bgmOff;
    public Sprite bgmOn;

    private void Awake()
    {
        UpdateUI();
    }
    private void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        sfxButton.sprite = SFX ? sfxOn : sfxOff;
        bgmButton.sprite = BGM ? bgmOn : bgmOff;
    }

    public bool BGM 
    {
        get => AudioManager.GetInstance().IsMusicPlaying();
        set 
        {
            AudioManager.GetInstance().ToggleMusic(value);
            UpdateUI();
        }
    }
    public bool SFX 
    {
        get => AudioManager.GetInstance().IsSoundPlaying();
        set 
        {
            AudioManager.GetInstance().ToggleSounds(value);
            UpdateUI();
        }
    }

    public void OnSFXButton() 
    {
        SFX = !SFX;
    }

    public void OnBGMButton() 
    {
        BGM = !BGM;
    }

    public override void RequestHide() => PopupManager.GetInstance().ClosePopup<ContextMenu>();
    public void RequestShow() => PopupManager.GetInstance().ShowPopup<ContextMenu>();

    public void Home() => GameManager.GetInstance().Home();
    public void Reset() => GameManager.GetInstance().Reset();

    public override void Hide()
    {
        GameManager.GetInstance().Resume();
        base.Hide();
    }


    public override void Show()
    {
        GameManager.GetInstance().Pause();
        base.Show();
    }
}

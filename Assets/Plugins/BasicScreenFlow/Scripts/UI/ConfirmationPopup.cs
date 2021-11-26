using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ConfirmationPopup : GenericPopup
{
    [SerializeField] private TMP_Text descriptionText;

    private Action _onPositiveCase;

    /// <summary>
    /// Sets the action to be called when player presses 'Yes' button.
    /// </summary>
    /// <param name="action"></param>
    public void SetPositiveCase(Action action)
    {
        _onPositiveCase = action;
    }

    /// <summary>
    /// Sets the text shown when popup opens.
    /// </summary>
    /// <param name="text"></param>
    public void SetText(string text)
    {
        descriptionText.text = text;
    }

    public void OnYes()
    {
        RequestHide();

        _onPositiveCase?.Invoke();
        _onPositiveCase = null;
    }

    public void OnNo()
    {
        RequestHide();
        _onPositiveCase = null;
    }

    public override void RequestHide()
    {
        PopupManager.GetInstance().ClosePopup<ConfirmationPopup>();
    }
}

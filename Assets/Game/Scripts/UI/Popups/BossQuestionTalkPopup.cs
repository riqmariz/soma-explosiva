using System;
using System.Collections.Generic;
using SharedData.Values;
using TMPro;
using UnityEngine;

public class BossQuestionTalkPopup : GenericPopup
{
    [SerializeField] 
    private StringListValue questionText;
    [SerializeField] 
    private TextMeshProUGUI questionTMP;
    [SerializeField] 
    private bool disableOnAwake;
    /*[SerializeField] 
    private Button nextButton;
    [SerializeField] 
    private Button previousButton;
    [SerializeField] 
    private Button continueButton;*/

    private void Awake()
    {
        questionText.AddOnValueChanged(UpdateTextAndShowPopup);
        gameObject.SetActive(!disableOnAwake);
    }

    private void Start()
    {
    }
    private void UpdateTextAndShowPopup(List<string> newQuestionText)
    {
        //todo change later to have a logic to next and previous buttons
        questionTMP.text = newQuestionText[0];
        
        RequestShow();
    }
    public void RequestShow()
    {
        //temp time scale here
        Time.timeScale = 0f;
        PopupManager.GetInstance().ShowPopup<BossQuestionTalkPopup>();
    }
    public override void RequestHide()
    {
        Time.timeScale = 1f;
        PopupManager.GetInstance().ClosePopup<BossQuestionTalkPopup>();
    }

    private void OnDestroy()
    {
        questionText.RemoveOnValueChanged(UpdateTextAndShowPopup);
    }
}

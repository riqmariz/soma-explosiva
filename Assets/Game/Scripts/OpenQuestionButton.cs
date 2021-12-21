using SharedData.Values;
using UnityEngine;
using UnityEngine.UI;

public class OpenQuestionButton : MonoBehaviour
{
    [SerializeField] 
    private bool disableOnAwake = true;
    [SerializeField] 
    private IntValue targetBossValue;

    [SerializeField] 
    private BossQuestionTalkPopup _bossQuestionTalkPopup;
    
    private Button _button;
    
    void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OpenQuestion);
        gameObject.SetActive(!disableOnAwake);
        targetBossValue.AddOnValueChanged(ActivateObject);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OpenQuestion);
        targetBossValue.RemoveOnValueChanged(ActivateObject);
    }

    private void ActivateObject(int i)
    {
        if (i > 0)
        {
            gameObject.SetActive(true);
        }
    }

    void OpenQuestion()
    {
        _bossQuestionTalkPopup.RequestShow();
    }
}

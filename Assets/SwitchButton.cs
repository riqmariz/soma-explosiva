using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchButton : MonoBehaviour
{
    private Button _button;
    private ModifierManager _modifierManager;
    private void Awake()
    {
        _button = GetComponent<Button>();
        _modifierManager = FindObjectOfType<ModifierManager>();
        _button.onClick.AddListener(_modifierManager.SwitchModifiers);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(_modifierManager.SwitchModifiers);
    }
}

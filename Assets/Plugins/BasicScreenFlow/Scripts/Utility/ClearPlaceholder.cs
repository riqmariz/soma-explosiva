using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClearPlaceholder : MonoBehaviour, ISelectHandler {
    [SerializeField] TMP_Text _placeholder;

    string _oldText;

    public void OnSelect (BaseEventData data) {
        _oldText = _placeholder.text;
        _placeholder.text = "";
    }

    public void BackPlaceholder () {
        _placeholder.text = _oldText;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class HoldValue : MonoBehaviour
{
    [SerializeField] private int _value;
    [SerializeField] private TextMeshPro valueField;


    public int Value 
    {
        get 
        {
            return _value;
        }
        set 
        {
            _value = value;
            UpdateField();
        }
    }

    private void UpdateField() 
    {
        valueField.text = Value.ToString();
    }

    public void ApplyModifier(Func<int,int> modifier) 
    {
        Value = modifier(Value);
    }

    private void OnValidate()
    {
        UpdateField();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class SingleModIlustration : MonoBehaviour
{
    [SerializeField]
    TextMeshPro descriptionField;
    [SerializeField]
    SpriteRenderer sprt;

    public void SetDescription(string description) 
    {
        descriptionField.text = description;    
    }

    public void DoTransParency(float a, float time) 
    {
        sprt.DOFade(a,time);
    }
}

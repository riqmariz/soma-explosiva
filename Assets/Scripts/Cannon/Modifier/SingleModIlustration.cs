using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SingleModIlustration : MonoBehaviour
{
    [SerializeField]
    TextMeshPro descriptionField;

    public void SetDescription(string description) 
    {
        descriptionField.text = description;    
    }
}

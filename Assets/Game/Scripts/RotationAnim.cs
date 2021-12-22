using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RotationAnim : MonoBehaviour
{
    [SerializeField] 
    private Vector3 endRotation;

    [SerializeField] 
    private float animDuration;

    [SerializeField] 
    private RotateMode animRotateMode;
    
    private Sequence _sequence;
    void Start()
    {
        //_initRotation = transform.localRotation.eulerAngles;
        _sequence = DOTween.Sequence();
        _sequence.Append(transform.DOLocalRotate(endRotation, animDuration, animRotateMode).SetEase(Ease.Linear));
        _sequence.SetLoops(-1);
        //_sequence.;
        _sequence.Play();
    }
}

using DG.Tweening;
using UnityEngine;

public class SkullJawAnimation : MonoBehaviour
{
    [SerializeField] 
    private Transform skullJaw;

    [SerializeField] 
    private Vector3 endRotation;

    [SerializeField] 
    private float animDuration;

    [SerializeField] 
    private RotateMode animRotateMode;
    
    private Sequence _sequence;
    private Vector3 _initRotation;
    void Start()
    {
        _initRotation = skullJaw.transform.localRotation.eulerAngles;
        _sequence = DOTween.Sequence();
        _sequence.Append(skullJaw.transform.DOLocalRotate(endRotation, animDuration, animRotateMode));
        _sequence.Append(skullJaw.transform.DOLocalRotate(_initRotation, animDuration, animRotateMode));
        _sequence.SetLoops(-1);
        _sequence.Play();
    }
}

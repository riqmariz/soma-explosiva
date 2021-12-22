using Event = SharedData.Events.Event;
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

    [SerializeField] 
    private Event onValidHitBoss;
    
    private Sequence _sequence;
    private Vector3 _initRotation;

    private void Awake()
    {
        onValidHitBoss.AddCallback(StopWhileInvulnerable);
    }

    private void OnDestroy()
    {
        onValidHitBoss.RemoveCallback(StopWhileInvulnerable);   
    }

    void Start()
    {
        _initRotation = skullJaw.transform.localRotation.eulerAngles;
        _sequence = DOTween.Sequence();
        _sequence.Append(skullJaw.transform.DOLocalRotate(endRotation, animDuration, animRotateMode));
        _sequence.Append(skullJaw.transform.DOLocalRotate(_initRotation, animDuration, animRotateMode));
        _sequence.SetLoops(-1);
        _sequence.Play();
    }

    void StopWhileInvulnerable()
    {
        Timers.CreateClock(
            this.gameObject,
            BossHP._InvulnerableTime,
            () => _sequence.Pause(),
            () => _sequence.Play()
            );
    }
}

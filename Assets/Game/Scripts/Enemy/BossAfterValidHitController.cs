using UnityEngine;
using Event = SharedData.Events.Event;
public class BossAfterValidHitController : MonoBehaviour
{
    [SerializeField] 
    private Event onValidHit;
    [SerializeField] 
    private BossMovementManager movementManager;
    private void Awake()
    {
        onValidHit.AddCallback(AfterHitRoutine);
    }
    private void AfterHitRoutine()
    {
        //just stops for now
        Timers.CreateClock(
            gameObject,
            BossHP._InvulnerableTime,
            movementManager.StopMovement,
            movementManager.StartMovement
            );
    }
    private void OnDestroy()
    {
        onValidHit.RemoveCallback(AfterHitRoutine);
    }
}

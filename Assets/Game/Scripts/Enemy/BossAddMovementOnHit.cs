using UnityEngine;
using Event = SharedData.Events.Event;

public class BossAddMovementOnHit : MonoBehaviour
{
    [SerializeField] 
    private Event onValidHitOnBoss;

    [SerializeField] 
    private MovementComponent movementComponent;

    [SerializeField] 
    private float bonusMovementOnHit;

    private int index;

    private void Awake()
    {
        onValidHitOnBoss.AddCallback(AddMovementOnHitEvent);
    }

    private void OnDestroy()
    {
        onValidHitOnBoss.RemoveCallback(AddMovementOnHitEvent);
    }
    private void AddMovementOnHitEvent()
    {
        var currentSpeed = movementComponent.GetSpeed();
        var bonusSpeed = bonusMovementOnHit;
        movementComponent.SetSpeed(currentSpeed+bonusSpeed);
        Debug.Log("Adding "+bonusMovementOnHit+" movement bonus on boss");
    }
    
}

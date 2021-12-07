using System.Collections.Generic;
using UnityEngine;

public class BossMovementManager : MonoBehaviour
{
    [SerializeField] 
    private List<BossMovementPoint> bmps;
    [SerializeField] 
    private MovementComponent movementComponent;
    [SerializeField] 
    private bool startMoving = true;

    private float MIN_DISTANCE_OFFSET = 0.01f;
    
    private bool isMoving;
    public bool IsMoving => isMoving;

    private List<BossMovementPoint> runtimeBmps;
    private BossMovementPoint currentBmp;

    private void Awake()
    {
        isMoving = startMoving;
        currentBmp = GetNextPoint();
        ForceGoToBMP(currentBmp,GetNextPoint());
    }

    private void ForceGoToBMP(BossMovementPoint pointToGO, BossMovementPoint nextToGO)
    {
        transform.position = pointToGO.GetPosition();
        currentBmp = nextToGO;
    }

    private BossMovementPoint GetNextPoint()
    {
        if (runtimeBmps == null || runtimeBmps.Count <= 0)
        {
            if (bmps.Count > 0)
            {
                runtimeBmps = new List<BossMovementPoint>(bmps);
            }
            else
            {
                Debug.LogError("error on the size of the bmp");
            }
        }

        var nextPoint = runtimeBmps[0];
        runtimeBmps.Remove(nextPoint);
        
        return nextPoint;
    }
    private void Update()
    {
        if (isMoving)
        {
            var a = transform.position;
            var b = currentBmp.GetPosition();
            var distance = Vector2.Distance(a, b);
            //Debug.Log(distance);
            if (distance > MIN_DISTANCE_OFFSET)
            {
                var dir = b - a;
                movementComponent.Move(dir);
            }
            else
            {
                //for now skips one frame
                currentBmp = GetNextPoint();
            }
        }
    }

    public void StartMovement()
    {
        isMoving = true;
    }

    public void StopMovement()
    {
        isMoving = false;
    }
}

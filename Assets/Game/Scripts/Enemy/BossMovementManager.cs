using System;
using System.Collections.Generic;
using UnityEngine;
using Event = SharedData.Events.Event;

public class BossMovementManager : MonoBehaviour
{
    [SerializeField] 
    private List<BossPath> pathList;
    [SerializeField] 
    private MovementComponent movementComponent;
    [SerializeField] 
    private Event changePathEvent;
    
    [SerializeField] 
    private bool startMoving = true;

    private float MIN_DISTANCE_OFFSET = 0.01f;
    
    private bool isMoving;
    public bool IsMoving => isMoving;

    private List<BossPath> runtimePaths;
    private List<BossMovementPoint> runtimeBmps;
    private BossMovementPoint currentBmp;
    private BossPath currentPath;

    private void Start()
    {
        isMoving = startMoving;
        ChangeToNextPath();
        currentBmp = GetNextPoint();
        ForceGoToBMP(currentBmp,GetNextPoint());
        changePathEvent.AddCallback(ChangeToNextPath);
    }

    private void OnDestroy()
    {
        changePathEvent.RemoveCallback(ChangeToNextPath);
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
            runtimeBmps = new List<BossMovementPoint>(currentPath.BossMovementPointList);
            if (runtimeBmps.Count < 2) 
            {
                Debug.LogError("error on the size of the bmp");
            }
        }

        var nextPoint = runtimeBmps[0];
        runtimeBmps.Remove(nextPoint);
        
        return nextPoint;
    }

    private void ChangeToNextPath()
    {
        if (runtimePaths == null || runtimePaths.Count <= 0)
        {
            if (pathList.Count > 0)
            {
                runtimePaths = new List<BossPath>(pathList);
            } else
            {
                Debug.LogError("error on the size of the pathlist");
            }
        }

        var nextPath = runtimePaths[0];
        runtimePaths.Remove(nextPath);
        currentPath = nextPath;
        runtimeBmps = null;
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

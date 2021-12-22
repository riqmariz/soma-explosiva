using System;
using System.Collections.Generic;
using System.Linq;
using Ball;
using Path;
using UnityEngine;
using Event = SharedData.Events.Event;

public class DashBallsOnInvalidHit : MonoBehaviour
{
    [SerializeField] 
    private Event onInvalidHit;
    [SerializeField] 
    private float increasePosition = 3f;

    private PathManager _pathManager;
    private void Awake()
    {
        _pathManager = FindObjectOfType<PathManager>();
        onInvalidHit.AddCallback(DashBalls);
    }
    private void OnDestroy()
    {
        onInvalidHit.RemoveCallback(DashBalls);
    }
    private void DashBalls()
    {
        var currentBalls = _pathManager.Balls;
        IncreaseBallsPosition(currentBalls, increasePosition);
    }
    private void IncreaseBallsPosition(List<PathBall> balls, float increasePosition)
    {
        foreach (var ball in balls)
        {
            ball.SetPosition(ball.Position+increasePosition);
        }
    }
}

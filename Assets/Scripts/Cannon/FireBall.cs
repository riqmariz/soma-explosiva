using System.Collections;
using System.Collections.Generic;
using Ball;
using UnityEngine;


public class FireBall : MonoBehaviour
{
    [SerializeField] private MoveToDirection BallPrefab;
    [SerializeField] private float initialBallDistanceFromCenter = 1;
    [SerializeField] private FollowPointer directionManager;
    [SerializeField] private float ballSpeed = 4;
    [SerializeField] private ModifierManager modManager;
    public delegate int ModifierDelegate(int value);
    private ModifierDelegate currentModifier;

    private void Start()
    {
        currentModifier = modManager.CurrentModifier.ModifierMethod;
        modManager.ModifierListUpdated.AddListener((list) => { currentModifier = modManager.CurrentModifier.ModifierMethod; });
    }

    public void Fire()
    {
        var ball = Instantiate(BallPrefab.gameObject, null).GetComponent<MoveToDirection>();
        ball.Direction = directionManager.GetDirection();
        ball.transform.position = InitialBallPosition;
        ball.Speed = ballSpeed;

        var valueHolder = ball.GetComponent<HoldValue>();
        ApplyModifier(currentModifier, valueHolder);
    }

    public void SetCurrentModifier(ModifierDelegate modifier) 
    {
        currentModifier = modifier;
    }

    private void ApplyModifier(ModifierDelegate modifier, HoldValue value) 
    {
        if(modifier != null)
            value.Value = modifier(value.Value);
    }

    private Vector3 InitialBallPosition
    {
        get 
        {
            return (Vector3)InitialBallDistanceFromCenter + transform.position;
        }
    }

    private Vector2 InitialBallDistanceFromCenter 
    {
        get 
        {
            return directionManager.GetDirection() * initialBallDistanceFromCenter;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(InitialBallPosition,1);
    }
}

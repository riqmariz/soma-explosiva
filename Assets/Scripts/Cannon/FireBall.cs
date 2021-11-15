using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FireBall : MonoBehaviour
{
    [SerializeField] private MoveToDirection BallPrefab;
    [SerializeField] private float initialBallDistanceFromCenter = 1;
    [SerializeField] private FollowPointer directionManager;

    public void Fire()
    {
        var ball = Instantiate(BallPrefab.gameObject, null).GetComponent<MoveToDirection>();
        ball.Direction = directionManager.GetDirection();
        ball.transform.position = InitialBallPosition;
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

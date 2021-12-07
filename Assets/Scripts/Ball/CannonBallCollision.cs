using System;
using UnityEngine;
using Ball;
using Path;

public class CannonBallCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var otherBall = other.gameObject.GetComponent<PathBall>();

        if (otherBall)
        {
            PathManager manager = GameObject.FindObjectOfType<PathManager>();
            int otherBallIndex = manager.Balls.IndexOf(otherBall);
            var befAf = otherBall.BeforeAfterBall;
            float diftBef = Vector3.Distance(transform.position, befAf.Item2.position);
            float diftAf = Vector3.Distance(transform.position, befAf.Item1.position);

            if (diftBef < diftAf)
            {
                manager.SpawnBallAt(otherBallIndex, otherBallIndex + 1, GetComponentInParent<HoldValue>().Value);
            }
            else
            {
                manager.SpawnBallAt(otherBallIndex, otherBallIndex, GetComponentInParent<HoldValue>().Value);
            }

            Destroy(transform.parent.gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        var takeDamage = other.gameObject.GetComponentInChildren<ITakeDamage>();
        //temp call to hold value
        var holdValue = GetComponentInParent<HoldValue>();
        takeDamage?.TakeDamage(holdValue.gameObject,holdValue.Value);
    }
}

using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MovementComponent : MonoBehaviour
{
    [SerializeField] protected float movementSpeed;
    public void SetSpeed(float speed)
    {
        movementSpeed = speed;
    }

    public float GetSpeed()
    {
        return movementSpeed;
    }
    public virtual void Move(Vector2 direction)
    {
        direction = direction.normalized;

        Vector2 position = transform.position;

        position += direction * (movementSpeed * Time.deltaTime);

        transform.position = position;
    }
}
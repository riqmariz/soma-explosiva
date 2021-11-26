using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MovementComponent : MonoBehaviour
{
    [SerializeField] protected float movementSpeed; //get this value from the spaceship

    private void Start()
    {
        
    }

    public void SetSpeed(float speed)
    {
        movementSpeed = speed;
    }

    public float GetSpeed()
    {
        return movementSpeed;
    }
    
    /// <summary>
    /// Moves the object in the normalized direction passed in params in fixedUpdate delta time
    /// </summary>
    /// <param name="direction">Parameter Vector2 to pass.</param>
    public virtual void Move(Vector2 direction)
    {
        direction = direction.normalized;

        Vector2 position = transform.position;

        position += direction * (movementSpeed * Time.fixedDeltaTime);

        transform.position = position;
    }
}
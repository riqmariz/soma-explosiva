using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnArea : MonoBehaviour
{
    [SerializeField] 
    private bool useTransformAsCenterPosition = true;
    [ConditionalHide("useTransformAsCenterPosition",true, true)]
    [SerializeField]
    private Transform CenterPoint;
    [SerializeField]
    private Vector3 Size = Vector3.one;
    [SerializeField] 
    private SpawnAreaPosition spawnPosition;
    public SpawnAreaPosition SpawnPosition => spawnPosition;

    private Color gizmosColor = Color.yellow;
    private Color onGizmosSelectedColor = Color.red;

    private void Awake()
    {
        if (useTransformAsCenterPosition)
        {
            CenterPoint = transform;
        }
        else
        {
            if (CenterPoint == null)
            {
                Debug.Log("should choose on inspector a center point");
            }
        }
    }

    public Vector3 GetRandomSpawnLocation()
    {
        Vector3 Position = CenterPoint.position + 
                           new Vector3(
                               Random.Range(-Size.x / 2, Size.x / 2),
                               Random.Range(-Size.y / 2, Size.y / 2),
                               CenterPoint.position.z);
        return Position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmosColor;
        Transform center; 
        if (useTransformAsCenterPosition)
        {
            center = transform;
        }
        else
        {
            center = CenterPoint;
        }
        Gizmos.DrawWireCube(center.position, Size);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = onGizmosSelectedColor;
        Transform center; 
        if (useTransformAsCenterPosition)
        {
            center = transform;
        }
        else
        {
            center = CenterPoint;
        }
        Gizmos.DrawWireCube(center.position, Size);
    }
}
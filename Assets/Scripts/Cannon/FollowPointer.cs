using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPointer : MonoBehaviour
{
    public float Offset;

    void Update()
    {
        var mousePos = Input.mousePosition;
        Vector3 difference = mousePos - Camera.main.WorldToScreenPoint(transform.position);
        difference.Normalize();
        float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotation_z + Offset);
    }

    public Vector2 GetDirection() 
    {
        Quaternion rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + Offset + 90f);
        Vector2 v = rotation * Vector3.down;

        return v.normalized;
    }
}

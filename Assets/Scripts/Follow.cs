using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform m_target;
    public Vector2 m_offset;
    // Update is called once per frame
    void Update()
    {
        var targetPosition = m_target.position;
        targetPosition.z = -0.5f;
        transform.position = targetPosition + (Vector3)m_offset;
    }
}

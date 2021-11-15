using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventOnClick : MonoBehaviour
{
    [SerializeField] private UnityEvent Click;

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
            Click.Invoke();
    }
}

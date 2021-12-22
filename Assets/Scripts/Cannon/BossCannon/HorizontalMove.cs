using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMove : MonoBehaviour
{
    public float speed = 1f;
    public Vector3 dirMove = Vector3.right;
    public DirButton leftButton;
    public DirButton rightButton;
    public bool LeftButton { get { return leftButton.mouseDown; } }
    public bool RightButton { get { return rightButton.mouseDown; } }

    public float maxX = 4f;


    void Update()
    {
        var dir = 0f;

        if (LeftButton)
            dir += -1f;

        if (RightButton)
            dir += 1f;

        transform.position += dir * dirMove * speed * Time.deltaTime;

        if (transform.position.x > maxX)
            transform.position = new Vector3(maxX, transform.position.y);
        else if (transform.position.x < -maxX)
            transform.position = new Vector3(-maxX, transform.position.y);
    }


}

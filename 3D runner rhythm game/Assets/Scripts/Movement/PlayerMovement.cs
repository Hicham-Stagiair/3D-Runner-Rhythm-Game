using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveDistance = 4f;
    
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow) && transform.position.x > -4)
        {
            MoveLeft();
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow) && transform.position.x < 4)
        {
            MoveRight();
        }
    }
    
    private void MoveLeft()
    {
        transform.position += Vector3.left * moveDistance;
    }
    
    private void MoveRight()
    {
        transform.position += Vector3.right * moveDistance;
    }
}
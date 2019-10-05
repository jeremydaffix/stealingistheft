using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    enum PlayerDirection
    {
        Left,
        TopLeft,
        Top,
        TopRight,
        Right,
        BottomRight,
        Bottom,
        BottomLeft,
        None
    }

    private PlayerDirection currentDirection = PlayerDirection.None;

    [SerializeField]
    private float speed = 0.1f;


    // components

    Rigidbody2D rb;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        CalcDirection();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }




    void CalcDirection()
    {
        if (Input.GetAxisRaw("Horizontal") > 0.1f) // right
        {
            if (Input.GetAxisRaw("Vertical") > 0.1f) // top
            {
                currentDirection = PlayerDirection.TopRight;
            }

            else if (Input.GetAxisRaw("Vertical") < -0.1f) // bottom
            {
                currentDirection = PlayerDirection.BottomRight;
            }

            else
            {
                currentDirection = PlayerDirection.Right;
            }
        }

        else if (Input.GetAxisRaw("Horizontal") < -0.1f) // left
        {
            if (Input.GetAxisRaw("Vertical") > 0.1f) // top
            {
                currentDirection = PlayerDirection.TopLeft;
            }

            else if (Input.GetAxisRaw("Vertical") < -0.1f) // bottom
            {
                currentDirection = PlayerDirection.BottomLeft;
            }

            else
            {
                currentDirection = PlayerDirection.Left;
            }
        }

        else
        {
            if (Input.GetAxisRaw("Vertical") > 0.1f) // top
            {
                currentDirection = PlayerDirection.Top;
            }

            else if (Input.GetAxisRaw("Vertical") < -0.1f) // bottom
            {
                currentDirection = PlayerDirection.Bottom;
            }

            else // idle
            {
                currentDirection = PlayerDirection.None;
            }
        }
    }


    void MovePlayer()
    {
        if(currentDirection != PlayerDirection.None)
        {
            Vector3 moving = new Vector3();

            if (currentDirection == PlayerDirection.Left || currentDirection == PlayerDirection.BottomLeft || currentDirection == PlayerDirection.TopLeft)
                moving.x = -1f;

            else if (currentDirection == PlayerDirection.Right || currentDirection == PlayerDirection.BottomRight || currentDirection == PlayerDirection.TopRight)
                moving.x = 1f;


            if (currentDirection == PlayerDirection.Top || currentDirection == PlayerDirection.TopLeft || currentDirection == PlayerDirection.TopRight)
                moving.y = 1f;

            else if (currentDirection == PlayerDirection.Bottom || currentDirection == PlayerDirection.BottomLeft || currentDirection == PlayerDirection.BottomRight)
                moving.y = -1f;


            rb.MovePosition(transform.position + moving * speed);
        }
    }
}

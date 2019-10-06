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

    public static PlayerController PlayerInstance = null;

    [SerializeField]
    private float speed = 0.1f;

    private float speedBoost = 1f;


    // components

    Rigidbody2D rb;
    Animator anim;



    void Start()
    {
        PlayerInstance = this;

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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

        if(Input.GetButton("Fire3"))
        {
            speedBoost = 2.0f;
        }

        else
        {
            speedBoost = 1f;
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


            Vector3 newPos = transform.position + moving * speed * speedBoost;

            var dir = newPos - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            rb.SetRotation(Quaternion.AngleAxis(angle - 90f, Vector3.forward));

            //transform.LookAt(newPos);
            rb.MovePosition(newPos); ;

            anim.SetBool("isWalking", true);
        }

        else
        {
            anim.SetBool("isWalking", false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private float speed;

    private float input;

    private float velocityx;

    private bool OnGround;

    private bool sideJumpUsed;

    public Rigidbody2D rb;

    void Awake()
    {
        OnGround = false;
        sideJumpUsed = false;
        speed = 5f;
    }

    void FixedUpdate()
    {
        input = Input.GetAxisRaw("Horizontal");

        if (OnGround || input != 0)
        {
            velocityx = input * speed;
        }

        rb.velocity = new Vector2(velocityx, rb.velocity.y);
        if(Input.GetAxisRaw("Jump") == 1 && OnGround)
        {
            Jump();
        }
        else if (Input.GetAxisRaw("SideJump") != 0 && !sideJumpUsed)
        {
            Jump(Input.GetAxisRaw("SideJump"));
            sideJumpUsed = true;
        }
    }

    void Jump(float direction = 0)
    {
        velocityx = direction * 5;
        rb.velocity = new Vector2(velocityx, 7.5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnGround = true;
        sideJumpUsed = false;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        OnGround = false;
    }
}
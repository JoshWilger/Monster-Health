using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 1.0f;

    public float jump_force;
    public float jump_cooldown;
    public float air_multiplier;
    bool ready_jump;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask Ground;
    bool grounded;

    Vector3 movement_direction;
    public Transform orientation;
    Rigidbody rb;
    public float drag = 10f;
    float horizontalInput;
    float verticalInput;

    // Start is called before the first frame update
    void Start()
    {
        ready_jump = true;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, Ground);
        MyInput();
        SpeedControl();

        if (grounded)
        {
            rb.drag = drag;
        }else
        {
            rb.drag = 0;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
        //rb.drag = drag;
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(jumpKey) && ready_jump && grounded)
        {
            ready_jump = false;
            Jump();
            Invoke(nameof(ResetJump), jump_cooldown);
        }
    }

    public void MovePlayer()
    {
        movement_direction = orientation.up * -verticalInput + orientation.right * -horizontalInput;
        if (grounded)
        {
            rb.AddForce(movement_direction.normalized * speed * 10f, ForceMode.Force);
        }

        else if (!grounded)
        {
            rb.AddForce(movement_direction.normalized * speed * 10f * air_multiplier, ForceMode.Force);
        }
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if(flatVel.magnitude > speed)
        {
            Vector3 limitedVel = flatVel.normalized * speed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
    
    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        print("JUMP");
        rb.AddForce(Vector3.up * jump_force, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        ready_jump = true;
    }
}

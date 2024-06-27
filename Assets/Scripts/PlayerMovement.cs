using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float walking_speed = 1.0f;
    float speed = 1.0f;

    public float jump_force;
    public float jump_cooldown;
    public float air_multiplier;
    bool ready_jump;

    [Header("Collisions")]
    public GameObject collision_box;
    public Vector3 regular_size;
    public Vector3 crouch_size;
    public Vector3 crouch_position;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode crouchKey = KeyCode.LeftControl;

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

    bool jump_debounce = false;
    bool crouch_debounce = false;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        speed = walking_speed;
        ready_jump = true;
        rb = GetComponent<Rigidbody>();
        animator = transform.GetChild(0).GetComponent<Animator>();
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
            if (jump_debounce)
            {
                //print("AAA");
                animator.SetTrigger("Grounded");
                Walk();
                jump_debounce = false;
            }
            rb.drag = drag;
        }
        else
        {
            rb.drag = 0;
            jump_debounce = true;
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

        if (Input.GetKeyDown(crouchKey))
        {
            Crouch();
        }

        if (Input.GetKeyUp(crouchKey))
        {
            crouch_debounce = false;
            Walk();
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

        if (flatVel.magnitude > speed)
        {
            Vector3 limitedVel = flatVel.normalized * speed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        animator.SetTrigger("Jump");
        //animator.ResetTrigger("Crouch");
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(Vector3.up * jump_force, ForceMode.Impulse);
        //crouch_debounce = false;
        //jump_debounce = true;
    }

    private void Crouch()
    {
        if (!crouch_debounce)
        {
            animator.SetTrigger("Crouch");
            //animator.ResetTrigger("Jump");
            speed = 0.5f;
            crouch_debounce = true;
            print("CROUCH");
            collision_box.transform.localPosition = crouch_position;
            collision_box.transform.localScale = crouch_size;
        }
    }

    private void Walk()
    {
        //crouch_debounce = false;
       // animator.ResetTrigger("Walk");
        animator.SetTrigger("Walk");
        collision_box.transform.localPosition = Vector3.zero;
        collision_box.transform.localScale = regular_size;
        //animator.ResetTrigger("Jump");
        //animator.ResetTrigger("Crouch");
        //animator.ResetTrigger("Grounded");
        animator.Rebind();
        animator.Update(0f);
        speed = walking_speed;
    }

    private void ResetJump()
    {
        ready_jump = true;
    }
}

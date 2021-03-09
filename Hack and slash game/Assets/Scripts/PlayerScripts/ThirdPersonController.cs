using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    
    public CharacterController characterController;
    public Transform playerCam;
    public float walkSpeed = 6f;
    public float sprintSpeed = 12f;
    private float speed;
    public float turnSmooth = 0.1f;
    float turnSmoothVel;

    Vector3 velocity;
    public float jump = 10f;
    public float fallGravity = -9.8f;
    public float jumpGravity = -15f;
    float gravity;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;

    public Animator anim;

    void Start()
    {
        speed = walkSpeed;
        gravity = fallGravity;
        anim.SetBool("isRunning", false);
        anim.SetBool("attack1", false);
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0.0f, vertical).normalized;
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if(isGrounded && velocity.y > 0)
        {
            isGrounded = false;
        }
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            gravity = fallGravity;
        }
        if(isGrounded && Input.GetAxisRaw("Jump") > .1f)
        {
            velocity.y = jump;
            gravity = jumpGravity;
        }
        if(!isGrounded && velocity.y < 0)
        {
            gravity = fallGravity;
        }
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);

        if (direction.magnitude >= 0.1f)
        {
            anim.SetBool("isRunning", true);
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVel, turnSmooth);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            characterController.Move(moveDirection.normalized * speed * Time.deltaTime);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }


        if(Input.GetAxis("Sprint") >= .1f)
        {
            speed = sprintSpeed;
        }
        else
        {
            speed = walkSpeed;
        }
    }
}

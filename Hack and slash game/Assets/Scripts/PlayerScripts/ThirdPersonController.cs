﻿using System.Collections;
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
    public float jumpSquat = .3f;
    public float landingSquat = .3f;
    public LayerMask groundMask;
    bool isGrounded;
    float stopJumping;
    float stopLanding;
    bool checkLanding;

    public Animator anim;

    public FMODUnity.StudioEventEmitter footsteps;

    void Start()
    {
        checkLanding = true;
        speed = walkSpeed;
        gravity = fallGravity;
        stopJumping = 0.0f;
        stopLanding = 0.0f;
        anim.SetBool("isRunning", false);
        anim.SetBool("isWalking", false);
        anim.SetBool("attack1", false);

        footsteps.Play();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0.0f, vertical).normalized;
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (checkLanding == false && isGrounded == true)
        {
            anim.SetBool("isLanding", true);
            stopLanding = Time.time + landingSquat;
        }
        checkLanding = isGrounded;
        if(isGrounded)
        {
            anim.SetBool("isFalling", false);
        }
        else
        {
            anim.SetBool("isFalling", true);
        }

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
            anim.SetBool("isJumping", true);
            stopJumping = Time.time + jumpSquat;
        }
        if(!isGrounded && velocity.y < 0)
        {
            gravity = fallGravity;
        }

        if (stopLanding <= Time.time)
        {
            anim.SetBool("isLanding", false);
        }
        if (stopJumping <= Time.time)
        {
            anim.SetBool("isJumping", false);
        }
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);

        if (direction.magnitude >= 0.1f)
        {
            anim.SetBool("isWalking", true);
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVel, turnSmooth);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            characterController.Move(moveDirection.normalized * speed * Time.deltaTime);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }


        if(Input.GetAxis("Sprint") >= .1f)
        {
            anim.SetBool("isRunning", true);
            speed = sprintSpeed;
        }
        else
        {
            anim.SetBool("isRunning", false);
            speed = walkSpeed;
        }

        FootstepHandler();
    }

    void FootstepHandler()
    {
        if (anim.GetBool("isWalking") == false && anim.GetBool("isRunning") == false)
        {
            footsteps.SetParameter("Speed", 0);
        }
        else if (speed == walkSpeed)
        {
            footsteps.SetParameter("Speed", 1);
        }
        else if (speed == sprintSpeed)
        {
            footsteps.SetParameter("Speed", 2);
        }
    }

}

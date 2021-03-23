using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SideKickBehavior : MonoBehaviour
{
    public CharacterController characterController;
    public float speed = 6f;
    public Transform characterDistance;
    public float chaseDistance = 200f;
    public float idleDistance = 2f;
    public LayerMask playerLayer;
    public Transform Player;
    public float turnSmooth = 0.1f;
    float turnSmoothVel;

    Vector3 velocity;
    public float fallGravity = -9.8f;
    float gravity;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;

    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        gravity = fallGravity;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
        if(Physics.OverlapSphere(characterDistance.position, idleDistance, playerLayer).Length > 0)
        {
            //Debug.Log("idle distance");
            anim.SetBool("isRunning", false);
        }
        else if (Physics.OverlapSphere(characterDistance.position, chaseDistance, playerLayer).Length > 0)
        {
            anim.SetBool("isRunning", true);
            //Debug.Log("chase distance");
            float horizontal = Player.position.x - characterController.transform.position.x;
            float vertical = Player.position.z - characterController.transform.position.z;
            Vector3 distance = new Vector3(horizontal, 0.0f, vertical);
            Vector3 direction = new Vector3(horizontal, 0.0f, vertical).normalized;
            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVel, turnSmooth);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                characterController.Move(moveDirection.normalized * Mathf.Lerp(speed * .9f, speed * 2, (distance.magnitude -5)/20)  * Time.deltaTime);
            }
        }
        else
        {
            //Debug.Log("warp distance");
            this.transform.position = new Vector3(Player.position.x, Player.position.y, Player.position.z - 3);
        }
    }
}

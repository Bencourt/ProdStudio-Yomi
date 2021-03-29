using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum state
{
    idle,
    chase,
    attack
}
public class EnemyBehavior : MonoBehaviour
{
    public CharacterController characterController;
    public state enemyState;
    public float speed = 6f;
    public Transform characterDistance;
    public float chaseDistance = 20f;
    public float attackDistance = 2f;
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
        enemyState = state.idle;
        gravity = fallGravity;
        anim.SetBool("isIdle", true);
        anim.SetBool("isChase", false);
        anim.SetBool("isAttackWindup", false);
        anim.SetBool("isAttack", false);
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

        if (Physics.OverlapSphere(characterDistance.position, attackDistance, playerLayer).Length > 0)
        {
            //Debug.Log("attack distance");
            enemyState = state.attack;
            anim.SetBool("isIdle", false);
            anim.SetBool("isChase", false);
            anim.SetBool("isAttackWindup", true);
        }
        else if(Physics.OverlapSphere(characterDistance.position, chaseDistance, playerLayer).Length > 0)
        {
            //Debug.Log("chase distance");
            enemyState = state.chase;
            anim.SetBool("isIdle", false);
            anim.SetBool("isChase", true);
            anim.SetBool("isAttackWindup", false);
        }
        else
        {
            //Debug.Log("idle distance");
            enemyState = state.idle;
            anim.SetBool("isIdle", true);
            anim.SetBool("isChase", false);
            anim.SetBool("isAttackWindup", false);
        }

        switch(enemyState){
            case state.idle:
                break;

            case state.chase:
                float horizontal = Player.position.x - characterController.transform.position.x;
                float vertical = Player.position.z - characterController.transform.position.z;
                Vector3 direction = new Vector3(horizontal, 0.0f, vertical).normalized;
                if (direction.magnitude >= 0.1f)
                {
                    float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                    float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVel, turnSmooth);
                    transform.rotation = Quaternion.Euler(0f, angle, 0f);

                    Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                    characterController.Move(moveDirection.normalized * speed * Time.deltaTime);
                }
                break;

            case state.attack:
                break;
        }
    }
}

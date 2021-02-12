using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_combat : MonoBehaviour
{
    public CharacterController characterController;
    public Transform enemyCheck;
    public float attackRange = 1;
    public LayerMask enemyLayer;
    public int damage = 30;
    public int knockback = 30;
    private bool attacked;

    public float attackRate = 2f;
    float attackTime = 0.0f;

    public Vector3 knockbackDirection;

    public int maxHealth = 100;
    int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        attacked = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (knockbackDirection.magnitude > .1f)
        {
            characterController.Move(knockbackDirection * Time.deltaTime);
        }
        knockbackDirection = Vector3.Lerp(knockbackDirection, Vector3.zero, Time.deltaTime * 2f);

        if (Time.time >= attackTime)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                //Debug.Log("Attacked");
                Attack();
                attackTime = Time.time + 1f / attackRate;
            }
        }
        
    }

    void Attack()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(enemyCheck.position, attackRange, enemyLayer);

        foreach (Collider enemy in hitEnemies)
        {
            //Debug.Log("hit enemy name:" + enemy.name);
            enemy.GetComponent<enemy>().TakeDamage(damage);
            enemy.GetComponent<enemy>().TakeKnockback(knockback, transform);
        }
    }

    public void TakeDamage(int danage)
    {
        currentHealth -= danage;

        if (currentHealth <= 0)
        {
            die();
        }
    }

    public void TakeKnockback(int strenght, Transform source)
    {
        float horizontal = characterController.transform.position.x - source.position.x;
        float vertical = characterController.transform.position.z - source.position.z;
        Vector3 direction = new Vector3(horizontal, 0.0f, vertical).normalized;
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            knockbackDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward * strenght;
            Debug.Log("Enemy Knocked Back");
            Debug.Log(knockbackDirection.magnitude.ToString());

        }
    }

    void die()
    {
        //Debug.Log("Player died!");
        GetComponent<CharacterController>().enabled = false;
        GetComponent<third_person_controller>().enabled = false;
        this.enabled = false;
    }

    void OnDrawGizmosSelected()
    {
        if (enemyCheck == null)
            return;
        Gizmos.DrawWireSphere(enemyCheck.position, attackRange);
    }
}

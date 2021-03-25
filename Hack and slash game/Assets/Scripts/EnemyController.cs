using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    public int maxHealth = 100;
    public float currentHealth;
    public CharacterController characterController;
    public PlayerCombat playerCombat;
    public Vector3 knockbackDirection;

    public Transform playerCheck;
    public float attackRange = 1;
    public LayerMask playerLayer;
    public int damage = 10;
    public int knockback = 10;
    private bool attacked;


    public float attackWindup = 1f;
    float attackWindupTime = 0.0f;
    public float attackRate = 2f;
    float attackTime = 0.0f;

    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (knockbackDirection.magnitude > .1f)
        {
            characterController.Move(knockbackDirection * Time.deltaTime);
        }
        knockbackDirection = Vector3.Lerp(knockbackDirection, Vector3.zero, Time.deltaTime * 2f);

        if (GetComponent<EnemyBehavior>().enemyState == state.attack && !attacked && Time.time >= attackWindupTime)
        {
            Debug.Log("Attacked");
            attacked = true;
            Attack();
            attackTime = Time.time + 1f / attackRate;
        }
        if (GetComponent<EnemyBehavior>().enemyState == state.attack && Time.time >= attackTime && attacked)
        {
            Debug.Log("attacking");
            attacked = false;
            attackWindupTime = Time.time + 1f / attackWindup;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Attack()
    {
        Collider[] hitPlayer = Physics.OverlapSphere(playerCheck.position, attackRange, playerLayer);

        foreach (Collider player in hitPlayer)
        {
            Debug.Log("hit player name:" + player.name);
            playerCombat.TakeDamage(damage);
            playerCombat.TakeKnockback(damage, transform);
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

    public void TakeDamage(int damageValue)
    {
        currentHealth -= damageValue;
        if (GetComponent<EnemyBehavior>().enemyState == state.attack)
        {
            attackTime = Time.time + 1f / attackRate;
        }
    }

    void Die()
    {
        //Debug.Log("enemy died!");
        GetComponent<CharacterController>().enabled = false;
        GetComponent<EnemyBehavior>().enabled = false;
        //this.enabled = false;
        //Debug.Log(playerCombat.GetComponent<PlayerCombat>().enemies);
        playerCombat.GetComponent<PlayerCombat>().enemies.Remove(this);
        Destroy(this);
    }

    void OnDrawGizmosSelected()
    {
        if (playerCheck == null)
            return;
        Gizmos.DrawWireSphere(playerCheck.position, attackRange);
    }

    public float GetCurrentHealth()
    {
        if (currentHealth / 100 < 0)
        {
            return 0;
        }
        return currentHealth / 100;
    }
}

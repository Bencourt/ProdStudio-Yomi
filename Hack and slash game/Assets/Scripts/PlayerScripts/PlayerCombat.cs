using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PlayerCombat : MonoBehaviour
{
    public CharacterController characterController;
    public Transform enemyCheck;
    public float attackRange = 1;
    public LayerMask enemyLayer;
    public List<Enemy> enemies = new List<Enemy>();
    public int damage = 30;
    public int knockback = 30;
    private bool attacked;
    public GameObject enemyUI;

    public float attackRate = 2f;
    float attackTime = 0.0f;

    public Vector3 knockbackDirection;

    public int maxHealth = 100;
    float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        attacked = false;

        GameObject[] enemiesArray = GameObject.FindGameObjectsWithTag("Enemies");
        //Debug.Log(enemiesArray.Length);
        for(int i = 0; i < enemiesArray.Length; i++)
        {
            enemies.Add(enemiesArray[i].gameObject.GetComponent<Enemy>());
            //Debug.Log(enemies[0].name);
        }

        enemyUI = GameObject.FindGameObjectWithTag("EnemyUI");
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
        PlayerHealthBarHandler.SetHealthBarValue(currentHealth / 100); // Changes health bar in HealthBarHandler Script
        GetClosestEnemy(enemies);
    }

    void Attack()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(enemyCheck.position, attackRange, enemyLayer);

        foreach (Collider enemy in hitEnemies)
        {
            //Debug.Log("hit enemy name:" + enemy.name);
            enemy.GetComponent<Enemy>().TakeDamage(damage);
            enemy.GetComponent<Enemy>().TakeKnockback(knockback, transform);
        }
    }

    public void TakeDamage(int damageValue)
    {
        currentHealth -= damageValue;

        if (currentHealth <= 0)
        {
            Die();
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

    void Die()
    {
        //Debug.Log("Player died!");
        GetComponent<CharacterController>().enabled = false;
        GetComponent<ThirdPersonController>().enabled = false;
        this.enabled = false;
    }

    void OnDrawGizmosSelected()
    {
        if (enemyCheck == null)
            return;
        Gizmos.DrawWireSphere(enemyCheck.position, attackRange);
    }

    void GetClosestEnemy(List<Enemy> enemiesList)
    {
        Enemy closestTarget = null;
        float closestDistSqr = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach(Enemy enemy in enemiesList)
        {
            Vector3 directionToTarget = enemy.transform.position - currentPos;
            float distSqrToTarget = directionToTarget.sqrMagnitude;
            if(distSqrToTarget < closestDistSqr)
            {
                closestDistSqr = distSqrToTarget;
                closestTarget = enemy;
            }
        }
        if (closestDistSqr > 175)
        {
            enemyUI.SetActive(false);
        }
        else
        {
            enemyUI.SetActive(true);
        }
        EnemyHealthBarHandler.SetHealthBarValue(closestTarget.currentHealth/100);
    }
}

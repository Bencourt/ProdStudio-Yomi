using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerCombat : MonoBehaviour
{
    public CharacterController characterController;
    public CapsuleCollider enemyCheck;
    public float attackRange = 1;
    public LayerMask enemyLayer;
    public List<EnemyController> enemies = new List<EnemyController>();
    private GameObject[] enemiesArray;
    public int damage = 30;
    public int damage2 = 25;
    public int damage3 = 40;
    public int knockback1 = 15;
    public int knockback2 = 40;
    public bool attacked;
    public bool attacked2;
    public bool attacked3;

    public GameObject enemyUI;
    public GameObject inGameUI;

    public float attackRate = 2f;
    float attackTime = 0.0f;
    float attackTime2 = 0.0f;
    float attackTime3 = 0.0f;

    public Vector3 knockbackDirection;

    public int maxHealth = 100;
    public float currentHealth;

    public Animator anim;

    void Start()
    {
        currentHealth = maxHealth;
        attacked = false;
        attacked2 = false;
        attacked3 = false;

        enemiesArray = GameObject.FindGameObjectsWithTag("Enemies");
        for(int i = 0; i < enemiesArray.Length; i++)
        {
            enemies.Add(enemiesArray[i].gameObject.GetComponent<EnemyController>());
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (knockbackDirection.magnitude > .1f)
        {
            characterController.Move(knockbackDirection * Time.deltaTime);
        }
        knockbackDirection = Vector3.Lerp(knockbackDirection, Vector3.zero, Time.deltaTime * 2f);
        if(Time.time >= attackTime && Time.time >= attackTime2 && Time.time >= attackTime3)
        {
            attacked = false;
            attacked2 = false;
            attacked3 = false;
            anim.SetBool("attack1", false);
            anim.SetBool("attack2", false);
            anim.SetBool("attack3", false);

        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (Time.time >= attackTime && !attacked)
            {
                Debug.Log("Attacked");
                attackTime = Time.time + 1f / attackRate;
                anim.SetBool("attack1", true);
                attacked = true;
            }
            else if(Time.time < attackTime && Time.time >= attackTime2 && attacked && !attacked2)
            {
                anim.SetBool("attack2", true);
                Debug.Log("Attacked2");
                attacked2 = true;
                attackTime2 = Time.time + 1f / attackRate;
            }
            else if (Time.time < attackTime2 && Time.time >= attackTime3 && attacked2)
            {
                Debug.Log("Attacked3");
                anim.SetBool("attack3", true);
                attacked3 = true;
                attackTime3 = Time.time + 1f / attackRate;
            }
        }
        GetClosestEnemy(enemies);
    }

    public void Attack(GameObject hitCollider)
    {
        foreach (GameObject e in enemiesArray)
        {
            if (e == hitCollider)
            {
                if (!attacked && !attacked2)
                {
                    //Debug.Log("hit enemy name:" + enemy.name);
                    hitCollider.GetComponent<EnemyController>().TakeDamage(damage);
                    hitCollider.GetComponent<EnemyController>().TakeKnockback(knockback1, transform);
                }
                else if (attacked && !attacked2)
                {
                    //Debug.Log("hit enemy name:" + enemy.name);
                    hitCollider.GetComponent<EnemyController>().TakeDamage(damage2);
                    hitCollider.GetComponent<EnemyController>().TakeKnockback(knockback1, transform);
                }
                else if (attacked && attacked2)
                {
                    //Debug.Log("hit enemy name:" + enemy.name);
                    hitCollider.GetComponent<EnemyController>().TakeDamage(damage3);
                    hitCollider.GetComponent<EnemyController>().TakeKnockback(knockback2, transform);
                }
            }
        }
    }

    public void TakeDamage(int damageValue)
    {
        currentHealth -= damageValue;
        PlayerHealthBarHandler.SetHealthBarValue(currentHealth / 100); // Changes health bar in HealthBarHandler Script
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void HealDamage(int potionValue)
    {
        currentHealth += potionValue;

        if (currentHealth >= 100)
        {
            currentHealth = 100;
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
        SceneManager.LoadScene("SampleScene");
        this.enabled = false;
    }

    void GetClosestEnemy(List<EnemyController> enemiesList)
    {
        EnemyController closestTarget = null;
        float closestDistSqr = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach(EnemyController enemy in enemiesList)
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
        if (enemies.Count > 0)
        {
            EnemyHealthBarHandler.SetHealthBarValue(closestTarget.currentHealth / 100);
        }
    }
}

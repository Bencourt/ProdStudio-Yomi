using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBarHandler : MonoBehaviour
{
    private static Image healthBarImage;
    public static Enemy target;

    /// <summary>
    /// Sets the health bar value
    /// </summary>
    /// <param name="value">should be between 0 to 1</param>
    public static void SetHealthBarValue(float value)
    {
        healthBarImage.fillAmount = value;
    }

    public static float GetHealthBarValue()
    {
        return healthBarImage.fillAmount;
    }

    public static void SetTarget(Enemy targetEnemy)
    {
        target = targetEnemy;
    }

    public static Enemy GetTarget()
    {
        return target;
    }

    /// <summary>
    /// Initialize the variable
    /// </summary>
    private void Start()
    {
        healthBarImage = GetComponent<Image>();
    }
}
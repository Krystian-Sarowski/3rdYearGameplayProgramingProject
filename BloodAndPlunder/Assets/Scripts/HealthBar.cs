using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    float maxHealth = 0;
    float currentHealth = 0;

    public void SetMaxHealth(float t_maxHealth)
    {
        maxHealth = t_maxHealth;
        currentHealth = maxHealth;
    }

    public void SetCurrentHealth(float t_currentHealth)
    {
        currentHealth = t_currentHealth;

        transform.localScale = new Vector3(currentHealth / maxHealth * 4.0f, 0.5f, 1.0f);
    }
}

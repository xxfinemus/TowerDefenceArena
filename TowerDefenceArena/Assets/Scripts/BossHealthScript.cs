using UnityEngine;
using System.Collections;

public class BossHealthScript : MonoBehaviour
{
    float maxHealth;

    float currentHealth;

    public float MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }

    public float CurrentHealth
    {
        get { return currentHealth; }
        set { currentHealth = value; }
    }

    HealthBarScript healthBar;

    // Use this for initialization
    void Start()
    {
        healthBar = GetComponentInChildren<HealthBarScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth > 0)
        {
            healthBar.SetSize(currentHealth / maxHealth);
        }
        else
            healthBar.SetSize(0);


        if (currentHealth <= 0)
        {
            OnDeath();
        }
    }

    void OnDeath()
    {

    }
}

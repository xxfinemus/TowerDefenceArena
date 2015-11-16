using UnityEngine;
using System.Collections;

public class PlayerHealthScript : MonoBehaviour
{
    HealthBarScript healthBar;

    [SerializeField]
    float maxHealth;

    [SerializeField]
    float currentHealth;

    [SerializeField]
    float invulnerabilityTime;

    float timer;

    // Use this for initialization
    void Start()
    {
        currentHealth = maxHealth;
        healthBar = GetComponentInChildren<HealthBarScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= 0)
        {
            timer -= Time.deltaTime;
        }
    }

    public void TakeDamage(float damage)
    {
        if (timer <= 0)
        {
            currentHealth -= damage;
            healthBar.SetSize(currentHealth / maxHealth);
            timer = invulnerabilityTime;
        }
    }
}

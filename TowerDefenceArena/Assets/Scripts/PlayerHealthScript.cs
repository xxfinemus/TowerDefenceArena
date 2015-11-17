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
        healthBar = GetComponentInChildren<HealthBarScript>();
        Begin();
    }

    public void Begin()
    {
        GetComponent<PlayerAttackScript>().Damage = StatScript.Instance.Strength;
        GetComponent<PlayerMovementScript>().Speed = StatScript.Instance.Speed;
        maxHealth = 100 + (StatScript.Instance.Vitality * 10);
        currentHealth = maxHealth;
        healthBar.SetSize(1);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
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

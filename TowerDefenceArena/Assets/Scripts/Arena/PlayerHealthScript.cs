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

    private Animator characterAnimator;

    // Use this for initialization
    void Start()
    {
        characterAnimator = GetComponentInChildren<Animator>();
        healthBar = GetComponentInChildren<HealthBarScript>();
        Begin();
    }

    public void Begin()
    {
        GetComponentInChildren<PlayerAttackScript>().Damage = StatScript.Instance.Strength * 10;
        GetComponent<PlayerMovementScript>().Speed = StatScript.Instance.Speed * 10;
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

            characterAnimator.SetTrigger("hit");
        }
    }
}

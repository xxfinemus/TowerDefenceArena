using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyHealthScript : MonoBehaviour
{
    [SerializeField]
    private float maxHealth;

    [SerializeField]
    private AudioClip impactSound;

    private float currentHealth;

    private ChangeMeshOnAnimation changeMeshScript;

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
        currentHealth = maxHealth;

        changeMeshScript = GetComponent<ChangeMeshOnAnimation>();

        healthBar = GetComponentInChildren<HealthBarScript>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "exit")
        {
            //add to boss
            WaveControl.EnemiesRemaning--;
            StatScript.Instance.ChangeStat("bossHealth", (int)currentHealth);
            gameObject.SetActive(false);
        }
    }

    public void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.SetSize(currentHealth / maxHealth);
        }
    }

    /// <summary>
    /// Takes a float and reduces health by it and updates the size of the healthbar
    /// </summary>
    /// <param name="dmg"></param>
    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;

        if (currentHealth <= 0)
        {
            currentHealth = 0;

            if (!changeMeshScript.DeathModel.activeSelf)
            {
                changeMeshScript.ChangeModel();
            }
        }

        UpdateHealthBar();

        PlayImpactSound();

        //if (currenthealth <= 0)
        //{
        //    StatScript.Instance.ChangeStat("gold", (int)(maxHealth / 50));
        //    StatScript.Instance.ChangeStat("exp", (int)(maxHealth / 10));
        //    WaveControl.EnemiesRemaning--;

        //    gameObject.SetActive(false);
        //}
    }

    private void PlayImpactSound()
    {
        GetComponent<AudioSource>().PlayOneShot(impactSound);
    }

    public void OnDeath()
    {
        StatScript.Instance.ChangeStat("gold", (int)(maxHealth / 50));
        StatScript.Instance.ChangeStat("exp", (int)(maxHealth / 10));
        WaveControl.EnemiesRemaning--;

        gameObject.SetActive(false);
        GetComponent<EnemyHealthScript>().currentHealth = maxHealth;
        changeMeshScript.ChangeModel();
    }
}

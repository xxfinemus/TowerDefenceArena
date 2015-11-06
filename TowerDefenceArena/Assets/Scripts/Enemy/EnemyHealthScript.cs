using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyHealthScript : MonoBehaviour
{
    [SerializeField]
    float maxHealth;

    float currenthealth;

    public float MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }

    public float Currenthealth
    {
        get { return currenthealth; }
        set { currenthealth = value; }
    }

    HealthBarScript healthBar;

    // Use this for initialization
    void Start()
    {
        currenthealth = maxHealth;

        healthBar = GetComponentInChildren<HealthBarScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "exit")
        {
            //add to boss
            WaveControl.EnemiesRemaning--;
            StatScript.Instance.ChangeStat("bossHealth", (int)currenthealth);
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Takes a float and reduces health by it and updates the size of the healthbar
    /// </summary>
    /// <param name="dmg"></param>
    public void TakeDamage(float dmg)
    {
        currenthealth -= dmg;

        if (healthBar != null)
        {
            healthBar.SetSize(currenthealth / maxHealth);
        }
        if (currenthealth <= 0)
        {
            StatScript.Instance.ChangeStat("gold", (int)(maxHealth / 50));
            StatScript.Instance.ChangeStat("exp", (int)(maxHealth / 10));
            WaveControl.EnemiesRemaning--;
            gameObject.SetActive(false);
        }
    }
}

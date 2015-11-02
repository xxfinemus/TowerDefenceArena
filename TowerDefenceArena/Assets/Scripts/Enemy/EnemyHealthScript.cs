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

    // Use this for initialization
    void Start()
    {
        currenthealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "bullet")
        {
            
            //Insert code to get the bullets script and get the damage then call the TakeDamage function
        }
    }

    void TakeDamage(float dmg)
    {
        currenthealth -= dmg;
    }
}

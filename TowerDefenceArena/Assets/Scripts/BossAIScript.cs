using UnityEngine;
using System.Collections;

public class BossAIScript : MonoBehaviour
{
    [SerializeField]
    GameObject target;

    public GameObject Target
    {
        get { return target; }
        set { target = value; }
    }

    [SerializeField]
    float attackSpeed;
    [SerializeField]
    float attackRange;
    [SerializeField]
    float damage;
    [SerializeField]
    float health;

    float cooldown;
    BossNavigationScript navScript;
    // Use this for initialization
    void Start()
    {
        navScript = GetComponent<BossNavigationScript>();

        cooldown = attackSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (!InRange(transform.position, target.transform.position, attackRange))
        {
            navScript.SetTarget(target);
        }
        else if (cooldown <= 0)
        {
            //placeholder code until attacks are in place
            int rnd = Random.Range(1, 2);
            if (rnd == 1)
            {
                Attack();
            }
            else
                SpecialAttack();
        }

        if (cooldown >= 0)
        {
            cooldown -= Time.deltaTime;
        }
    }

    bool InRange(Vector3 a, Vector3 b, float range)
    {
        if (Vector3.Distance(a, b) <= range)
        {
            return true;
        }
        else return false;
    }

    void Attack()
    {
        cooldown = attackSpeed;
    }

    void SpecialAttack()
    {
        cooldown = attackSpeed;
    }
}

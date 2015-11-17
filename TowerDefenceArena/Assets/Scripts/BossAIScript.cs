using UnityEngine;
using System.Collections;

public class BossAIScript : MonoBehaviour
{
    [SerializeField]
    private GameObject target;

    public GameObject Target
    {
        get { return target; }
        set { target = value; }
    }

    private bool attacking;

    public bool Attacking
    {
        get { return attacking; }
        set { attacking = value; }
    }

    [SerializeField]
    private float attackSpeed;
    [SerializeField]
    private float attackRange;
    [SerializeField]
    private float damage;
    [SerializeField]
    private float health;

    private float cooldown;

    private BossNavigationScript navScript;

    private BossAttack attackScript;
    // Use this for initialization
    void Start()
    {
        navScript = GetComponent<BossNavigationScript>();

        attackScript = GetComponent<BossAttack>();

        attackScript.Damage = damage;

        cooldown = attackSpeed;
    }

    public void Begin()
    {
        GetComponent<BossHealthScript>().Begin(StatScript.Instance.BossHealth * 10);
        GetComponent<BossAttack>().Damage = StatScript.Instance.EnemiesLeaked * 10;
    }

    // Update is called once per frame
    void Update()
    {

        if (!InRange(transform.position, target.transform.position, attackRange))
        {
            navScript.StartChasing();
            navScript.SetTarget(target);
        }
        else if (cooldown <= 0)
        {
            navScript.StopChasing();
            Attack();
        }

        if (cooldown >= 0)
        {
            cooldown -= Time.deltaTime;
        }

    }

    private bool InRange(Vector3 a, Vector3 b, float range)
    {
        return (Vector3.Distance(a, b) <= range);
    }

    private void Attack()
    {
        if (Random.value > 0.5f)
        {
            attackScript.MeleeAttack();
        }
        else
            attackScript.SpecialAttack1();
    }

    public void BeginFight()
    {
        
    }
}

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

    float timer;
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
        transform.parent.position = new Vector3(0, 0, -10);
        GetComponent<BossHealthScript>().Begin(StatScript.Instance.BossHealth * 10);
        GetComponent<BossAttack>().Damage = StatScript.Instance.EnemiesLeaked * 1.5f;
        timer = 2;
        navScript.StopChasing();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0)
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
        timer -= Time.deltaTime;
    }

    private bool InRange(Vector3 a, Vector3 b, float range)
    {
        return (Vector3.Distance(a, b) <= range);
    }

    private void Attack()
    {
        //if (Random.value > 0.5f)
        //{
        attackScript.MeleeAttack();
        //}
        //else
        //    attackScript.SpecialAttack1();
    }

    public void BeginFight()
    {

    }
}

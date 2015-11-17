using UnityEngine;
using System.Collections;

public class PlayerAttackScript : MonoBehaviour
{
    [SerializeField]
    float damage;
    [SerializeField]
    float attackSpeed;
    float cooldown;

    private Animator characterAnimator;
    [SerializeField]
    private BoxCollider meleeCollider;

    public float Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    // Use this for initialization
    void Start()
    {
        characterAnimator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
    }

    void Attack()
    {
        if (cooldown <= 0)
        {
            //Insert code to attack here
            characterAnimator.SetTrigger("attack");

            cooldown = attackSpeed;
        }
    }

    public void ToggleMeleeCollider()
    {
        meleeCollider.enabled = !meleeCollider.enabled;
    }
}

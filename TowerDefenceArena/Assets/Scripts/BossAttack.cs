using UnityEngine;
using System.Collections;

public class BossAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject meleeCollider;

    private Animator bossAnimator;

    // Use this for initialization
    void Start()
    {
        bossAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Toggles the melee attack collider on the boss. This methode should be called in an animation event.
    /// </summary>
    public void ToggleMeleeCollider()
    {
        if (meleeCollider.activeSelf)
        {
            meleeCollider.SetActive(false);
        }
        else
        {
            meleeCollider.SetActive(true);
        }
    }

    /// <summary>
    /// This methode should be used when the boss should preform a melee attack. 
    /// </summary>
    public void MeleeAttack()
    {
        bossAnimator.SetTrigger("meleeAttack");
    }

    /// <summary>
    /// This methode should be used when the boss should preform special attack 1.
    /// </summary>
    public void SpecialAttack1()
    {
        bossAnimator.SetTrigger("specialAttack1");
    }
}

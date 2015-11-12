using UnityEngine;
using System.Collections;

public class BossAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject meleeCollider;

    private Animator bossAnimator;
    private bool specialAttackInProgress = false;

    // Use this for initialization
    void Start()
    {
        bossAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {

        LogicSpecialAttack1();

        if (specialAttackInProgress)
        {

        }

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
    /// This methode is heaviely inspiered by "alucardj's" answer to the folowing post:
    /// http://answers.unity3d.com/questions/362763/raycast-over-arc.html
    /// </summary>
    public void LogicSpecialAttack1()
    {
        float distance = 5;
        int angle = 360;

        int segments = 45;
        int angleStep = angle / segments;
        Vector3 defaultPos = transform.position + new Vector3(0, 0.2f, 0);
        Vector3 tagetPos = Vector3.zero;

        RaycastHit hit;

        for (int i = 0; i <= 360; i += 2)
        {
            tagetPos = (Quaternion.Euler(0, i, 0) * transform.up).normalized * distance;
            tagetPos += new Vector3(0, 0.2f, 0);

            Debug.DrawLine(defaultPos, tagetPos, Color.green);
        }

        if (Physics.Linecast(defaultPos, tagetPos, out hit))
        {

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

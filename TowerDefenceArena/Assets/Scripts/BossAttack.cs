using UnityEngine;
using System.Collections;

public class BossAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject meleeCollider;

    [SerializeField]
    private float specialAttackSpeed = 100;

    [SerializeField]
    private GameObject fireSystem;

    private Animator bossAnimator;

    private bool specialAttackInProgress = false;
    private float specialAttack1Range = 0;
    private float specialAttack1MaxRange = 50;

    // Use this for initialization
    void Start()
    {
        bossAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            specialAttackInProgress = true;
        }
    }

    void FixedUpdate()
    {
        if (specialAttackInProgress)
        {
            if (specialAttack1Range <= specialAttack1MaxRange)
            {
                specialAttack1Range += 1 * (specialAttackSpeed / 1000);

                LogicSpecialAttack1(specialAttack1Range);
            }
            else
            {
                specialAttack1Range = 0;
                specialAttackInProgress = false;
            }  
        }

    }

    public void ToggleSpecialAttack1()
    {
        specialAttackInProgress = !specialAttackInProgress;
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
    public void LogicSpecialAttack1(float distance)
    {
        int angle = 360;

        int segments = 45;
        int angleStep = angle / segments;
        Vector3 defaultPos = transform.position  + new Vector3(0, 0.2f, 0);
        Vector3 tagetPos = defaultPos;

        RaycastHit hit;

        for (int i = 0; i <= 360; i += 2)
        {
            tagetPos = ((Quaternion.Euler(0, i, 0) * transform.forward).normalized * distance) + defaultPos;

            if (Physics.Raycast(defaultPos, tagetPos, out hit, distance))
            {
                if (hit.collider.gameObject.tag == "Player")
                {
                    // Player was hit
                    Debug.Log("The player was hit");
                }
            }

            Debug.DrawRay(defaultPos, tagetPos, Color.green);
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

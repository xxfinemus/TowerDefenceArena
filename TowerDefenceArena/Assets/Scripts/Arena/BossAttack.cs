using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BossAttack : MonoBehaviour
{
    [SerializeField]
    float damage;


    [SerializeField]
    private GameObject meleeCollider;

    [SerializeField]
    private float specialAttackSpeed = 100;

    [SerializeField]
    private GameObject fireSystem;

    private GameObject[] fireSystems;


    [SerializeField]
    private List<Vector3> tagetPositions;

    [SerializeField]
    private float[] distances;

    private Animator bossAnimator;

    private bool specialAttackInProgress = false;
    private float specialAttack1Range = 0;
    private float specialAttack1MaxRange = 50;
    public float Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    // Use this for initialization
    void Start()
    {
        bossAnimator = GetComponent<Animator>();
        tagetPositions = new List<Vector3>();
        distances = new float[181];

        fireSystems = new GameObject[180];

        for (int i = 0; i < 180; i++)
        {
            fireSystems[i] = (GameObject)Instantiate(fireSystem, Vector3.zero, Quaternion.identity);
        }
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

                //if (fireSystems == null)
                //{
                //    fireSystems = new GameObject[180];

                //    for (int i = 0; i < tagetPositions.Count - 1; i++)
                //    {
                //        fireSystems[i] = (GameObject)Instantiate(fireSystem, tagetPositions[i], Quaternion.identity);
                //    }
                //}
                //else
                //{
                for (int i = 0; i < tagetPositions.Count - 1; i++)
                {
                    if (!fireSystems[i].activeSelf)
                    {
                        fireSystems[i].SetActive(true);
                    }

                    fireSystems[i].transform.position = tagetPositions[i];
                }
                //}
            }
            else
            {
                specialAttack1Range = 0;
                specialAttackInProgress = false;

                foreach (var system in fireSystems)
                {
                    system.SetActive(false);
                }
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
        Vector3 defaultPos = transform.position + new Vector3(0, 0.2f, 0);
        Vector3 tagetPos = Vector3.zero;

        RaycastHit hit;

        tagetPositions.Clear();

        for (int i = 0; i <= 360; i += 2)
        {

            if (Physics.Raycast(defaultPos, tagetPos, out hit, distance))
            {
                if (hit.collider.transform.parent.tag == "Player")
                {
                    hit.collider.gameObject.GetComponentInParent<PlayerHealthScript>().TakeDamage(damage);
                    distances[i / 2] = Vector3.Distance(defaultPos, hit.point);
                }

                if (hit.collider.gameObject.tag == "wall")
                {
                    distances[i / 2] = Vector3.Distance(defaultPos, hit.point);
                }
            }

            if (distances[i / 2] == 0)
            {
                tagetPos = ((Quaternion.Euler(0, i, 0) * transform.forward).normalized * distance);
            }
            else
            {
                tagetPos = ((Quaternion.Euler(0, i, 0) * transform.forward).normalized * distances[i / 2]);
            }

            tagetPositions.Add(tagetPos + defaultPos);

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

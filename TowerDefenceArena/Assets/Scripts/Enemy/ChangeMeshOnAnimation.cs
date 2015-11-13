using UnityEngine;
using System.Collections;

public class ChangeMeshOnAnimation : MonoBehaviour
{
    [SerializeField]
    private GameObject walkingModel;

    [SerializeField]
    private GameObject deathModel;

    private float health;

    public float Health
    {
        get { return health; }
        set { health = value; }
    }


    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        health = GetComponent<EnemyHealthScript>().Currenthealth;

        if (Input.GetKeyDown(KeyCode.H))
        {
            health = 0;
        }

        if (health <= 0)
        {
            ChangeModel();
        }
    }

    public void ChangeModel()
    {
        walkingModel.SetActive(false);
        deathModel.SetActive(true);

        deathModel.GetComponent<Animator>().SetTrigger("die");
    }
}

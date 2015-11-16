using UnityEngine;
using System.Collections;

public class ChangeMeshOnAnimation : MonoBehaviour
{
    [SerializeField]
    private GameObject walkingModel;

    [SerializeField]
    private GameObject deathModel;

    private Animator deathAnimator;

    public GameObject DeathModel
    {
        get { return deathModel; }
    }

    public GameObject WalkingModel
    {
        get { return walkingModel; }
    }

    private void Start()
    {
        deathAnimator = deathModel.GetComponent<Animator>();
    }

    public void ChangeModel()
    {
        if (walkingModel.activeSelf)
        {
            walkingModel.SetActive(false);
            deathModel.SetActive(true);

            deathAnimator.SetTrigger("die");
            GetComponent<NavMeshAgent>().enabled = false;
        }
        else
        {
            walkingModel.SetActive(true);
            deathModel.SetActive(false);

            deathAnimator.SetTrigger("reset");
            GetComponent<NavMeshAgent>().enabled = true;
        }
    }
}

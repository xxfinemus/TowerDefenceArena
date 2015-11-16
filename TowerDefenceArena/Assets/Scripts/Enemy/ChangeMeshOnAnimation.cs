using UnityEngine;
using System.Collections;

public class ChangeMeshOnAnimation : MonoBehaviour
{
    [SerializeField]
    private GameObject walkingModel;

    [SerializeField]
    private GameObject deathModel;

    public GameObject DeathModel
    {
        get { return deathModel; }
    }

    public GameObject WalkingModel
    {
        get { return walkingModel; }
    }

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeModel()
    {
        if (walkingModel.activeSelf)
        {
            walkingModel.SetActive(false);
            deathModel.SetActive(true);

            deathModel.GetComponent<Animator>().SetTrigger("die");
        }
        else
        {
            walkingModel.SetActive(true);
            deathModel.SetActive(false);

            deathModel.GetComponent<Animator>().SetTrigger("reset");
        }
    }
}

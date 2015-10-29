using UnityEngine;
using System.Collections;

public class EntranceScript : MonoBehaviour {

    
    private NavMeshAgent navMeshAgent;
    [SerializeField]
    private GameObject endNode;
    public GameObject Checker;



    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.Stop();
    }

    public bool CheckIfPathIsValid(Vector3 pos)
    {
        Checker.transform.position = pos;
        
        NavMeshPath path = new NavMeshPath();
        navMeshAgent.CalculatePath(endNode.transform.position, path);
        bool test = path.status == NavMeshPathStatus.PathPartial;
        Debug.Log(path.status);
        Debug.Log(test);
        //Checker.transform.position = new Vector3(-12,0,12);
        return test;
    }
}

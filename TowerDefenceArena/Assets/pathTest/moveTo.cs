using UnityEngine;
using System.Collections;

public class moveTo : MonoBehaviour
{

    // MoveTo.cs
    public Transform goal1;
    public Transform goal2;
    NavMeshAgent agent;
    public bool canFindPath;
    

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = goal1.position;
    }

    void Update()
    {
        if (!IsPathFree())
        {
            agent.Stop();
            
        }
        if(IsPathFree())
        {
            agent.Resume();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            agent.destination = goal1.position;
            
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            agent.destination = goal2.position;
        }
        
    }
    public bool IsPathFree()
    {
        return agent.pathStatus != NavMeshPathStatus.PathPartial;
    }

}

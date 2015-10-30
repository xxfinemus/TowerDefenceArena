using UnityEngine;
using System.Collections;

public class MoveTo : MonoBehaviour
{

    // MoveTo.cs
    public Transform startObj;
    public Transform endObj;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = startObj.position;
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
            agent.destination = startObj.position;
            
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            agent.destination = endObj.position;
        }
        
    }
    public bool IsPathFree()
    {
        return agent.pathStatus != NavMeshPathStatus.PathPartial;
    }

}

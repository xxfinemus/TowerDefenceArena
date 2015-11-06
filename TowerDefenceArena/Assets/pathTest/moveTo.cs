using UnityEngine;
using System.Collections;

public class moveTo : MonoBehaviour
{
    NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Transform goal = GameObject.Find("Exit").transform;
        agent.destination = goal.position;
    }

    void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        Transform goal = GameObject.Find("Exit").transform;
        agent.destination = goal.position;
    }
}

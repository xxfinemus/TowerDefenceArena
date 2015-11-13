using UnityEngine;
using System.Collections;

public class BossNavigationScript : MonoBehaviour
{
    public NavMeshAgent navAgent;

    // Use this for initialization
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetTarget(GameObject target)
    {
        navAgent.SetDestination(target.transform.position);
    }

    public void StopChasing()
    {
        navAgent.Stop();
        Debug.Log("Boss Stopped chasing the Hero!");
    }
    public void StartChasing()
    {
        navAgent.Resume();
    }
}

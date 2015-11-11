using UnityEngine;
using System.Collections;

public class BossNavigationScript : MonoBehaviour
{
    NavMeshAgent navAgent;

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
}

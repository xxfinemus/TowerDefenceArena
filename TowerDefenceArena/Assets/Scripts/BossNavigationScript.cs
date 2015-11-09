using UnityEngine;
using System.Collections;

public class BossNavigationScript : MonoBehaviour
{
    NavMeshAgent navAgent;

    [SerializeField]
    GameObject target;

    // Use this for initialization
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MoveToTarget()
    {
        navAgent.destination = target.transform.position;
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerBehavior : MonoBehaviour {

    [SerializeField]
    private string name;
    [SerializeField]
    private BulletObjectPoolScript objectPool;
    [SerializeField]
    private float range;
    [SerializeField]
    private float rateOfFire;
    [SerializeField]
    private Queue<GameObject> enemies;
    [SerializeField]
    private GameObject target;
    public string Name
    {
        get { return name; }
    }
	// Use this for initialization
	void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.LookAt(target.transform);
	}
    private void AddToQueue()
    {
        GameObject[] _enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in _enemies)
        {
            if (!enemies.Contains(enemy))
            {
                if (Vector3.Distance(transform.position, enemy.transform.position) < range)
                {
                    enemies.Enqueue(enemy);
                }
            }
        }
    }
    private void DequeueEnemy()
    { 
        Transform _enemy = enemies.Peek().transform;
        if (Vector3.Distance(transform.position, _enemy.position) > range)
        {
            enemies.Dequeue();
        }
    }
    private GameObject SetTarget()
    {
        if(enemies.Count > 0)
        {
            foreach (GameObject obj in enemies)
            {
                if (Vector3.Distance(transform.position, obj.transform.position) < range)
                {
                    return obj;
                }
            }
        }
        return null;
    }
    private void Attack(GameObject enemy)
    {

        GameObject bullet = objectPool.GetPooledObject();
        // bullet.transform.position = transform.position;
        // bullet.GetComponent<FireScript>().Target(enemy);
        // bullet.GetComponent<FireScript>().Fire();
    }
}

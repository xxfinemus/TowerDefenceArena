﻿using UnityEngine;
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
    private Queue<GameObject> enemies = new Queue<GameObject>();

    public GameObject[] arrayEnemies;

    [SerializeField]
    private GameObject target;

    public string Name
    {
        get { return name; }
    }
	// Use this for initialization
	void Start ()
    {
        if (!objectPool)
        {
            objectPool = BulletObjectPoolScript.current;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        LockToTarget();
        AddToQueue();
        ClearQueue();
        target = SetTarget();
        if (target != null)
        {
            Attack(target);
        }

	}
    private void LockToTarget()
    {
        if (target != null)
        {
            Vector3 targetPos = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
            transform.LookAt(targetPos);
        }
    }
    // Add enemy to queue
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
                    arrayEnemies = enemies.ToArray();
                }
            }
        }
    }
    // If the front enemy is out of range, then put in the back of the queue
    private void DequeueEnemy()
    { 
        Transform _enemy = enemies.Peek().transform;
        if (Vector3.Distance(transform.position, _enemy.position) > range)
        {
            enemies.Dequeue();
        }
    }
    // Set target equal to front element of queue
    private GameObject SetTarget()
    {
        if(enemies.Count > 0)
        {
            DequeueEnemy();
            return enemies.Peek();
        }
        return null;
    }
    // Clear queue
    private void ClearQueue()
    {
        if (enemies.Count > 0)
        {
            foreach (GameObject obj in enemies)
            {
                if (Vector3.Distance(transform.position, obj.transform.position) < range)
                {
                    return;
                }
            }
            enemies.Clear();
        }
    }
    private void Attack(GameObject enemy)
    {

        //GameObject bullet = objectPool.GetPooledObject();
        //bullet.transform.position = transform.position;
        // bullet.GetComponent<FireScript>().Target(enemy);
        // bullet.GetComponent<FireScript>().Fire();
    }
}

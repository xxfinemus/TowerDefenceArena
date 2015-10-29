using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyObjectPoolScript : MonoBehaviour
{

    public static EnemyObjectPoolScript current;
    public GameObject pooledObject;
    public int pooledAmount = 20; //size of the pool.

    //Makes the size of the pool grow as needed. It remains that size, it does not become smaller.
    public bool WillGrow = true;

    List<GameObject> pooledObjects; //This list can be made public to view the size of the growing pool.

    void Awake()
    {
        current = this;
    }

    /// <summary>
    /// Creates the object pool.
    /// </summary>
    void Start()
    {
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        if (WillGrow)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject);
            pooledObjects.Add(obj);
            return obj;
        }

        return null;
    }
}

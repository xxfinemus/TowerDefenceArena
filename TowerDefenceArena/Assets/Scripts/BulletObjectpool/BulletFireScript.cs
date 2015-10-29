using UnityEngine;
using System.Collections;

public class BulletFireScript : MonoBehaviour
{
    //Time between bullet is fired.
    public float fireTime = .05f;

    void Start()
    {
        //Creates the bullet with interval of fireTime; 
        InvokeRepeating("Fire", fireTime, fireTime);
    }

    void Fire()
    {
        //Gets a bullet from the object pool.
        GameObject obj = BulletObjectPoolScript.current.GetPooledObject();
        
        //If there is not an object in the object pool and willGrow is not true,
        //it will return null and we will not get a bullet.
        if (obj == null) return;

        //Creates the bullet at the transforms position.
        obj.transform.position = transform.position;
        obj.transform.rotation = transform.rotation;
        obj.SetActive(true);

    }
}

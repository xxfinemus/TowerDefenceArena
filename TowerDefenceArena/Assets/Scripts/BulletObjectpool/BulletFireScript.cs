using UnityEngine;
using System.Collections;

public class BulletFireScript : MonoBehaviour
{
    //Time between bullet is fired.
    public float fireTime = 5f;

    void Start()
    {
        //Creates the bullet with interval of fireTime; 
        //InvokeRepeating("Fire", 0, 3);

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Fire();
        }
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
        obj.GetComponent<SecondBulletScript>().StartPosition = transform.position;
        obj.transform.rotation = transform.rotation;
        obj.SetActive(true);

    }
}

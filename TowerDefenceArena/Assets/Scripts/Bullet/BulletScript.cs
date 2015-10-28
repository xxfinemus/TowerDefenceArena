using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour
{
    //The speed of the bullet;
    public float speed = 5;
    
    /// <summary>
    /// Moves the bullet.
    /// </summary>
    void Update()
    {
        transform.Translate(0, speed * Time.deltaTime, 0);
    }
}

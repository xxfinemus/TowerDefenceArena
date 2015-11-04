using UnityEngine;
using System.Collections;

public class BulletDestroyScript : MonoBehaviour
{
    /// <summary>
    /// Sends the bullet back in the object pool after it is used.
    /// </summary>

    //void OnEnable()
    //{

    //    //For now it is set to be set to Inactive after 2 seconds.
    //    Invoke("Destroy", 3f);
    //}

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "enemy")
        {
            Destroy();
        }
    }

    //Set the bullet to Inactive = sends it back in the object pool.
    void Destroy()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Makes sure the bullet does not immediately set it self to disabled
    /// as soon as it is enabled.
    /// </summary>
    void OnDisable()
    {
        CancelInvoke();
    }

}
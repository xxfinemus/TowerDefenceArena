using UnityEngine;
using System.Collections;

public class OnDeath : MonoBehaviour
{
    private bool die = false;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (die)
        {
            transform.parent.GetComponent<EnemyHealthScript>().OnDeath();

            die = false;
        }
    }

    public void Die()
    {
        die = true;
    }
}

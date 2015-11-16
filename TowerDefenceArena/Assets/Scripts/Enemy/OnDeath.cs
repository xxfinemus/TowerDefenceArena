using UnityEngine;
using System.Collections;

public class OnDeath : MonoBehaviour
{
    [SerializeField]
    private int timer;
    
    private bool die = false;
    private int counter;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (die)
        {
            if (counter <= timer)
            {
                transform.position += new Vector3(0, -0.05f, 0);
                counter++;
            }
            else
            {
                transform.parent.GetComponent<EnemyHealthScript>().OnDeath();
                
                die = false;
                counter = 0;
            }
        }
    }

    public void Die()
    {
        die = true;
    }
}

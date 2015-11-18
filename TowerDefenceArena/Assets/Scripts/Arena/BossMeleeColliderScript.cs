using UnityEngine;
using System.Collections;

public class BossMeleeColliderScript : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Player")
        {
            other.gameObject.GetComponentInParent<PlayerHealthScript>().TakeDamage(GetComponentInParent<BossAttack>().Damage);
        }
    }
}

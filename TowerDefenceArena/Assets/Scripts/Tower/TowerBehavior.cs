using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerBehavior : MonoBehaviour
{

    //[SerializeField]
    private string _name;

    //[SerializeField]
    private BulletObjectPoolScript objectPool;

    [SerializeField]
    private GameObject stone;

    [SerializeField]
    private AudioClip fireSound;

    [SerializeField]
    private float range;

    [SerializeField]
    private float rateOfFire;
    private float cooldown;
    [SerializeField]
    private Queue<GameObject> enemies = new Queue<GameObject>();

    [SerializeField]
    private int damage;

    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    public GameObject[] arrayEnemies;

    [SerializeField]
    private GameObject target;
    private GameObject tempTarget;

    public string _Name
    {
        get { return _name; }
    }

    public float Range
    {
        get
        {
            return range;
        }
        set
        {
            range = value;
        }
    }

    // Use this for initialization
    void Start()
    {
        damage = 20;
        cooldown = 0;

        if (!objectPool)
        {
            objectPool = BulletObjectPoolScript.current;
        }
    }

    // Update is called once per frame
    void Update()
    {
        LockToTarget();
        AddToQueue();
        ClearQueue();
        target = SetTarget();

        if (cooldown <= 0)
        {
            if (target != null)
            {
                tempTarget = target;
                GetComponent<Animator>().SetTrigger("fire");

                cooldown = rateOfFire;
            }
        }
        else
        {
            cooldown -= Time.deltaTime;
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
        GameObject[] _enemies = GameObject.FindGameObjectsWithTag("enemy");
        foreach (GameObject enemy in _enemies)
        {
            if (!enemies.Contains(enemy))
            {
                if (Vector3.Distance(transform.position, enemy.transform.position) < Range && enemy.GetComponent<EnemyHealthScript>().CurrentHealth > 0)
                {
                    Vector3 targetPos = new Vector3(enemy.transform.position.x, transform.position.y, enemy.transform.position.z);

                    if (Vector3.Distance(transform.position, targetPos) < range)
                    {
                        enemies.Enqueue(enemy);
                        arrayEnemies = enemies.ToArray();
                    }
                }
            }
        }
    }

    // If the front enemy is out of range, then put in the back of the queue
    private void DequeueEnemy()
    {
        Transform _enemy = enemies.Peek().transform;
        if (Vector3.Distance(transform.position, _enemy.position) > Range || _enemy.GetComponent<EnemyHealthScript>().CurrentHealth <= 0)
        {
            Vector3 targetPos = new Vector3(_enemy.position.x, transform.position.y, _enemy.position.z);

            if (Vector3.Distance(transform.position, targetPos) > range)
            {
                enemies.Dequeue();
            }
        }
    }

    // Set target equal to front element of queue
    private GameObject SetTarget()
    {
        if (enemies.Count > 0)
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
                if (Vector3.Distance(transform.position, obj.transform.position) < Range)
                {
                    Vector3 targetPos = new Vector3(obj.transform.position.x, transform.position.y, obj.transform.position.z);

                    if (Vector3.Distance(transform.position, targetPos) < range)
                    {
                        return;
                    }
                }
            }
            enemies.Clear();
        }
    }
    private void Attack()
    {
        //GameObject bullet = objectPool.GetPooledObject();
        //bullet.transform.position = transform.position;
        // bullet.GetComponent<FireScript>().Target(enemy);
        // bullet.GetComponent<FireScript>().Fire();
        //Gets a bullet from the object pool.
        GameObject obj = BulletObjectPoolScript.current.GetPooledObject();

        //If there is not an object in the object pool and willGrow is not true,
        //it will return null and we will not get a bullet.
        if (obj == null) return;

        //Creates the bullet at the transforms position.
        obj.transform.position = transform.position;
        obj.GetComponent<SecondBulletScript>().Damage = damage;
        obj.GetComponent<SecondBulletScript>().StartPosition = transform.position;
        obj.GetComponent<SecondBulletScript>().Target = tempTarget.transform;
        obj.SetActive(true);
    }

    private void PlayFireSound()
    {
        GetComponent<AudioSource>().PlayOneShot(fireSound);
    }

    private void AnimToggleStone()
    {
        if (stone.activeSelf == true)
        {
            stone.SetActive(false);
        }
        else
        {
            stone.SetActive(true);
        }
    }
}

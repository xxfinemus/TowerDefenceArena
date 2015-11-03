using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour
{
    //The speed of the bullet;
    public float speed;
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float gravity;
    private float distance;
    private float hight;
    private Vector3 startPosition;
    private float flightTime;
    private float angle;
    private float distanceTraveled;

    public Transform Target
    {
        set { target = value; }
    }

    /// <summary>
    /// Moves the bullet.
    /// </summary>
    void Update()
    {
        MathTest();
        TurnTowardsTarget();
        TargetDistance();
        FireAngle();
        BulletHeight();
        transform.Translate(0, 0, 5 * Time.deltaTime);

    }

    void OnEnable()
    {
        startPosition = transform.position;
        flightTime = 0;
        //Debug.Log(startPosition);
       
    }
    /// <summary>
    /// Calculates the distance between the tower and the enemy target.
    /// </summary>
    void TargetDistance()
    {
        distance = Vector3.Distance(target.position, startPosition);
        Vector3 targetPos = startPosition;
        targetPos.y = transform.position.y;
        distanceTraveled = Vector3.Distance(startPosition, transform.position);
        //Debug.Log(distance);
        //Debug.Log(distanceTraveled);
        //Debug.Log(startPosition);
        //Debug.Log(target.position);
        //Debug.Log(transform.position);
        hight = target.position.y - transform.position.y;
    }
    /// <summary>
    /// Calculates the angle the bullet is fired from the tower.
    /// </summary>
    void FireAngle()
    {
        angle =(float) (Mathf.Atan((Mathf.Pow(speed, 2) - Mathf.Sqrt(Mathf.Pow(speed, 4) - gravity * (gravity * Mathf.Pow(distance, 2) + 2 * hight * Mathf.Pow(speed, 2)))) / (gravity * distance)));
        //Debug.Log(angle);
    }

    void FlightTime()
    {
        //flightTime = 
    }

    /// <summary>
    /// sets the targets y position the same as ours temporary.
    /// </summary>
    void TurnTowardsTarget()
    {
        Vector3 targetPos = target.position;
        targetPos.y = transform.position.y;
        transform.LookAt(targetPos);
    }
    /// <summary>
    /// Sets the height as the value it should be comparred to the length, according to the calculated trajectory.
    /// </summary>
    void BulletHeight()
    {
        float tempHeight = (float) (distanceTraveled * Mathf.Tan(angle) - (gravity * Mathf.Pow(distanceTraveled, 2) / (2 * Mathf.Pow(((speed * Mathf.Cos(angle))), 2))));
        Vector3 heightToBe = new Vector3(transform.position.x, tempHeight, transform.position.z);
        //Debug.Log(hight);
        //Debug.Log(distanceTraveled);
        //Debug.Log(angle);
        //transform.position = heightToBe;
        //Debug.Log(heightToBe);
        transform.position = Vector3.MoveTowards(transform.position, heightToBe, Vector3.Distance(transform.position, heightToBe));
        //transform.Translate(0, tempHeight, 0);
        //Debug.Log(tempHeight);
    }
    /// <summary>
    /// Test of our math.
    /// </summary>
    void MathTest()
    {
        float v = 20;
        float g = 9.8f;
        float x = 10;
        //float z = 5;
        float y = 0;
        float r;

        r = Mathf.Atan((Mathf.Pow(v, 2) - Mathf.Sqrt(Mathf.Pow(v, 4) - g * (g * Mathf.Pow(x, 2) + 2 * y * Mathf.Pow(v, 2)))) / (g * x));
        //Debug.Log(r);
    }
}

using UnityEngine;
using System.Collections;

public class SecondBulletScript : MonoBehaviour
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

    [SerializeField]
    private float testTravel;

    public Transform Target
    {
        set { target = value; }
    }
    void OnEnable()
    {
        startPosition = transform.position;
        flightTime = 0;
        //Debug.Log(startPosition);

    }
    void TargetDistance()
    {
        distance = Vector3.Distance(startPosition, target.position);
        Vector3 targetPos = startPosition;
        targetPos.y = transform.position.y;
        distanceTraveled = Vector3.Distance(startPosition, transform.position);
        Debug.Log(distance);
        Debug.Log(distanceTraveled);
        //Debug.Log(startPosition);
        //Debug.Log(target.position);
        //Debug.Log(transform.position);

    }
    // Use this for initialization
    void Start()
    {

    }
    void FireAngle()
    {
        angle = (float)(Mathf.Atan((Mathf.Pow(speed, 2) - Mathf.Sqrt(Mathf.Pow(speed, 4) - gravity * (gravity * Mathf.Pow(distance, 2) + 2 * hight * Mathf.Pow(speed, 2)))) / (gravity * distance) + transform.position.y));
        Debug.Log(angle);
    }
    // Update is called once per frame
    void Update()
    {
        TurnTowardsTarget();
        TargetDistance();
        FireAngle();
        BulletHeight();
    }

    void BulletHeight()
    {
        float tempHeight = (float)(testTravel * Mathf.Tan(angle) - (gravity * Mathf.Pow(testTravel, 2) / (2 * Mathf.Pow(((speed * Mathf.Cos(angle))), 2))));
        Vector3 heightToBe = new Vector3(transform.position.x, tempHeight, transform.position.z);
        //Debug.Log(hight);
        //Debug.Log(distanceTraveled);
        //Debug.Log(angle);
        //transform.position = heightToBe;
        //Debug.Log(heightToBe);
        //transform.position = Vector3.MoveTowards(transform.position, heightToBe, Vector3.Distance(transform.position, heightToBe));
        transform.position = heightToBe;
        //transform.Translate(0, tempHeight, 0);
        Debug.Log(tempHeight);
    }

    void TurnTowardsTarget()
    {
        Vector3 targetPos = target.position;
        targetPos.y = transform.position.y;
        transform.LookAt(targetPos);
    }
}

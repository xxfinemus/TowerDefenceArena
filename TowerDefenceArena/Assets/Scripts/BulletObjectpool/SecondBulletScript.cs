using UnityEngine;
using System.Collections;

public class SecondBulletScript : MonoBehaviour
{
    //The speed of the bullet;
    public float speed;
    [SerializeField]
    private GameObject target;
    [SerializeField]
    private float gravity;
    private float distance;
    private float hight;
    private Vector3 startPosition;

    public Vector3 StartPosition
    {
        get { return startPosition; }
        set { startPosition = value; }
    }
    private float flightTime;
    private float angle;
    private float distanceTraveled;

    [SerializeField]
    private float testTravel;

    public GameObject Target
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
        Vector3 targetPos = target.transform.position;
        targetPos.y = startPosition.y;
        distance = Vector3.Distance(startPosition, targetPos);
        targetPos = startPosition;
        targetPos.y = transform.position.y;
        distanceTraveled = Vector3.Distance(targetPos, transform.position);
        Debug.Log("Distance " + distance);
        Debug.Log("DistancTraveled " + distanceTraveled);
        //Debug.Log(startPosition);
        //Debug.Log(target.position);
        //Debug.Log(transform.position);
        hight = target.transform.position.y - startPosition.y;
        Debug.Log("Height " + hight);

    }
    // Use this for initialization
    void Start()
    {
        

        startPosition = transform.position;
        flightTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        TurnTowardsTarget();
        TargetDistance();
        if (distance <= distanceTraveled)
        {
            transform.position = target.transform.position;
        }
        else
        {
            transform.Translate(0, 0, speed * Time.deltaTime);
        }
        //FireAngle();
        //BulletHeight();
        BetterPath();
    }

    void BulletHeight()
    {
        float tempHeight = (float)(startPosition.y + distanceTraveled * Mathf.Tan(angle) - (gravity * Mathf.Pow(distanceTraveled, 2) / (2 * Mathf.Pow(((speed * Mathf.Cos(angle))), 2))));
        Vector3 heightToBe = new Vector3(transform.position.x, tempHeight, transform.position.z);
        transform.position = heightToBe;
    }
    void FireAngle()
    {
        angle = (float)(Mathf.Atan((Mathf.Pow(speed, 2) + Mathf.Sqrt(Mathf.Pow(speed, 4) - gravity * (gravity * Mathf.Pow(distance, 2) + 2 * hight * Mathf.Pow(speed, 2)))) / (gravity * distance)));
    }

    void TurnTowardsTarget()
    {
        Vector3 targetPos = target.transform.position;
        targetPos.y = transform.position.y;
        transform.LookAt(targetPos);
    }

    void BetterPath()
    {
        Vector2 start = new Vector2(0, startPosition.y);
        Vector2 end = new Vector2(distance, target.transform.position.y);
        Vector2 mid = new Vector2(distance/2, startPosition.y + 5);
        float A = -Mathf.Pow(start.x, 2) + Mathf.Pow(mid.x, 2);
        float B = -start.x + mid.x;
        float D = -start.y + mid.y;
        float AA = -Mathf.Pow(mid.x, 2) + Mathf.Pow(end.x, 2);
        float BB = -mid.x + end.x;
        float DD = -mid.y + end.y;
        float BM = -(BB / B);
        float AAA = BM * A + AA;
        float DDD = BM * D + DD;
        float a = DDD / AAA;
        float b = (D-A*a)/B;
        float c = start.y - a * Mathf.Pow(start.x, 2) - b * start.x;
        float tempHeight = (float)(a*Mathf.Pow(distanceTraveled, 2) + b * distanceTraveled + c);
        Vector3 heightToBe = new Vector3(transform.position.x, tempHeight, transform.position.z);
        transform.position = heightToBe;


    }
}

﻿using UnityEngine;
using System.Collections;

public class SecondBulletScript : MonoBehaviour
{
    //The speed of the bullet;
    Vector2 start;
    Vector2 end;
    Vector2 mid;
    float A ;
    float B;
    float D;
    float AA;
    float BB;
    float DD;
    float BM;
    float AAA;
    float DDD;
    float a;
    float b;
    float c;
    Vector3 oldTargetPos;
    public float speed;
    [SerializeField]
    private Transform target;
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
    private float angle;
    private float distanceTraveled;


    public Transform Target
    {
        set { target = value; }
    }

    void OnEnable()
    {
        oldTargetPos = new Vector3(100, 100, 100);
        startPosition = transform.position;
        hight = target.position.y - startPosition.y;
        distanceTraveled = 0;

        //Debug.Log(startPosition);

    }
    void TargetDistance()
    {
        Vector3 targetPos = target.position;
        targetPos.y = startPosition.y;
        distance = Vector3.Distance(startPosition, targetPos);
        targetPos = startPosition;
        targetPos.y = transform.position.y;
        distanceTraveled = Vector3.Distance(targetPos, transform.position);
     

    }
    // Use this for initialization
    void Start()
    {

        oldTargetPos = new Vector3(100, 100, 100);
        startPosition = transform.position;
        hight = target.position.y - startPosition.y;
        distanceTraveled = 0;

    }

    // Update is called once per frame
    void Update()
    {
        TargetDistance();
        if (distance <= distanceTraveled)
        {
            transform.position = target.position;
        }
        else
        {
            TurnTowardsTarget();            
            transform.Translate(0, 0, speed * Time.deltaTime);
            BetterPath();
        }

   
    }


    void TurnTowardsTarget()
    {
        Vector3 targetPos = target.position;
        targetPos.y = transform.position.y;
        transform.LookAt(targetPos);
    }

    void BetterPath()
    {

        
        //if (Vector3.Distance(target.position, oldTargetPos) >= 1)
        //{
            oldTargetPos = target.position;
            start = new Vector2(0, startPosition.y);
            end = new Vector2(distance, target.position.y);
            mid = new Vector2(distance / 2, startPosition.y + 5);
            A = -Mathf.Pow(start.x, 2) + Mathf.Pow(mid.x, 2);
            B = -start.x + mid.x;
            D = -start.y + mid.y;
            AA = -Mathf.Pow(mid.x, 2) + Mathf.Pow(end.x, 2);
            BB = -mid.x + end.x;
            DD = -mid.y + end.y;
            BM = -(BB / B);
            AAA = BM * A + AA;
            DDD = BM * D + DD;
            a = DDD / AAA;
            b = (D - A * a) / B;
            c = start.y - a * Mathf.Pow(start.x, 2) - b * start.x;
        //}
        float tempHeight = (float)(a * Mathf.Pow(distanceTraveled, 2) + b * distanceTraveled + c);
        //float tempHeight = 0;
        Vector3 heightToBe = new Vector3(transform.position.x, tempHeight, transform.position.z);
        transform.position = heightToBe;


    }
}

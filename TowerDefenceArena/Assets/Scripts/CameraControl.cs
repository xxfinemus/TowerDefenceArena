using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour 
{
    [SerializeField]
    private float top, bottom, left, right, front, back;
    private Vector3 zoom1, zoom2;
	// Use this for initialization
	void Start () 
    {
	    
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    private void Zoom()
    {
        Touch[] myTouches = Input.touches;
        if (myTouches.Length == 2)
        {
            
        }
        else
        {

        }

    }

    private void Move()
    {

    }

    private void ConfineToBounds()
    {
        Vector3 confineVecter = transform.position;
        if (transform.position.y < bottom)
        {
            confineVecter.y = bottom;
        }
        else if (transform.position.y > top)
        {
            confineVecter.y = top;
        }
        if (transform.position.x < back)
        {
            confineVecter.x = back;
        }
        else if (transform.position.x > front)
        {
            confineVecter.x = front;
        }
        if (transform.position.z < left)
        {
            confineVecter.z = left;
        }
        else if (transform.position.z > right)
        {
            confineVecter.z = right;
        }
    }
}

﻿using UnityEngine;
using System.Collections;

public class MoveEnemy : MonoBehaviour {

    [SerializeField]
    private float speed;
	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position += Vector3.left * (speed *Time.deltaTime);
	}
}

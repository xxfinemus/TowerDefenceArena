using UnityEngine;
using System.Collections;

public class testscript : MonoBehaviour {

	// Use this for initialization
    NavMeshObstacle obs;
	void Start () {
        obs = GetComponent<NavMeshObstacle>();
        obs.carving = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

using UnityEngine;
using System.Collections;

public class obstest : MonoBehaviour {

	// Use this for initialization
    NavMeshObstacle obs;
    void Start()
    {
        obs = GetComponent<NavMeshObstacle>();
        obs.carving = true;
        obs.carvingTimeToStationary = 0.0f;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}

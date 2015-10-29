using UnityEngine;
using System.Collections;

public class obstacleCarve : MonoBehaviour {

	// Use this for initialization
    NavMeshObstacle obs;
    void Start()
    {
        obs = GetComponent<NavMeshObstacle>();
        obs.carving = true;
        obs.carvingTimeToStationary = 0.0f;
    }
}

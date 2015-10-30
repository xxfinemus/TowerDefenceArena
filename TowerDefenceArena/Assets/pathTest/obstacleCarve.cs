using UnityEngine;
using System.Collections;

public class ObstacleCarve : MonoBehaviour {

	// Use this for initialization
    private NavMeshObstacle obs;
    void Start()
    {
        obs = GetComponent<NavMeshObstacle>();
        obs.carving = true;
        obs.carvingTimeToStationary = 0.0f;
    }
}

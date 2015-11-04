using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WallBehaviorAgent : MonoBehaviour {

    // Field
    [SerializeField]
    private GameObject[] walls;
    private Transform[][] corners;
	// Use this for initialization
	void Start ()
    {
        walls = GameObject.FindGameObjectsWithTag("Wall");
        corners = new Transform[walls.Length][];
        for (int i = 0; i < walls.Length; i++)
        {
            corners[i] = GetChildsInWall(walls[i]);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}
    // Get childs of wall
    private Transform[] GetChildsInWall(GameObject wall)
    {
        List<Transform> _childs = new List<Transform>();
        foreach (Transform child in wall.transform)
        {
            _childs.Add(child);
        }
        return _childs.ToArray();
    }
    // Finds neighbours to a speciffic wall
    private GameObject[] GetNeighbours(GameObject wall)
    {
        List<GameObject> _neighbours = new List<GameObject>();
        Vector3[] dirs = { Vector3.down, Vector3.left, Vector3.up, Vector3.right};
        for (int i = 0; i < 4; i++)
        { 
            RaycastHit hit;
            if(Physics.Raycast(transform.position, dirs[i], out hit, 1f))
            {
                
            }
        }
        return _neighbours.ToArray();
    }
}

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
        for (int i = 0; i < walls.Length; i++)
        {
            GameObject[] _neighbours = GetNeighbours(walls[i]);
            //if(walls[i])
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        
	}
    // Get all corners from all walls
    private Transform[][] GetAllCorners()
    {
        Transform[][] _corners = new Transform[walls.Length][];
        for (int i = 0; i < _corners.Length; i++)
        {
            _corners[i] = GetCornersInWall(walls[i]);
        }
        return _corners;
    }
    // Get childs of wall
    private Transform[] GetCornersInWall(GameObject wall)
    {
        List<Transform> _childs = new List<Transform>();
        foreach (Transform child in wall.transform)
        {
            _childs.Add(child);
        }
        return _childs.ToArray();
    }
    // Find neighbours to a speciffic wall
    private GameObject[] GetNeighbours(GameObject wall)
    {
        List<GameObject> _neighbours = new List<GameObject>();
        Vector3[] dirs = { -Vector3.forward, Vector3.left, Vector3.forward, Vector3.right};
        int count = 0;
        for (int i = 0; i < 4; i++)
        { 
            RaycastHit hit;
            if(Physics.Raycast(wall.transform.position, dirs[i], out hit, 5f))
            {
                if (hit.collider.tag == "Wall")
                {
                    _neighbours.Add(hit.collider.gameObject);
                    Debug.Log(string.Format("Name: {0}->\t Neighbour[{1}]\t Name:{2}\t Pos:{3}", wall.name, count, _neighbours[count].name, _neighbours[count].transform.position));
                    count++;
                }
            }
        }
        return _neighbours.ToArray();
    }
}

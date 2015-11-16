using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WallBehaviorAgent : MonoBehaviour {

    static public WallBehaviorAgent WallAgent;
    // Field
    [SerializeField]
    private GameObject wallModel;

    //private Transform[][] corners;
	// Use this for initialization
	void Start ()
    {
        WallAgent = this;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            InstantiateWall(new Vector3(-2.7f, 0, -2.7f));
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            InstantiateWall(new Vector3(0, 0, -2.8f));
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            InstantiateWall(new Vector3(2.8f, 0, -2.8f));
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            InstantiateWall(new Vector3(-2.8f, 0, 0));
        }
        else if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            InstantiateWall(new Vector3(0, 0, 0));
        }
        else if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            InstantiateWall(new Vector3(3, 0, 0));
        }
        else if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            InstantiateWall(new Vector3(-3, 0, 3));
        }
        else if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            InstantiateWall(new Vector3(0, 0, 3));
        }
        else if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            InstantiateWall(new Vector3(3, 0, 3));
        }
	}
    // When instantiating a new wall
    public void InstantiateWall(Vector3 pos)
    {
        GameObject _wall = Instantiate(wallModel, pos, Quaternion.identity) as GameObject;
        _wall.transform.localEulerAngles = new Vector3(0, 0, 0);

        Transform[] _neighbours = GetNeighbours(_wall);    // Neighbours of the wall
        Transform[] _corners = GetCornersInWall(_wall);     // Corners of the wall
        // Get all neighbours from wall individualy
        foreach (Transform neighbour in _neighbours)
        {
            Transform[] n_corners = GetCornersInWall(neighbour.gameObject);
            foreach (Transform n_corner in n_corners)   // Neighbor wall's corners
            {
                foreach (Transform t_corner in _corners)    // This wall's corners
                {
                    if (Vector3.Distance(t_corner.position, n_corner.position) < 15f)
                    {
                        Destroy(t_corner.gameObject);
                    }
                }
            }
        }
    }
    // Get all corners from all walls
    private Transform[][] GetAllCorners(GameObject[] walls)
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
    private Transform[] GetNeighbours(GameObject wall)
    {
        List<Transform> _neighbours = new List<Transform>();
        Vector3[] dirs = { -Vector3.forward, Vector3.left, Vector3.forward, Vector3.right};
        int count = 0;
        for (int i = 0; i < 4; i++)
        {
            Debug.DrawRay(wall.transform.position + new Vector3(0, 1, 0), dirs[i] * 5f, Color.red, 5f);
            RaycastHit hit;
            if(Physics.Raycast(wall.transform.position + new Vector3(0, 1, 0), dirs[i], out hit, 5f))
            {
                if (hit.collider.tag == "Wall")
                {
                    _neighbours.Add(hit.collider.transform);
                    Debug.Log(string.Format("Name: {0}->\t Neighbour[{1}]\t Name:{2}\t Pos:{3}", wall.name, count, _neighbours[count].name, _neighbours[count].transform.position));
                    count++;
                }
            }
        }
        return _neighbours.ToArray();
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WallBehaviorAgent : MonoBehaviour {

    static public WallBehaviorAgent WallAgent;
    // Field
    [SerializeField]
    private GameObject wallModel;
    [SerializeField]
    private GameObject[] walls;

    //private Transform[][] corners;
	// Use this for initialization
	void Start ()
    {
        WallAgent = this;

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
        _wall.transform.localEulerAngles = new Vector3(270, 0, 0);

        GameObject[] _neighbours = GetNeighbours(_wall);    // Neighbours of the wall
        Transform[] _corners = GetCornersInWall(_wall);     // Corners of the wall
        // Get all neighbours from wall individualy
        foreach (GameObject neighbour in _neighbours)
        {
            Transform[] n_corners = GetCornersInWall(neighbour);
            foreach (Transform n_corner in n_corners)
            {
                foreach (Transform t_corner in _corners)
                {
                    if (Vector3.Distance(t_corner.position, n_corner.position) < 5f)
                    {
                        Destroy(t_corner.gameObject);
                    }
                }
            }
        }
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

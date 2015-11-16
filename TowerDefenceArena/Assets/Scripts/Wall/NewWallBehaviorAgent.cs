using UnityEngine;
using System.Collections;

public class NewWallBehaviorAgent : MonoBehaviour
{
    public static NewWallBehaviorAgent WallAgent;

    [SerializeField]
    private GameObject wallModel;

    [SerializeField]
    private GameObject[] walls;

    [SerializeField]
    private Transform[][] corners;

    private Vector3 sizeOfWall = Vector3.zero;

    private int wallCount = 0;

    // Use this for initialization
    void Start()
    {
        WallAgent = this;

        sizeOfWall = wallModel.transform.GetChild(0).GetComponent<Renderer>().bounds.extents;

        walls = GameObject.FindGameObjectsWithTag("Wall");
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.FindGameObjectsWithTag("Wall").Length > walls.Length)
        {
            walls = GameObject.FindGameObjectsWithTag("Wall");
            corners = GetAllCorners();
            RemoveCorners();
        }
        else if(GameObject.FindGameObjectsWithTag("Wall").Length < walls.Length)
        {
            walls = GameObject.FindGameObjectsWithTag("Wall");
            AddCorners();
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            GameObject newWall = InstantiateWall(new Vector3(0 + (sizeOfWall.x * 2 * wallCount), 0, 0));
            wallCount++;
        }
    }

    public GameObject InstantiateWall(Vector3 pos)
    {
        GameObject _wall = Instantiate(wallModel, pos, Quaternion.identity) as GameObject;
        _wall.transform.localEulerAngles = Vector3.zero;
        return _wall;
    }

    private Transform[][] GetAllCorners()
    {
        Transform[][] _corners = new Transform[walls.Length][];
        for (int i = 0; i < walls.Length; i++)
        {
            _corners[i] = new Transform[walls[i].transform.childCount];
            int j = 0;
            foreach(Transform _corner in walls[i].transform)
            {
                _corners[i][j] = _corner;
                j++;
            }
        }
        return _corners;
    }

    public void RemoveCorners()
    {
        for (int i = 0; i < walls.Length; i++)
        {
            foreach (Transform _corner in corners[i])
            {
                for (int j = 0; j < walls.Length; j++)
                {
                    if(walls[i] != walls[j])
                    {
                        foreach (Transform _compare in corners[j])
                        {
                            if(_corner.gameObject.activeInHierarchy == true && Vector3.Distance(_corner.position, _compare.position) < 0.2f)
                            {
                                _compare.gameObject.SetActive(false);
                            }
                        }
                    }
                }
            }
        }
    }

    public void AddCorners()
    {
        for(int i = 0; i < walls.Length; i++)
        {
            foreach(Transform _corner in corners[i])
            {
                if(_corner.gameObject.activeInHierarchy == false)
                {
                    for(int j = 0; j < walls.Length; j++)
                    {
                        if(walls[i] != walls[j])
                        {
                            foreach(Transform _compare in corners[j])
                            {
                                if(!(Vector3.Distance(_corner.position, _compare.position) < 0.2f))
                                {
                                    _corner.gameObject.SetActive(true);
                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}

using UnityEngine;
using System.Collections;

public class TouchInput : MonoBehaviour
{
    private RaycastHit hit;
    public GameObject obj;
    private Pathfinding path;
    private Grid _grid;
    [SerializeField]
    private LayerMask mask;
    [SerializeField]
    private GameObject makerObject;


    // Use this for initialization
    void Start()
    {
        path = GetComponent<Pathfinding>();
        _grid = GetComponent<Grid>();
    }




    // Update is called once per frame
    void Update()
    {
        ShowPointerPosition();
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            Ray rayt = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(rayt, out hit, mask))
            {
                Debug.Log("raycast");
                Node _node = _grid.NodeFromWorldPoint(hit.point);
                if (path.CheckIfPathIsValid(_node) && _node.walkable)
                {
                    Debug.Log("finger spawn");
                    _node.walkable = false;
                    GameObject _tower = (GameObject)Instantiate(obj, _node.worldPosition, transform.rotation);
                    _node.Tower = (GameObject)_tower;
                }
            }
        }
        //delete
        //if (Input.GetKeyDown(KeyCode.Mouse1))
        //{
        //    Ray testRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    if (Physics.Raycast(testRay, out hit, mask))
        //    {
        //        Node _node = _grid.NodeFromWorldPoint(hit.point);
        //        if (_node.Tower)
        //        {
        //            _node.DestroyTower();
        //            _node.walkable = true;
        //        }
        //    }
        //}
    }

    void ShowPointerPosition()
    {
        RaycastHit hitInfo;
        if (Input.touchCount > 0)
        {
            makerObject.transform.position = new Vector3(0, -500, 0);
        }
        if (Input.touchCount > 0 && (Input.GetTouch(0).phase == TouchPhase.Stationary || Input.GetTouch(0).phase == TouchPhase.Moved))
        {
            Ray raym = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(raym, out hitInfo, mask))
            {
                Node node = _grid.NodeFromWorldPoint(hitInfo.point);
                makerObject.transform.position = node.worldPosition;
            }
        }
    }
}
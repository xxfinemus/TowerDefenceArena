using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class TouchInput : MonoBehaviour
{
    [SerializeField]
    private GameObject objToSpawn;
    private Pathfinding path;
    private Grid grid;
    [SerializeField]
    private LayerMask mask;
    [SerializeField]
    private GameObject markerObject;
    private Vector3 markerObjectIdlePosition;
    [SerializeField]
    private Text textToFade;
    private bool coroutineIsRunning;
    [SerializeField]
    private GameObject buildMenu;
    [SerializeField]
    private GameObject DeleteUpgradeMenu;
    [SerializeField]
    private bool dontShowPointer;
    private GameObject buildObject;
    private Node nodeIsPressed;




    // Use this for initialization
    void Start()
    {

        markerObjectIdlePosition = new Vector3(0, -50, 0);
        path = GetComponent<Pathfinding>();
        grid = GetComponent<Grid>();
        textToFade = GameObject.Find("CantBuildText").GetComponent<Text>();
        coroutineIsRunning = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (buildObject != null)
        {
            BuildTower();
        }
        //Debug.Log(KeyUpInsideGrid());
        if (DeleteUpgradeMenu.activeInHierarchy && Deselect())
        {
            markerObjectPosition(markerObjectIdlePosition);
            buildMenu.SetActive(true);
            DeleteUpgradeMenu.SetActive(false);
        }
        if (PressedOnTower())
        {
            if (nodeIsPressed.Tower != null)
            {
                buildMenu.SetActive(false);
                DeleteUpgradeMenu.SetActive(true);
                markerObjectPosition(nodeIsPressed.worldPosition);
            }
        }

    }



    void markerObjectPosition(Vector3 position)
    {
        markerObject.transform.position = position;
    }

    private bool Deselect()
    {
        if (Input.touchCount > 0 && Camera.main.GetComponent<CameraControl>().TouchThreshold(Input.GetTouch(0), 2))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                float distance = Vector3.Distance(nodeIsPressed.worldPosition, hit.point);
                if (distance > 2)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private bool PressedOnTower()
    {
        if (Input.touchCount > 0 && Camera.main.GetComponent<CameraControl>().TouchThreshold(Input.GetTouch(0), 2))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 200, mask))
            {
                nodeIsPressed = grid.NodeFromWorldPoint(hit.point);
                return true;

            }
        }
        return false;
    }

    private IEnumerator Fade(object text)
    {
        textToFade.text = (string)text;
        coroutineIsRunning = true;
        Color main = textToFade.color;
        Color c;
        for (float f = 0.0f; f < 1; f += 0.1f)
        {
            c = textToFade.color;
            c.a = f;
            textToFade.color = c;
            yield return new WaitForSeconds(.05f);
        }
        textToFade.color = new Color(textToFade.color.r, textToFade.color.g, textToFade.color.b, 1f);
        yield return new WaitForSeconds(1f);

        for (float f = 1; f > 0f; f -= 0.1f)
        {
            c = textToFade.color;
            c.a = f;
            textToFade.color = c;
            yield return new WaitForSeconds(.05f);
        }
        textToFade.color = textToFade.color = new Color(textToFade.color.r, textToFade.color.g, textToFade.color.b, 0f);
        coroutineIsRunning = false;
    }
    public void BuildMenuButtons(string buttonClick)
    {
        if (buttonClick == "catapult")
        {
            Quaternion rotation = Quaternion.identity;
            rotation.eulerAngles = new Vector3(0, 0, 0);
            buildObject = (GameObject)Instantiate(objToSpawn, Vector3.zero, rotation);
            Camera.main.GetComponent<CameraControl>().IsBuilding = true;
        }
    }
    private void BuildTower()
    {
        float distance_to_screen = Camera.main.WorldToScreenPoint(buildObject.transform.position).z;
        Vector3 pos_move = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen));
        Node node = grid.NodeFromWorldPoint(new Vector3(pos_move.x, 0, pos_move.z));
        buildObject.transform.position = node.worldPosition;

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            bool validPath = path.CheckIfPathIsValid(node);
            //Debug.Log("validPath " + validPath);
            if (validPath && node.Tower == null)
            {
                
                node.walkable = false;
                node.Tower = buildObject;
                buildObject.GetComponent<NavMeshObstacle>().carving = true;
                buildObject = null;
                //Husk først at aktiverer bulletscripts efter tårnet er blevet placeret!

            }
            else if (node.Tower != null)
            {
                if (!coroutineIsRunning)
                {
                    StartCoroutine("Fade", "Space Occupied by tower");
                }
                Destroy(buildObject);
            }
            else if (!validPath)
            {
                if (!coroutineIsRunning)
                {
                    StartCoroutine("Fade", "You can't block the path");
                }
                Destroy(buildObject);
            }
            Camera.main.GetComponent<CameraControl>().IsBuilding = false;
            buildObject = null;
        }
    }
    public void DeleteTower()
    {
        markerObjectPosition(markerObjectIdlePosition);
        Destroy(nodeIsPressed.Tower);
        nodeIsPressed.walkable = true;
        nodeIsPressed = null;
        buildMenu.SetActive(true);
        DeleteUpgradeMenu.SetActive(false);
    }
    public void UpgradeTower()
    {

    }
}
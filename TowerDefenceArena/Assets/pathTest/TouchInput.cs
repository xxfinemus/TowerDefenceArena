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

        if (Input.GetKey(KeyCode.Escape))
        {
            Debug.Log("tilbage knappen er trykket på");
        }



        if (Input.GetKeyDown(KeyCode.G))
        {
            StatScript.Instance.ChangeStat("gold", 100);
            StatScript.Instance.ChangeStat("exp", 100);
        }

        if (buildObject != null)
        {
            Node node = BuildTowerPosition();
            BuildTower(node);
        }
        //Debug.Log(KeyUpInsideGrid());
        if (DeleteUpgradeMenu.activeInHierarchy && Deselect())
        {
            markerObjectPosition(markerObjectIdlePosition);
            buildMenu.SetActive(true);
            DeleteUpgradeMenu.SetActive(false);

            foreach (Transform child in nodeIsPressed.Tower.gameObject.transform)
            {
                if (child.name == "RangeFinder")
                {
                    child.gameObject.SetActive(false);
                }
            }
        }
        if (PressedOnTower())
        {
            if (nodeIsPressed.Tower != null)
            {
                buildMenu.SetActive(false);
                DeleteUpgradeMenu.SetActive(true);
                markerObjectPosition(nodeIsPressed.worldPosition);

                foreach (Transform child in nodeIsPressed.Tower.gameObject.transform)
                {
                    if (child.name == "RangeFinder")
                    {
                        child.gameObject.SetActive(true);
                    }
                }
            }
        }
    }
    void markerObjectPosition(Vector3 position)
    {
        markerObject.transform.position = position + new Vector3(0,2.5f,0);
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

    private Node BuildTowerPosition()
    {
        float distance_to_screen = Camera.main.WorldToScreenPoint(buildObject.transform.position).z;
        Vector3 pos_move = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen));
        Node node = grid.NodeFromWorldPoint(new Vector3(pos_move.x, 0, pos_move.z));
        buildObject.transform.position =  new Vector3(node.worldPosition.x, 2, node.worldPosition.z);

        foreach (Transform child in buildObject.gameObject.transform)
        {
            if (child.name == "RangeFinder")
            {
                child.lossyScale.Set(buildObject.GetComponentInChildren<TowerBehavior>().Range * 2, buildObject.GetComponentInChildren<TowerBehavior>().Range * 2, buildObject.GetComponentInChildren<TowerBehavior>().Range * 2);
                child.position = new Vector3(child.position.x, 0 , child.position.z);
            }
        }
        return node;
    }

    private void BuildTower(Node node)
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            bool validPath = path.CheckIfPathIsValid(node);
            //Debug.Log("validPath " + validPath);
            if (validPath && node.walkable == true && StatScript.Instance.GetStat("gold") >= buildObject.GetComponentInChildren<TowerBehavior>().GetTowerCost)
            {

                StatScript.Instance.ChangeStat("gold", -buildObject.GetComponentInChildren<TowerBehavior>().GetTowerCost);

                node.walkable = false;
                node.Tower = buildObject;
                buildObject.GetComponent<NavMeshObstacle>().enabled = true;

                foreach (Transform child in buildObject.gameObject.transform)
                {
                    if (child.name == "RangeFinder")
                    {
                        child.gameObject.SetActive(false);
                    }
                }

                buildObject.GetComponentInChildren<TowerBehavior>().enabled = true;
                buildObject = null;

                //Husk først at aktiverer bulletscripts efter tårnet er blevet placeret!
            }

            else if (StatScript.Instance.GetStat("gold") <= buildObject.GetComponentInChildren<TowerBehavior>().GetTowerCost)
            {
                if (!coroutineIsRunning)
                {
                    StartCoroutine("Fade", "You don't have enough gold");
                }
                Destroy(buildObject);
            }
            else if (node.Tower != null || node.walkable == false)
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
        StatScript.Instance.ChangeStat("gold", (int)(nodeIsPressed.Tower.GetComponentInChildren<TowerBehavior>().GetTowerCost / 2));
        nodeIsPressed = null;
        buildMenu.SetActive(true);
        DeleteUpgradeMenu.SetActive(false);


    }
    public void UpgradeTower()
    {

    }
}
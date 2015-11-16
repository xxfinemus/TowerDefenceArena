using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class TouchInput : MonoBehaviour
{


    [SerializeField]
    private GameObject wallPrefab;
    [SerializeField]
    private GameObject catapultPrefab;
    [SerializeField]
    private GameObject balistaPrefab;


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
    private GameObject buildObject;
    private Node nodeIsPressed;
    [SerializeField]
    private GameObject rightBarObject;
    [SerializeField]
    private Vector3 RightMenuBarEndPos;
    [SerializeField]
    private Vector3 RightMenuBarStartPos;

    private bool ShowDeleteUpgradeMenu = false;
    private bool ShowBuildMenu = false;
    private bool RightMenuTransition = false;

    void Start()
    {

        markerObjectIdlePosition = new Vector3(0, -50, 0);
        path = GetComponent<Pathfinding>();
        grid = GetComponent<Grid>();
        textToFade = GameObject.Find("CantBuildText").GetComponent<Text>();
        coroutineIsRunning = false;
    }
    void Update()
    {
        #region rightbaremenu
        array
        if (ShowDeleteUpgradeMenu)
        {
            ShowDeleteMenuBar();
        }
        else if (ShowBuildMenu)
        {
            ShowBuildMenuBar();
        }
        else
        {
            rightBarObject.transform.localPosition = Vector3.Lerp(rightBarObject.transform.localPosition, RightMenuBarStartPos, Time.deltaTime * 3f);
        }

        Debug.Log("build " + ShowBuildMenu + "upgrade " + ShowDeleteUpgradeMenu);
        #endregion

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
            ShowBuildMenu = true;

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

                markerObjectPosition(nodeIsPressed.worldPosition);
                ShowDeleteUpgradeMenu = true;
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
    void ShowDeleteMenuBar()
    {
        ShowDeleteUpgradeMenu = true;
        rightBarObject.transform.localPosition = Vector3.Lerp(rightBarObject.transform.localPosition, RightMenuBarEndPos, Time.deltaTime * 3f);

        if (RightMenuBarEndPos.y - rightBarObject.transform.localPosition.y < 50)
        {
            ShowDeleteUpgradeMenu = false;
            buildMenu.SetActive(false);
            DeleteUpgradeMenu.SetActive(true);
        }
    }
    void ShowBuildMenuBar()
    {
        ShowBuildMenu = true;
        rightBarObject.transform.localPosition = Vector3.Lerp(rightBarObject.transform.localPosition, RightMenuBarEndPos, Time.deltaTime * 3f);

        if (RightMenuBarEndPos.y - rightBarObject.transform.localPosition.y < 50)
        {
            ShowBuildMenu = false;
            buildMenu.SetActive(true);
            DeleteUpgradeMenu.SetActive(false);
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
            
            buildObject = (GameObject)Instantiate(catapultPrefab, Vector3.zero, rotation);
            Camera.main.GetComponent<CameraControl>().IsBuilding = true;
        }
        if (buttonClick == "wall")
        {
            Quaternion rotation = Quaternion.identity;
            rotation.eulerAngles = new Vector3(0, 0, 0);

            buildObject = (GameObject)Instantiate(wallPrefab, Vector3.zero, rotation);
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
            if (validPath && node.walkable == true && StatScript.Instance.GetStat("gold") >= buildObject.GetComponent<TowerCost>().Cost)
            {
                StatScript.Instance.ChangeStat("gold", -buildObject.GetComponent<TowerCost>().Cost);

                node.walkable = false;
                node.Tower = buildObject;
                buildObject.GetComponent<NavMeshObstacle>().enabled = true;
                if (buildObject.GetComponentInChildren<TowerBehavior>() != null)
                {
                    buildObject.GetComponentInChildren<TowerBehavior>().enabled = true;
                    Debug.Log("enabled tower beha");
                }

                foreach (Transform child in buildObject.gameObject.transform)
                {
                    if (child.name == "RangeFinder")
                    {
                        child.gameObject.SetActive(false);
                    }
                }

                buildObject = null;

                //Husk først at aktiverer bulletscripts efter tårnet er blevet placeret!
            }

            else if (StatScript.Instance.GetStat("gold") <= buildObject.GetComponent<TowerCost>().Cost)
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

        StatScript.Instance.ChangeStat("gold", (int)(nodeIsPressed.Tower.GetComponent<TowerCost>().Cost / 2));
        nodeIsPressed = null;
        ShowBuildMenu = true;


    }
    public void UpgradeTower()
    {
        if (StatScript.Instance.GetStat("gold") >= 10)
        {
            if (nodeIsPressed.Tower.GetComponentInChildren<TowerBehavior>() != null)
            {
                nodeIsPressed.Tower.GetComponentInChildren<TowerBehavior>().Damage++;
                StatScript.Instance.ChangeStat("gold", -10);
                StartCoroutine("Fade", "Tower Upgraded");
            }
            else
            {
                StartCoroutine("Fade", "You Can't upgrade this type of tower");
            }
        }
        else
        {
            StartCoroutine("Fade", "You don't have enough money to upgrade");
        }
    }
}
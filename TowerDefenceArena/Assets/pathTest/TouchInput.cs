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
    private GameObject makerObject;
    [SerializeField]
    private Text textToFade;
    private bool coroutineIsRunning;
    [SerializeField]
    private GameObject buildMenu;
    [SerializeField]
    private bool dontShowPointer;
    private Node _node;
    private Node buildNode;

    // Use this for initialization
    void Start()
    {
        path = GetComponent<Pathfinding>();
        grid = GetComponent<Grid>();
        textToFade = GameObject.Find("CantBuildText").GetComponent<Text>();
        coroutineIsRunning = false;
    }

    // Update is called once per frame
    void Update()
    {
        #region touch
        //ShowPointerPosition();
        //if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        //    RaycastHit hit;
        //    if (Physics.Raycast(ray, out hit, 100, mask.value))
        //    {
        //        _node = _grid.NodeFromWorldPoint(hit.point);
        //        ShowCanvasItems(_node);
        //    }
        //    else
        //    {
        //        dontShowPointer = false;
        //        buildMenu.SetActive(false);
        //    }
        //}
        #endregion

        #region mouse
        ShowPointerPositionMouse();


        if (Input.GetKeyDown(KeyCode.Mouse0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, mask.value))
            {
                _node = grid.NodeFromWorldPoint(hit.point);
                Debug.Log(_node.worldPosition + " click");
                ShowCanvasItems(_node);
            }
            else
            {
                dontShowPointer = false;
                buildMenu.SetActive(false);
            }
        }
        #endregion

    }
    private void ShowCanvasItems(Node _node)
    {
        if (!buildMenu.activeInHierarchy)
        {
            if (path.CheckIfPathIsValid(_node))
            {
                buildNode = _node;
                dontShowPointer = true;
                buildMenu.SetActive(true);
                buildMenu.transform.position = new Vector3(_node.worldPosition.x, _node.worldPosition.y, _node.worldPosition.z);
                buildMenu.transform.localPosition = new Vector3(buildMenu.transform.localPosition.x - 50, buildMenu.transform.localPosition.y, buildMenu.transform.localPosition.z -50);
            }
            else if (!coroutineIsRunning)
            {
                StartCoroutine("Fade", "You can't block the path");
            }
        }
    }
    void ShowPointerPositionMouse()
    {
        if (!dontShowPointer)
        {
            RaycastHit hitInfo;
            Ray raym = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(raym, out hitInfo, mask))
            {
                Node node = grid.NodeFromWorldPoint(hitInfo.point);
                makerObject.transform.position = new Vector3(node.worldPosition.x, 0, node.worldPosition.z);
            }
            else
            {
                makerObject.transform.position = new Vector3(0, -500, 0);
            }
        }
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
                Node node = grid.NodeFromWorldPoint(hitInfo.point);
                makerObject.transform.position = node.worldPosition;
            }
        }
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
            Debug.Log(f);
            yield return new WaitForSeconds(.05f);
        }
        textToFade.color = new Color(textToFade.color.r, textToFade.color.g, textToFade.color.b, 1f);
        yield return new WaitForSeconds(1f);

        for (float f = 1; f > 0f; f -= 0.1f)
        {
            c = textToFade.color;
            c.a = f;
            textToFade.color = c;
            Debug.Log(f);
            yield return new WaitForSeconds(.05f);
        }
        textToFade.color = textToFade.color = new Color(textToFade.color.r, textToFade.color.g, textToFade.color.b, 0f);
        coroutineIsRunning = false;
    }
    void BuildMenuButtons(string buttonClick)
    {
        if (buttonClick == "catapult" && buildNode.Tower == null)
        {
            buildNode.walkable = false;
            GameObject _tower = (GameObject)Instantiate(objToSpawn, buildNode.worldPosition, transform.rotation);
            buildNode.Tower = (GameObject)_tower;
        }
        if (buttonClick == "delete" && buildNode.Tower != null)
        {
            Debug.Log(_node.worldPosition + " destroy");
            Debug.Log("no tower to destroy");
            Destroy(buildNode.Tower);
            buildNode.Tower = null;
            buildNode.walkable = true;
        }
        buildMenu.SetActive(false);
        dontShowPointer = false;
    }
}
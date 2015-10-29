using UnityEngine;
using System.Collections;

public class TouchInput : MonoBehaviour
{
    RaycastHit hit;
    public GameObject obj;
    private Pathfinding path;

    // Use this for initialization
    void Start()
    {
        path = GetComponent<Pathfinding>();
    }




    // Update is called once per frame
    void Update()
    {
        #region touch
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray rayt = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(rayt, out hit))
            {
                if (hit.collider.tag == "floor" && path.CheckIfPathIsValid(hit.point))
                {
                    Instantiate(obj, hit.point, transform.rotation);
                }
            }

        }
        #endregion


        //bool test = EntranceScript.Instance.CheckIfPathIsValid();

        #region mouse
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray raym = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(raym, out hit))
            {
                if (hit.collider.tag == "floor" && path.CheckIfPathIsValid(hit.point))
                {
                    //Debug.Log("spawned wall");
                    //Instantiate(obj, hit.point, transform.rotation);
                }
            }
        }
        #endregion
    }
}

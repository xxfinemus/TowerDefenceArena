using UnityEngine;
using System.Collections;

public class finger : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    RaycastHit hit;
    public GameObject obj;
    // Update is called once per frame
    void Update()
    {
        #region touch
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray rayt = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(rayt, out hit))
            {
                if (hit.collider.tag == "floor")
                {
                    Instantiate(obj, hit.point, transform.rotation);
                }
            }

        }
        #endregion
        #region mouse
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Ray raym = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(raym, out hit))
            {
                if (hit.collider.tag == "floor")
                {
                    Instantiate(obj, hit.point, transform.rotation);
                }
            }
        }
        #endregion
    }
}

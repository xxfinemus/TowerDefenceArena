using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{
    [SerializeField]
    private float top, bottom, left, right, front, back;
    [SerializeField]
    public static CameraControl current;
    private GameObject plane;
    private float zoomDistance;
    private Vector3 startPos;
    private Vector2 touchStartPos;
    // Use this for initialization
    void Start()
    {
        current = this;
        startPos = transform.position;
        left += startPos.z;
        right += startPos.z;
        front += startPos.x;
        back += startPos.x;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Zoom();
        ConfineToBounds();
    }
    void OnGUI()
    {
        Vector3[] p_corners = GetVertices(plane);
        for (int i = 0; i < p_corners.Length; i++)
        {
            //Debug.Log("Corner " + (i + 1) + " position: " + p_corners[i]);
            Vector3 w_point = Camera.main.WorldToScreenPoint(p_corners[i]);
            //GUI.Label(new Rect(new Vector2(w_point.x, w_point + Screen.height), new Vector2(10, 10)), p_corners[i].ToString());
        }
    }
    private void Zoom()
    {
        Touch[] myTouches = Input.touches;
        if (myTouches.Length == 2 && (myTouches[0].phase == TouchPhase.Moved || myTouches[1].phase == TouchPhase.Moved))
        {
            zoomDistance = Vector2.Distance(myTouches[0].position, myTouches[1].position);
            float newDistance = Vector2.Distance((myTouches[0].position - myTouches[0].deltaPosition), (myTouches[1].position - myTouches[1].deltaPosition));
            float changeZoom = zoomDistance - newDistance;
            Debug.Log("Touch inputs: " + myTouches.Length);
            if (transform.position.z > bottom && transform.position.z < top)
            {
                transform.Translate(new Vector3(0, 0, changeZoom) * Time.deltaTime * 5);
            }
        }
    }

    private void Move()
    {
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 newPos = Input.GetTouch(0).deltaPosition;

            Vector3 dir = Quaternion.AngleAxis(45, transform.forward) * new Vector3(newPos.y, 0, -newPos.x);
            transform.position += new Vector3(dir.x, 0, dir.z) * Time.deltaTime * 8;
            Debug.Log(dir);
        }
    }
    public bool TouchThreshold(Touch touch, float delta)
    {
        float distance = (Vector2.Distance(touch.position, touchStartPos));
        if (touch.phase == TouchPhase.Began)
        {
            touchStartPos = touch.position;
        }
        else if (touch.phase == TouchPhase.Moved)
        {
            if (distance > delta)
            {
                Move();
                return false;
            }
        }
        else if (touch.phase == TouchPhase.Ended)
        {
            return (distance < delta);
        }
        return false;
    }
    private Vector3[] GetVertices(GameObject obj)
    {
        if (obj.GetComponent<MeshFilter>())
        {
            return obj.GetComponent<MeshFilter>().mesh.vertices;
        }
        return null;
    }
    private void ConfineToBounds()
    {
        Vector3 pos = transform.position;
        transform.position = new Vector3(Mathf.Clamp(pos.x, back, front), Mathf.Clamp(pos.y, bottom, top), Mathf.Clamp(pos.z, left, right));
    }
}

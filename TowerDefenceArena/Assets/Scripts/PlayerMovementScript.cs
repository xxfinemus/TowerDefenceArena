using UnityEngine;
using System.Collections;

public class PlayerMovementScript : MonoBehaviour
{
    [SerializeField]
    float speed;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
    }

    public void Move(Vector2 direction)
    {
        transform.position += new Vector3(direction.x * (speed * Time.deltaTime), 0, direction.y * (speed * Time.deltaTime));
    }
}

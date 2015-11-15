using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

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
        Move(new Vector3(CrossPlatformInputManager.GetAxis("Left_Horizontal"), 0, CrossPlatformInputManager.GetAxis("Left_Vertical")));
    }

    public void Move(Vector3 direction)
    {
        transform.position += new Vector3(direction.x * (speed * Time.deltaTime), 0, direction.z * (speed * Time.deltaTime));
    }
}

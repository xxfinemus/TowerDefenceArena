using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class Hero : MonoBehaviour
{
    private enum Device { touch, pc };
    [SerializeField]
    private float speed = 5;
    [SerializeField]
    private Device device = Device.pc;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = Vector3.zero;
        if (device == Device.touch)
        {
            dir = new Vector3(CrossPlatformInputManager.GetAxis("Left_Horizontal"), 0, CrossPlatformInputManager.GetAxis("Left_Vertical"));
        }
        else
        {
            dir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        }
        transform.Translate(dir * Time.deltaTime * speed);
    }
}

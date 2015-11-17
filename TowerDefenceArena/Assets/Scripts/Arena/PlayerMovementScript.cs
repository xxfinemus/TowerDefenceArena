using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovementScript : MonoBehaviour
{
    private enum Device { touch, pc };
    [SerializeField]
    private float speed = 5;
    [SerializeField]
    private Device device = Device.pc;

    private Animator characterAnimator;

    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    // Use this for initialization
    void Start()
    {
        characterAnimator = GetComponentInChildren<Animator>();
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

        if (dir != Vector3.zero)
        {
            if (!characterAnimator.GetBool("isMoving"))
            {
                characterAnimator.SetBool("isMoving", true);
            }
        }
        else
        {
            if (characterAnimator.GetBool("isMoving"))
            {
                characterAnimator.SetBool("isMoving", false);
            }
        }

        characterAnimator.transform.LookAt(dir + characterAnimator.transform.position);

        transform.Translate(dir * Time.deltaTime * speed);
    }
}

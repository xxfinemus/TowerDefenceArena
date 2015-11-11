using UnityEngine;
using System.Collections;

public class JoystickControl : MonoBehaviour
{
    private class Boundry
    {
        public Vector2 min, max;
    }
    private static JoystickControl[] joysticks;
    [SerializeField]
    private Vector2 posistion = Vector2.zero, size = Vector2.zero;
    [SerializeField]
    private bool isJoystick;
    [SerializeField]
    private RectTransform imageTransform;

    private int lastFingerId = -1;
    private Rect touchZone = new Rect(), defaultRect = new Rect();
    private Vector2 DeadZone;
	// Use this for initialization
	void Start ()
    {
        // Auto-resize the touch zone if it is a joystick
        if (isJoystick)
        {
            imageTransform = GetComponent<RectTransform>();
            posistion = new Vector2(imageTransform.position.x, imageTransform.position.y);
            size = imageTransform.sizeDelta;
        }
        touchZone = new Rect(posistion, size);
	}
    void OnEnable()
    {
        joysticks = FindObjectsOfType<JoystickControl>();
       // Vector2 touchPos
       // defaultRect = new Rect()
    }
	// Update is called once per frame
	void Update ()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.touches[i];
            if (touchZone.Contains(touch.position))
            {
                bool shouldLatchFinger = false;
                if (touchZone.Contains(touch.position))
                {
                    shouldLatchFinger = true;
                }
                if (shouldLatchFinger && (lastFingerId == -1 || lastFingerId != touch.fingerId))
                { 
                    
                }
            }
        }
	}
}

using UnityEngine;
using System.Collections;

public class JoystickControl : MonoBehaviour
{
    private class Boundary
    {
        public Vector2 min, max;
    }

    private static JoystickControl[] joysticks;

    private int lastFingerId = -1;

    private Rect touchZone = new Rect();    // The touch zone area
    private Rect defaultRect = new Rect();  // The default size and position of the image/button

    [SerializeField]
    private RectTransform imageTransform;

    private Vector2 guiTouchOffset = Vector2.zero;
    private Vector2 guiCenter = Vector2.zero;

    private Boundary guiBoundary = new Boundary();

    public bool isFingerDown
    {
        get { return (lastFingerId != -1); }    // Return true if a finger is pressed
    }
    public Vector2 position
    {
        get;
        private set;
    }
	// Use this for initialization
	void Start ()
    {
        imageTransform = GetComponent<RectTransform>(); // Get Rect!! :P

        defaultRect = new Rect(imageTransform.position, imageTransform.sizeDelta);

        guiTouchOffset.x = defaultRect.width * 0.5f;
        guiTouchOffset.y = defaultRect.height * 0.5f;

        guiCenter.x = defaultRect.x + guiTouchOffset.x;
        guiCenter.y = defaultRect.y + guiTouchOffset.y;

        guiBoundary.min.x = defaultRect.x - guiTouchOffset.x;
        guiBoundary.max.x = defaultRect.x + guiTouchOffset.x;
        guiBoundary.min.y = defaultRect.y - guiTouchOffset.y;
        guiBoundary.max.y = defaultRect.y + guiTouchOffset.y;
	}
    void OnEnable()
    {
        joysticks = FindObjectsOfType<JoystickControl>();
    }
    private void Reset()
    {
        imageTransform.position = defaultRect.position;
        lastFingerId = -1;
        position = Vector2.zero;
    }
	// Update is called once per frame
    void Update()
    {
        // Loop through all touches
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.touches[i];                         // Get current touch
            Vector2 guiTouchPos = touch.position - guiTouchOffset;  // Get the touch position relative to the offset
            Debug.Log(guiTouchPos);
            bool shouldLatchFinger = false;     // Create a new bool
            // If current touch is in touch zone
            if (touchZone.Contains(touch.position))
            {
                if (touchZone.Contains(touch.position))
                {
                    shouldLatchFinger = true;
                }
                if (shouldLatchFinger && (lastFingerId == -1 || lastFingerId != touch.fingerId))
                {
                    // position.x = radius * MathF.Cos(position.y);
                    // position.y = radius * MathF.Sin(position.x);
                    lastFingerId = touch.fingerId;
                }
            }
            if (lastFingerId == touch.fingerId)
            {
                imageTransform.position = new Vector2
                    (
                        Mathf.Clamp(guiTouchPos.x, guiBoundary.min.x, guiBoundary.max.x),
                        Mathf.Clamp(guiTouchPos.x, guiBoundary.min.y, guiBoundary.max.y)
                    );
            }
            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                Reset();
            }
        }
        position = new Vector2
            (
                (imageTransform.position.x + guiTouchOffset.x - guiCenter.x) / guiTouchOffset.x,
                (imageTransform.position.y + guiTouchOffset.y - guiCenter.y) / guiTouchOffset.y
            );
        Debug.Log(defaultRect);
        //float absoluteX = Mathf.Abs(position.x), absoluteY = Mathf.Abs(position.y);
        //if(absoluteX < )
    }	
}

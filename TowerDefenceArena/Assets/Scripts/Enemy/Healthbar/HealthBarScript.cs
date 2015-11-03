using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    Image bar;

    [SerializeField]
    Vector2 size;
    // Use this for initialization
    void Start()
    {
        RectTransform t = GetComponent<RectTransform>();

        t.sizeDelta = size;

        if (bar == null)
        {
            bar = GetComponent<Image>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Takes a scale and sets the size of the healthbar to be equal to that (Remember to divide current health with max health)
    /// </summary>
    /// <param name="size">The scale of the health bar</param>
    public void SetSize(float size)
    {
            bar.transform.localScale = new Vector3(size, transform.localScale.y);
    }

}

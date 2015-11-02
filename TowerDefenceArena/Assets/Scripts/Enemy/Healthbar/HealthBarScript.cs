using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    Image bar;

    EnemyHealthScript parent;

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

        if (parent == null)
        {
            parent = GetComponentInParent<EnemyHealthScript>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        SetSize();
    }

    //Gets the parent enemy's current and max health and sets the scale of the healthbar to be equal to them divided
    public void SetSize()
    {
        if (parent != null)
        {
            bar.transform.localScale = new Vector3(parent.Currenthealth / parent.MaxHealth, transform.localScale.y);
        }
    }

}

using UnityEngine;
using System.Collections;

public class TowerCost : MonoBehaviour {
    [SerializeField]
    private int cost;

    public int Cost
    {
        get
        {
            return cost;
        }
    }
}

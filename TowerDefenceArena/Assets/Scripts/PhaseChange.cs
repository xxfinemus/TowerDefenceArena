using UnityEngine;
using System.Collections;

public class PhaseChange : MonoBehaviour {


    static GameObject TDParent;
    static GameObject ArenaParent;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}


    public static void EnterArena()
    {
        if (TDParent == null)
        {
            TDParent = GameObject.Find("TD");
        }
        if (ArenaParent == null)
        {
            ArenaParent = GameObject.Find("ARENA");
        }

        TDParent.SetActive(false);
        ArenaParent.SetActive(true);


    }

    public static void EnterTD()
    {
        if (TDParent == null)
        {
            TDParent = GameObject.Find("TD");
        }
        if (ArenaParent == null)
        {
            ArenaParent = GameObject.Find("ARENA");
        }

        TDParent.SetActive(true);
        ArenaParent.SetActive(false);
    }
}

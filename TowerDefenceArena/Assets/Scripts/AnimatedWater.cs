using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimatedWater : MonoBehaviour {

    [SerializeField]
    private List<GameObject> waterobjList;
    [SerializeField]
    private float scrollSpeed;
	
	// Update is called once per frame
	void Update ()
    {
        float offset = Time.time * scrollSpeed;
        foreach (GameObject waterobj in waterobjList)
        {
            waterobj.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
        }

	}
}

using UnityEngine;
using System.Collections;

public class ArenaStartScript : MonoBehaviour
{
    static GameObject boss;

    static GameObject player;

    // Use this for initialization
    void Start()
    {
        boss = GameObject.FindGameObjectWithTag("Boss");

        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void BeginArena()
    {
        boss.GetComponent<BossAIScript>().Begin();

        player.GetComponent<PlayerHealthScript>().Begin();
    }

    void OnEnable()
    {
        BeginArena();
    }
}

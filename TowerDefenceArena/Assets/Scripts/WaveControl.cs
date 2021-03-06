﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class WaveControl : MonoBehaviour 
{
    [SerializeField]
    private GameObject button;
    [SerializeField]
    private GameObject[] enemies;
    [SerializeField]
    private float spawnFrequency;
    private int spawnNumber;
    [SerializeField]
    private int waveNumber;
    [SerializeField]
    private float waveDificulty;
    private bool waveRunning;
    private float enemyStatModifiyer;
    private List<GameObject> enemiesToSpawn;
    private static int enemiesRemaning;
    private bool waveCanStart;
    private float timeToSpawn;
    [SerializeField]
    private GameObject spawnPosition;

    GameObject fader;
    //test
    public bool forceStartWave;


    public static int EnemiesRemaning
    {
        set { enemiesRemaning = value; }
        get { return enemiesRemaning; }
    }

    public bool WaveCanStart
    {
        get { return waveCanStart; }
        set { waveCanStart = value; }
    }

    public bool WaveRunning
    {
        get { return waveRunning; }
    }

    public int WaveNumber
    {
        get { return waveNumber; }
    }
	// Use this for initialization
    void OnEnable()
    {
        button.GetComponent<Button>().interactable = true;
    }

	void Start () 
    {
        PhaseChange.FindParents();
        fader = GameObject.Find("Fader");
        waveCanStart = true;
        waveRunning = false;
        enemiesRemaning = 0;
        forceStartWave = false;
        enemiesToSpawn = new List<GameObject>();
        
	}
	
	// Update is called once per frame
	void Update () 
    {
        //if (forceStartWave)
        //{
        //    StartNextWave();
        //    forceStartWave = false;
        //}
        if (IsWaveDone() && waveRunning)
        {
            WaveComplete();

            fader.GetComponent<CameraFade_Script>().EndTDScene();
        }
        
        SpawnNextEnemy();


        
            

	}

    public void StartNextWave()
    {
        waveCanStart = false;
        waveRunning = true;
        waveNumber++;
        timeToSpawn = spawnFrequency;
        CreateNextWave();
    }

    private void SpawnNextEnemy()
    {
        if (enemiesToSpawn.Count > 0 && timeToSpawn <= 0)
        {
            timeToSpawn = spawnFrequency;
            
            GameObject obj = EnemyObjectPoolScript.current.GetPooledObject();
            if (obj == null)
            {
                return;
            }
            obj.transform.position = spawnPosition.transform.position;
            obj.GetComponent<EnemyHealthScript>().MaxHealth = enemyStatModifiyer * 20 + 100;
            obj.GetComponent<EnemyHealthScript>().CurrentHealth = enemyStatModifiyer * 20 + 100;
            obj.SetActive(true);
            obj.GetComponent<EnemyHealthScript>().UpdateHealthBar();
            enemiesRemaning++;
            enemiesToSpawn.RemoveAt(0);
        }
        if (waveRunning && timeToSpawn > 0)
        {
            timeToSpawn -= Time.deltaTime;
        }        
    }

    private void CreateNextWave()
    {
        enemiesToSpawn = new List<GameObject>();
        spawnNumber = (int)(waveNumber * waveDificulty + 5);
        enemyStatModifiyer = waveNumber * waveDificulty;
        for (int i = 0; i < spawnNumber; i++)
        {
            enemiesToSpawn.Add(enemies[Random.Range(0, enemies.Length)]);
        }
    }

    private bool IsWaveDone()
    {
        if (enemiesRemaning + enemiesToSpawn.Count <= 0)
        {
            return true;
        }
        return false;
    }

    private void WaveComplete()
    {
        
        waveRunning = false;
    }
}

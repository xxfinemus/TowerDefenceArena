using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveControl : MonoBehaviour 
{
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
    private int enemiesRemaning;
    private bool waveCanStart;
    private float timeToSpawn;

    public int EnemiesRemaning
    {
        set { enemiesRemaning = value; }
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
	void Start () 
    {
        waveCanStart = true;
        waveRunning = false;
        enemiesRemaning = 0;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (IsWaveDone())
        {
            WaveComplete();
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
            GameObject obj = GenericObjectPoolScript.current.GetPooledObject();
            if (obj == null)
            {
                return;
            }
            obj.transform.position = transform.position;
            obj.SetActive(true);
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

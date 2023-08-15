using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyPoolManager : MonoBehaviour
{
    private StahlGameManager gameManager;
    private TimerManager timerManager;

    [SerializeField]private List<EnemyPool> enemyPools;
    private List<bool> enemyPoolSpawnHasActivated;
    [Header("Totalnya sama kayak total pool di child - dimulai dr 1, 0 = 0 - Buat tau kapan (dr detik berapa) spawn dimulai - DALAM DETIK")]
    [SerializeField]private float[] spawnTimeMark;
    private bool isFirstTimeGameStart = true;

    private void Awake() 
    {
        if(enemyPools == null) enemyPools = new List<EnemyPool>();
        enemyPoolSpawnHasActivated = new List<bool>();
        for(int i=0;i<transform.childCount;i++)
        {
            if(transform.childCount != enemyPools.Count)
            {
                enemyPools.Add(transform.GetChild(i).
                GetComponent<EnemyPool>());
            }
            enemyPoolSpawnHasActivated.Add(false);
        }
    }
    private void Start() {
        gameManager = StahlGameManager.Instance;
        gameManager.OnGameStart += gameManager_OnGameStart;
        gameManager.OnGameStop += gameManager_OnGameStop;

        timerManager = TimerManager.Instance;
        
        DebugError();
    }
    private void DebugError()
    {
        if(enemyPools == null) Debug.LogError("List enemyPools masih kosong di EnemyPoolManager nama" + gameObject.name);
        if(spawnTimeMark == null) Debug.LogError("List spawnTimeMark masih kosong di EnemyPoolManager nama" + gameObject.name);
        if(enemyPoolSpawnHasActivated == null) Debug.LogError("List enemyPoolSpawnChecker masih kosong di EnemyPoolManager nama" + gameObject.name);
        if(!gameManager) Debug.LogError("StalhGameManager gameManager masih kosong di EnemyPoolManager nama" + gameObject.name);
        if(!timerManager) Debug.LogError("TimerManager timerManager masih kosong di EnemyPoolManager nama" + gameObject.name);
    }
    private void Update() 
    {
        for(int i=0; i<enemyPoolSpawnHasActivated.Count;i++)
        {
            if(!enemyPoolSpawnHasActivated[i])
            {
                if(timerManager.GetTime() >= spawnTimeMark[i])
                {
                    enemyPoolSpawnHasActivated[i] = true;
                    enemyPools[i].StartSpawn();
                }
            }
        }
    }
    private void gameManager_OnGameStart(object sender, EventArgs e)
    {
        if(isFirstTimeGameStart)
        {
            isFirstTimeGameStart = false;
            enemyPoolSpawnHasActivated[0] = true;
            enemyPools[0].StartSpawn();
        }
        else
        {
            for(int i=0;i<enemyPoolSpawnHasActivated.Count;i++)
            {
                if(enemyPoolSpawnHasActivated[i])
                {
                    enemyPools[i].StartSpawn();
                }
            }
        }
    }
    private void gameManager_OnGameStop(object sender, EventArgs e)
    {
        foreach(EnemyPool enemyPool in enemyPools)
        {
            if(enemyPool.GetCanSpawn()) enemyPool.StopSpawn();
        }
    }

    

}

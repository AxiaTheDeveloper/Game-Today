using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//kita buat yang pemimpin utamanya, bisa ngatur byk enemy pool, ini cuma jadi ya isi semua func spawn etc

public class EnemyPool : MonoBehaviour
{
    [SerializeField]private Transform player;
    [SerializeField]private int totalEnemySpawned;
    private int totalActive = 0;
    private bool notAllActive = true;
    [SerializeField]private Transform enemyPrefab;
    [SerializeField]private bool isGroupEnemy;
    [SerializeField]private float startSpawnDelay, enemySpawnDelay;
    private bool canSpawn = false;
    [SerializeField]private List<Transform> enemies;

    private Collider spawnArea;

    private void Awake() 
    {
        if(!player) player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        spawnArea = GetComponent<Collider>();
        enemies = new List<Transform>();
        CreatePool();
    }
    private void Start() 
    {
        DebugError();
    }
    private void DebugError()
    {
        if(!spawnArea) Debug.LogError("Collider spawnArea masih kosong di EnemyPool nama" + gameObject.name);
        if(enemies == null) Debug.LogError("List enemies masih kosong di EnemyPool nama" + gameObject.name);
        if(totalEnemySpawned == 0) Debug.LogError("Spawner enemies masih 0 totalspawnnya di EnemyPool nama" + gameObject.name);
        if(!enemyPrefab) Debug.LogError("Transform enemyPrefab masih kosong di EnemyPool nama" + gameObject.name);
        if(!player) Debug.LogError("Transform player masih kosong di EnemyPool nama" + gameObject.name);
    }
    private void CreatePool()
    {
        for(int i=0;i<totalEnemySpawned;i++)
        {
            Transform enemy = Instantiate(enemyPrefab, this.gameObject.transform);
            enemy.gameObject.SetActive(false);
            if(!isGroupEnemy)
            {
                enemy.GetComponent<EnemyMovement>().GiveTransformPlayer(player);
                enemy.position = transform.position;
                enemy.rotation = Quaternion.Euler(0f,0f,0f);
            }
            else
            {
                enemy.position = transform.position;
                enemy.rotation = Quaternion.Euler(0f,0f,0f);
                for(int j=0;j<enemy.childCount;j++)
                {
                    enemy.GetChild(j).GetComponent<EnemyMovement>().GiveTransformPlayer(player);
                    enemy.GetChild(j).position = enemy.GetChild(j).GetComponent<EnemyIdentity>().GetStartPosition();
                    enemy.GetChild(j).rotation = Quaternion.Euler(0f,0f,0f);
                }
            }
            enemies.Add(enemy);
        }
    }
    public void StartSpawn()
    {
        canSpawn = true;
        StartCoroutine(StartSpawning());
    }
    public void StopSpawn()
    {
        canSpawn = false;
        StopCoroutine(StartSpawning());
    }
    public bool GetCanSpawn()
    {
        return canSpawn;
    }
    public void ReduceTotalActive()
    {
        totalActive -= 1;
        if(!notAllActive) notAllActive = true;
    }
    private IEnumerator StartSpawning()
    {
        yield return new WaitForSeconds(startSpawnDelay);
        while(canSpawn)
        {
            if(notAllActive)
            {
                foreach(Transform enemy in enemies)
                {
                    if(!canSpawn)
                    {
                        break;
                    }
                    if(!enemy.gameObject.activeSelf)
                    {
                        totalActive++;
                        if(totalActive == totalEnemySpawned)
                        {
                            notAllActive = false;
                        }

                        //Reset position rotation di spawner
                        enemy.position = new Vector3(Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x), transform.position.y, Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z));
                        enemy.rotation = Quaternion.Euler(0f,0f,0f);

                        //Reset EnemyIdentity
                        if(!isGroupEnemy)
                        {
                            EnemyIdentity enemyIdentity = enemy.GetComponent<EnemyIdentity>();
                            enemyIdentity.RestoreHealth();
                            enemyIdentity.ChangeIsAlive();
                        }
                        else
                        {
                            for(int j=0;j<enemy.childCount;j++)
                            {
                                EnemyIdentity enemyIdentity = enemy.GetChild(j).GetComponent<EnemyIdentity>();
                                enemyIdentity.RestoreHealth();
                                enemyIdentity.ChangeIsAlive();
                                enemy.GetChild(j).localPosition = enemyIdentity.GetStartPosition();
                                enemy.GetChild(j).rotation = Quaternion.Euler(0f,0f,0f);
                            }
                        }
                        enemy.gameObject.SetActive(true);
                    }
                    yield return new WaitForSeconds(enemySpawnDelay);
                }
            }
            yield return null;
        }
    }

    

}

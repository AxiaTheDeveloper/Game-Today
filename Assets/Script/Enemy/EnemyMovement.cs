using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private StahlGameManager gameManager;
    private EnemyIdentity enemyIdentity;

    [SerializeField]private NavMeshAgent enemy;
    [SerializeField]private Transform player;
    [SerializeField]private float moveSpeed;
    private Transform visualGameObject;
    private Vector3 playerToEnemy;
    // private float cross;

    private void Awake() {
        enemy = GetComponent<NavMeshAgent>();
        enemyIdentity = GetComponent<EnemyIdentity>();
        enemy.speed = moveSpeed;
        visualGameObject = transform.GetChild(0).transform;
    }
    private void Start() {
        gameManager = StahlGameManager.Instance;
        
        DebugError();
    }
    private void DebugError()
    {
        if(!gameManager) Debug.LogError("StahlGameManager gameManager masih kosong di EnemyMovement nama" + gameObject.name);
        if(!enemy) Debug.LogError("NavMeshAgent enemy masih kosong di EnemyMovement nama" + gameObject.name);
        if(!enemyIdentity) Debug.LogError("EnemyIdentity enemyIdentity masih kosong di EnemyMovement nama" + gameObject.name);
        if(!player) Debug.LogError("Transform player masih kosong di EnemyMovement nama" + gameObject.name);
        if(!visualGameObject) Debug.LogError("Transform visualGameObject masih kosong di EnemyMovement nama" + gameObject.name);
    }
    private void Update()
    {
        if(gameManager.isStart()){
            if(enemyIdentity.GetIsAlive())
            {
                if(enemy.isStopped) enemy.isStopped = false;
            }
            else
            {
                if(!enemy.isStopped) enemy.isStopped = true;
            }
            enemy.SetDestination(player.position);
            playerToEnemy = transform.position - player.position;
            // Debug.Log("Before "+playerToEnemy + " " + player.rotation);
            playerToEnemy = Quaternion.Inverse(player.rotation) * playerToEnemy;

            // Debug.Log("After "+playerToEnemy + " " + Quaternion.Inverse(player.rotation));
            if(playerToEnemy.x > 0)
            {
                visualGameObject.localScale = new Vector3(-1f, 1f, 1f);
            }
            else if(playerToEnemy.x < 0)
            {
                visualGameObject.localScale = new Vector3(1f, 1f, 1f);
            }
        }
        else{
            if(!enemy.isStopped)
            {
                enemy.isStopped = true;
                enemy.velocity = Vector3.zero;
            }
        }
        
    }
    public void GiveTransformPlayer(Transform playerNow)
    {
        player = playerNow;
    }
}

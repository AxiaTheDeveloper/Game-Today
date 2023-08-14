using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private StahlGameManager gameManager;

    [SerializeField]private NavMeshAgent enemy;
    [SerializeField]private Transform player;
    [SerializeField]private float moveSpeed;
    private Transform visualGameObject;
    private Vector3 playerToEnemy;
    // private float cross;

    private void Awake() {
        enemy = GetComponent<NavMeshAgent>();
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
        if(!player) Debug.LogError("Transform player masih kosong di EnemyMovement nama" + gameObject.name);
        if(!visualGameObject) Debug.LogError("Transform visualGameObject masih kosong di EnemyMovement nama" + gameObject.name);
    }
    private void Update()
    {
        if(gameManager.isStart()){
            if(enemy.isStopped) enemy.isStopped = false;
            enemy.SetDestination(player.position);
            playerToEnemy = transform.position - player.position;
            Debug.Log("Before "+playerToEnemy + " " + player.rotation);
            playerToEnemy = Quaternion.Inverse(player.rotation) * playerToEnemy;

            Debug.Log("After "+playerToEnemy + " " + Quaternion.Inverse(player.rotation));
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
}

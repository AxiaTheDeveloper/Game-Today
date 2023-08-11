using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]private NavMeshAgent enemy;
    [SerializeField]private Transform player;
    private void Awake() {
        enemy = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        enemy.SetDestination(player.position);
    }
}

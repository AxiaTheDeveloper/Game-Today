using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private StahlGameManager gameManager;

    [SerializeField]private float enemyAttackDamage;
    private bool canAttack = false;
    [SerializeField]private float attackCooldown;
    private PlayerIdentity playerIdentity;

    private void Start()
    {
        gameManager = StahlGameManager.Instance;
        DebugError();
    }
    private void DebugError()
    {
        if(!gameManager) Debug.LogError("StahlGameManager gameManager masih kosong di EnemyAttack nama" + gameObject.name);
    }
    private void Update()
    {
        if(gameManager.isStart())
        {
            if(playerIdentity)
            {
                AttackPlayer();
            }
        }
        
    }
    private void AttackPlayer()
    {
        if(canAttack)
        {
            playerIdentity.ChangePlayerHealth(-enemyAttackDamage);
            canAttack = false;
            StartCoroutine(AttackCooldown());
        } 
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            playerIdentity = other.GetComponent<PlayerIdentity>();
            canAttack = true;
            
        }
    }
    private void OnTriggerExit(Collider other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            playerIdentity = null;
            canAttack = false;
        }
    }
    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

}

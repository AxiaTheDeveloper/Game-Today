using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow
{
    public void Attack(ShootObjectPool shootPool)
    {
        shootPool.IsCanStartShootingAgain();
        shootPool.StartShooting();
    }
}
public class Sword
{
    public void Attack()
    {
        Debug.Log("Sword");
    }
    
}

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]private PlayerIdentity playerIdentity;
    private StahlGameManager gameManager;
    private Arrow arrowAttackClass;
    [SerializeField]private ShootObjectPool shootObjectPool;
    private Sword swordAttackClass;
    [SerializeField]private float attackCooldownStart;
    private float attackCooldown;
    private bool canDoAttack = true, canStartCooldown = false;

    [SerializeField]private float attackDamage;

    private void Awake()
    {
        attackCooldown = attackCooldownStart;

        if(playerIdentity.GetCharacterType() == CharacterType.Sword)
        {
            swordAttackClass = new Sword();
        }
        else if(playerIdentity.GetCharacterType() == CharacterType.Arrow)
        {
            arrowAttackClass = new Arrow();
            shootObjectPool = GetComponent<ShootObjectPool>();
            shootObjectPool.OnShootObjectAllUnActive += shootObject_OnShootObjectAllUnActive;
        }
    }

    private void shootObject_OnShootObjectAllUnActive(object sender, EventArgs e)
    {
        canStartCooldown = true;
    }

    private void Start() 
    {
        gameManager = StahlGameManager.Instance;
        DebugError();
    }
    private void DebugError()
    {
        if(!gameManager) Debug.LogError("StahlGameManager gameManager masih kosong di PlayerAttack nama" + gameObject.name);
        if(!playerIdentity) Debug.LogError("PlayerIdentity playerIdentity masih kosong di PlayerAttack nama" + gameObject.name);
        if(swordAttackClass == null && arrowAttackClass == null) Debug.LogError("Class Attack masih belum diset dan masih kosong di PlayerAttack nama" + gameObject.name);
    }
    private void Update() 
    {
        if(gameManager.isStart())
        {
            if(canDoAttack)
            {
                canDoAttack = false;
                if(playerIdentity.GetCharacterType() == CharacterType.Sword)
                {
                    swordAttackClass.Attack();
                }
                else if(playerIdentity.GetCharacterType() == CharacterType.Arrow)
                {
                    arrowAttackClass.Attack(shootObjectPool);
                }
            }
            else{
                if(canStartCooldown)
                {
                    if(attackCooldown > 0) attackCooldown -= Time.deltaTime;
                    else
                    {
                        canStartCooldown = false;
                        canDoAttack = true;
                        attackCooldown = attackCooldownStart;
                    }
                }
                
            }
        }
    }
    public float GetAttackDamage()
    {
        return attackDamage;
    }
    
}

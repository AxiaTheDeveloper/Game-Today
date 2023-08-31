using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum CharacterType
{
    Sword, Arrow
}

public class PlayerIdentity : MonoBehaviour
{
    public static PlayerIdentity Instance {get; private set;}
    private StahlGameManager gameManager;

    [SerializeField]private string characterName;
    [SerializeField]private CharacterType characterType;
    [SerializeField]private float healthMax;
    private float health;
    private int enemyTotalDead = 0;
    public event EventHandler<OnChangeHealthEventArgs> OnChangeHealth;
    
    public class OnChangeHealthEventArgs : EventArgs{
        public float playerHealthNormalized;
    }
    private void Awake() {
        Instance = this;
    }

    private void Start()
    {
        health = healthMax;
        gameManager = StahlGameManager.Instance;
        DebugError();
    }
    private void DebugError()
    {
        if(!gameManager) Debug.LogError("StahlGameManager gameManager masih kosong di PlayerIdentity nama" + gameObject.name);
        if(health <= 0) Debug.LogError("Health lupa dimasukkin datanya aka masih kosong di PlayerIdentity nama" + gameObject.name);
    }
    public void ChangePlayerHealth(float change)
    {
        if(gameManager.isStart())
        {
            if(health > 0) health += change;
            else if(health < 0)
            {
                health = 0;
            }
            if(health == 0)
            {
                StahlGameManager.Instance.ChangeState_Dead();
            }
            OnChangeHealth?.Invoke(this, new OnChangeHealthEventArgs{
            playerHealthNormalized = (float)health / healthMax
        });
             
        }
        
    }
    public CharacterType GetCharacterType()
    {
        return characterType;
    }
    public void AddEnemyTotalDead()
    {
        enemyTotalDead++;
    }
    public int GetEnemyTotalDead()
    {
        return enemyTotalDead;
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Bot1, Bot2
}
public class EnemyIdentity : MonoBehaviour
{
    private StahlGameManager gameManager;
    [SerializeField]private EnemyType enemyType;
    [SerializeField]private float health;
    
    private void Start() 
    {
        gameManager = StahlGameManager.Instance;
        DebugError();  
    }
    private void DebugError()
    {
        if(!gameManager) Debug.LogError("StahlGameManager gameManager masih kosong di EnemyIdentity nama" + gameObject.name);
        if(health <= 0) Debug.LogError("Health lupa dimasukkin datanya aka masih kosong di EnemyIdentity nama" + gameObject.name);
    }
    public void ChangeEnemyHealth(float change)
    {
        if(gameManager.isStart())
        {
            if(health > 0) health += change;
            if(health < 0) health = 0;
        }
    }

}

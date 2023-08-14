using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterType
{
    Sword, Arrow
}

public enum AttackType
{
    Melee, Range
}
public class PlayerIdentity : MonoBehaviour
{
    private StahlGameManager gameManager;

    [SerializeField]private CharacterType characterType;
    [SerializeField]private AttackType attackType;
    [SerializeField]private string characterName;
    [SerializeField]private float health;

    private void Start()
    {
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
            if(health < 0) health = 0;
        }
        
    }
}

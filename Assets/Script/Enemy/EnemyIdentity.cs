using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum EnemyType
{
    Bot1, Bot2
}
public class EnemyIdentity : MonoBehaviour
{
    private StahlGameManager gameManager;

    [SerializeField]private EnemyType enemyType;
    [Header("Kalau ada di dalam group, isGroupTyped dicentang")]
    [SerializeField]private bool isGroupTyped; //kalau true berarti kalo mati ntr suru kirim event OnDead
    public event EventHandler OnDead;//kirim ke GroupEnemyActiveSelfChecker

    [SerializeField]private float maxhealth;
    private bool isAlive = true;
    private float health;

    [SerializeField]private Vector3 startLocation;
    private Transform visualGameObject;

    private void Awake() 
    {
        visualGameObject = transform.GetChild(0).transform;
        RestoreHealth();
        startLocation = transform.localPosition;
    }
    
    private void Start() 
    {
        gameManager = StahlGameManager.Instance;
        DebugError();  
    }
    private void DebugError()
    {
        if(!gameManager) Debug.LogError("StahlGameManager gameManager masih kosong di EnemyIdentity nama" + gameObject.name);
        if(maxhealth <= 0) Debug.LogError("Health lupa dimasukkin datanya aka masih kosong di EnemyIdentity nama" + gameObject.name);
        if(!visualGameObject) Debug.LogError("Transform visualGameObject masih kosong di EnemyIdentity nama" + gameObject.name);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            ChangeEnemyHealth(-10);
        }
        if(isAlive && health == 0)
        {
            isAlive = false;
            LeanTween.color(visualGameObject.gameObject, new Color(1,1,1,0), 0.5f).setOnComplete(
                ()=> Dead1()
            );   
        }
    }
    private void Dead1()
    {
        gameObject.SetActive(false);
        LeanTween.color(visualGameObject.gameObject, new Color(1,1,1,1), 0f).setOnComplete(
            ()=> Dead2()
        );
    }
    private void Dead2()
    {
        GetComponentInParent<EnemyPool>().ReduceTotalActive();
        if(isGroupTyped) OnDead?.Invoke(this,EventArgs.Empty);
    }
    public void ChangeEnemyHealth(float change)
    {
        if(gameManager.isStart())
        {
            if(health > 0) health += change;
            if(health < 0)
            {
                health = 0;
            }
        }
    }
    public void ChangeIsAlive()
    {
        isAlive = true;
    }
    public bool GetIsAlive()
    {
        return isAlive;
    }
    public void RestoreHealth()
    {
        health = maxhealth;
    }
    public Vector3 GetStartPosition()
    {
        return startLocation;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum ShootObjectType
{
    Arrow, Bullet
}

public class ShootObject : MonoBehaviour
{
    private StahlGameManager gameManager;

    public event EventHandler OnDurationFinished;// kirim ke ShootObjectPool
    [SerializeField]private ShootObjectType shootObjectType;
    [Header("Speed harus sama dengan atau lebih besar dari speed pemain biar pemain bs ga catch up ama shootnya")]
    [SerializeField]private float movingSpeed;
    private float shootObjectDuration;
    private Vector3 direction, saveLastDirection;
    private Quaternion startRotation;
    private Vector3 startPosition;

    [Header("Kalau ada di dalam group, isGroupTyped dicentang")]
    [SerializeField]private bool isGroupTyped; //kalau true berarti kalo mati ntr suru kirim event OnDead

    private float damage;

    private void Awake() 
    {
        startRotation = transform.rotation;
        startPosition = transform.localPosition;
    }

    private void Start()
    {
        gameManager = StahlGameManager.Instance;
    }
    
    private void Update()
    {
        if(gameManager.isStart())
        {
            transform.position += (direction * movingSpeed * Time.deltaTime);
            if(shootObjectDuration > 0)
            {
                shootObjectDuration -= Time.deltaTime;
            }
            else{
                gameObject.SetActive(false);
                OnDurationFinished?.Invoke(this, EventArgs.Empty);
            }
        }
        else
        {
            transform.position += (direction * 0 * Time.deltaTime);
        }
    }

    public void SetShootDirection(Vector3 changeDirection, float shootObjDuration)
    {
        shootObjectDuration = shootObjDuration;
        direction = changeDirection;
    }
    public Quaternion GetStartRotation()
    {
        return startRotation;
    }
    public Vector3 GetStartPosition()
    {
        return startPosition;
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject);
        if(other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log(other.gameObject);
            EnemyIdentity enemyIdentity = other.GetComponent<EnemyIdentity>();
            enemyIdentity.ChangeEnemyHealth(-damage);
        }
        if(other.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("nabrak tembok");
            gameObject.SetActive(false);
            OnDurationFinished?.Invoke(this, EventArgs.Empty);
            
        }
    }
    public void ChangeDamage(float playerDamage)
    {
        damage = playerDamage;
    }
}

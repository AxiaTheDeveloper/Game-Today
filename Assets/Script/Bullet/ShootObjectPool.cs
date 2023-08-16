using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShootObjectPool : MonoBehaviour
{
    [SerializeField]private PlayerMovement playerMovement;
    private PlayerAttack playerAttack;
    private StahlGameManager gameManager;

    public event EventHandler OnShootObjectAllUnActive; // kirim ke PlayerAttack kalau semua ud selesai ditembak
    [SerializeField]private Transform spawnPlaceParent, spawnPosition;
    [SerializeField]private int totalShootObject;
    [SerializeField]private Transform shootObjectPrefab;
    [SerializeField]private float objectShootDelayMax, objectShootDuration;
    private float objShootDelayMax, objShootDelay;
    private bool canShoot = false;
    [SerializeField]private List<Transform> shootObjects, shootObjectsTemp;

    private bool isFirstTimeGameStart = true,isAllObjectShooted = false, canStartCooldownNow = false; 
    private int shootObjectNumber = 0;
    
    private void Awake() {
        playerAttack = GetComponent<PlayerAttack>();
        shootObjects = new List<Transform>();
        CreatePoolShoot(totalShootObject);
    }
    private void Start()
    {
        gameManager = StahlGameManager.Instance;
        gameManager.OnGameStart += gameManager_OnGameStart;
        gameManager.OnGameStop += gameManager_OnGameStop;
        DebugError();
    }

    private void gameManager_OnGameStart(object sender, EventArgs e)
    {
        if(isFirstTimeGameStart) isFirstTimeGameStart = false;
        else
        {
            StartShooting();
        }
    }
    private void gameManager_OnGameStop(object sender, EventArgs e)
    {
        StopShooting();
    }

    

    private void DebugError()
    {
        if(!playerMovement) Debug.LogError("PlayerMovement playerMovement masih kosong di ShootObjectPool nama" + gameObject.name);
        if(shootObjects == null) Debug.LogError("List shootObjects masih kosong di ShootObjectPool nama" + gameObject.name);
        if(totalShootObject == 0) Debug.LogError("Spawner shoot masih 0 totalspawnnya di ShootObjectPool nama" + gameObject.name);
        if(!shootObjectPrefab) Debug.LogError("Transform shootObjectPrefab masih kosong di ShootObjectPool nama" + gameObject.name);
        if(!spawnPlaceParent) Debug.LogError("Transform spawnPlace masih kosong di ShootObjectPool nama" + gameObject.name);
    }
    private void Update() 
    {
        if(gameManager.isStart())
        {
            if(objShootDelay > 0) objShootDelay -= Time.deltaTime;
            else{
                canShoot = true;
            }
            ShootingCommence();
        }
        
    }
    //kalau misal upgrade nambah shoot bs lewat sini
    public void CreatePoolShoot(int totalAddShootObject)
    {
        for(int i=0;i<totalAddShootObject;i++)
        {
            Transform shoot = Instantiate(shootObjectPrefab, spawnPlaceParent);
            shoot.gameObject.SetActive(false);
            shoot.position = transform.position;
            
            ShootObject shoots = shoot.GetComponent<ShootObject>();

            if(shoots)
            {
                shoots.OnDurationFinished += shoot_OnDurationFinished;
                shoot.rotation = shoots.GetStartRotation();
                shoots.ChangeDamage(playerAttack.GetAttackDamage());
            }
            else{
                shoot.rotation = Quaternion.Euler(0f,0f,0f);
                shoot.GetComponent<GroupShootObject>().OnDurationFinished += shoot_OnDurationFinished;
                for(int j=0;j<shoot.childCount;j++)
                {
                    Transform shootChild = shoot.GetChild(j).GetComponent<Transform>();
                    ShootObject shootsChildObject = shootChild.GetComponent<ShootObject>(); 
                    shootChild.localPosition = shootsChildObject.GetStartPosition();
                    shootChild.rotation = shootsChildObject.GetStartRotation();
                    shootsChildObject.ChangeDamage(playerAttack.GetAttackDamage());
                }
            }
            shootObjects.Add(shoot);
        }
    }

    private void shoot_OnDurationFinished(object sender, EventArgs e)
    {
        bool allUnActive = true;
        foreach(Transform shoot in shootObjects)
        {
            if(shoot.gameObject.activeSelf)
            {
                allUnActive = false;
                break;
            }
        }
        if(allUnActive && canStartCooldownNow)
        {
            canStartCooldownNow = false;
            OnShootObjectAllUnActive?.Invoke(this,EventArgs.Empty);
        }
    }

    public void StartShooting()
    {
        if(isAllObjectShooted)
        {
            spawnPosition = transform;
            isAllObjectShooted = false;
            canShoot = true;
            shootObjectNumber = 0;
            shootObjectsTemp = new List<Transform>(shootObjects);
            objShootDelayMax = objectShootDelayMax;
        }
    }

    public void StopShooting()
    {
        canShoot = false;
    }

    private void ShootingCommence()
    {
        while(shootObjectNumber < shootObjectsTemp.Count && canShoot)
        {
            canShoot = false;
            ShootObject shoots = shootObjectsTemp[shootObjectNumber].GetComponent<ShootObject>();
            shootObjectsTemp[shootObjectNumber].position = spawnPosition.position;
            if(shoots)
            {
                shootObjectsTemp[shootObjectNumber].rotation = Quaternion.Euler(0f,playerMovement.gameObject.transform.eulerAngles.y,shoots.GetStartRotation().eulerAngles.z);
                //kalau mau misal d arah yg sama ini dipindah ke pas start shooting
                shoots.SetShootDirection(playerMovement.GetLastDirectionMove(), objectShootDuration);
            }
            else{
                shootObjectsTemp[shootObjectNumber].rotation = Quaternion.Euler(0f,playerMovement.gameObject.transform.eulerAngles.y,0f);
                for(int j=0;j<shootObjectsTemp[shootObjectNumber].childCount;j++)
                {
                    Transform shootChild = shootObjectsTemp[shootObjectNumber].GetChild(j).GetComponent<Transform>();
                    ShootObject shootsChildObject = shootChild.GetComponent<ShootObject>(); 
                    shootChild.localPosition = shootsChildObject.GetStartPosition();
                    shootChild.localRotation = shootsChildObject.GetStartRotation();
                    shootsChildObject.SetShootDirection(playerMovement.GetLastDirectionMove(), objectShootDuration);
                }
            }
            
            
            
            

            shootObjectsTemp[shootObjectNumber].gameObject.SetActive(true);

            shootObjectNumber++;
            objShootDelay = objectShootDelayMax;
        }
        if(shootObjectNumber == shootObjectsTemp.Count) canStartCooldownNow = true;
        
    }
    public void IsCanStartShootingAgain()
    {
        isAllObjectShooted = true;
    }

}

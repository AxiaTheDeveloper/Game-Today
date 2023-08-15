using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GroupEnemyActiveSelfChecker : MonoBehaviour
{
    [SerializeField]private List<EnemyIdentity> enemyList;
    
    private void Awake() 
    {
        if(enemyList == null) enemyList = new List<EnemyIdentity>();
        for(int i=0;i<transform.childCount;i++)
        {
            if(transform.childCount != enemyList.Count)
            {
                EnemyIdentity enemyNow = transform.GetChild(i).
                GetComponent<EnemyIdentity>();
                enemyList.Add(enemyNow);
                enemyNow.OnDead += enemyNow_OnDead;
            }
        }
    }
    private void enemyNow_OnDead(object sender, EventArgs e)
    {
        bool allDead = true;
        for(int i=0;i<enemyList.Count;i++)
        {
            if(enemyList[i].gameObject.activeSelf)
            {
                allDead = false;
                break;
            }
        }
        if(allDead)
        {
            for(int i=0;i<enemyList.Count;i++)
            {
                enemyList[i].gameObject.SetActive(true);
            }
            gameObject.SetActive(false);
        }
    }

}

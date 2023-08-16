using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GroupShootObject : MonoBehaviour
{
    [SerializeField]private List<ShootObject> shootList;
    public event EventHandler OnDurationFinished;// kirim ke shootobjectpool
    private void Awake()
    {
        if(shootList == null) shootList = new List<ShootObject>();
        for(int i=0;i<transform.childCount;i++)
        {
            if(transform.childCount != shootList.Count)
            {
                ShootObject shootObj = transform.GetChild(i).
                GetComponent<ShootObject>();
                shootList.Add(shootObj);
                shootObj.OnDurationFinished += shootObj_OnDurationFinished;
            }
        }
    }

    private void shootObj_OnDurationFinished(object sender, EventArgs e)
    {
        bool allUnActive = true;
        for(int i=0;i<shootList.Count;i++)
        {
            if(shootList[i].gameObject.activeSelf)
            {
                allUnActive = false;
                break;
            }
        }
        if(allUnActive)
        {
            for(int i=0;i<shootList.Count;i++)
            {
                shootList[i].gameObject.SetActive(true);
            }
            gameObject.SetActive(false);
            OnDurationFinished?.Invoke(this,EventArgs.Empty);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TimerManager : MonoBehaviour
{
    public static TimerManager Instance{get; private set;}

    [SerializeField]private float timer = 0f;
    [SerializeField]private TextMeshProUGUI textTimer;

    private void Awake() {
        Instance = this;
    }
    private void FixedUpdate() {
        if(StahlGameManager.Instance.isStart()){
            timer += Time.fixedDeltaTime;
            var ts = TimeSpan.FromSeconds(timer);
            textTimer.text = string.Format("{0:00}:{1:00}", ts.TotalMinutes, ts.Seconds);
        }
    }
    public float GetTime(){
        return timer;
    }
}

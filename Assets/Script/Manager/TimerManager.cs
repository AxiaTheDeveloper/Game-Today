using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public static TimerManager Instance{get; private set;}

    [SerializeField]private float timer = 0f;

    private void Awake() {
        Instance = this;
    }
    private void FixedUpdate() {
        if(StahlGameManager.Instance.isStart()){
            timer += Time.fixedDeltaTime;
        }
    }
    public float GetTime(){
        return timer;
    }
}

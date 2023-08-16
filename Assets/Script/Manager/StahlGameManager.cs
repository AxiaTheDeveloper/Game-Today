using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StahlGameManager : MonoBehaviour
{
    public static StahlGameManager Instance {get; private set;}

    private GameInput gameInput;
    public event EventHandler OnGameStart, OnGameStop; //OnGameStart - EnemyPoolManager, OnGameStop - EnemyPoolManager
    public enum GameState
    {
        Waiting, Start, Dead, Finish, Pause
    }
    [SerializeField]private GameState state;
    [Header ("Pause")]
    [SerializeField]private bool isPause = false;

    private void Awake() 
    {
        Instance = this;
    }
    private void Start() {
        gameInput = GameInput.Instance;
        StartCoroutine(StartGame());
        
        DebugError();
    }
    private IEnumerator StartGame()
    {
        yield return new WaitForSeconds(0.1f);
        state = GameState.Start;
    }
    private void DebugError()
    {
        if(!gameInput) Debug.LogError("GameInput gameInput masih kosong di StalhGameManager nama" + gameObject.name);
    }
    private void Update() {
        if(gameInput.GetInputPause())
        {
            if(state == GameState.Start || state == GameState.Pause)
            {
                PauseGame();
            }
        }
    }
    public bool isStart()
    {
        return state == GameState.Start;
    }
    public void ChangeState_Start()
    {
        state = GameState.Start;
        OnGameStart?.Invoke(this, EventArgs.Empty);
    }
    public void ChangeState_Dead()
    {
        state = GameState.Dead;
        OnGameStop?.Invoke(this, EventArgs.Empty);
    }
    public void ChangeState_Finish()
    {
        state = GameState.Finish;
        OnGameStop?.Invoke(this, EventArgs.Empty);
    }
    public void PauseGame()
    {
        isPause = !isPause;
        if(isPause)
        {
            Debug.Log("Game is Pause");
            state = GameState.Pause;
            OnGameStop?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Debug.Log("Game is Start");
            state = GameState.Start;
            OnGameStart?.Invoke(this, EventArgs.Empty);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

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
    [SerializeField]private GameObject pauseUI;
    [SerializeField]private DeadUI deadUI;

    private void Awake() 
    {
        
        Instance = this;
    }
    private void Start() {
        deadUI.HideDeadUI();
        gameInput = GameInput.Instance;
        
        pauseUI.SetActive(false);
        DebugError();
    }
    public void StartGameCourotine()
    {
        StartCoroutine(StartGame());
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
        if(state == GameState.Dead)
        {
            if(gameInput.GetInputRestart())
            {
                deadUI.HideDeadScore();
                SceneManager.LoadSceneAsync("Main Game");
            }
            else if(gameInput.GetInputPause())
            {
                Application.Quit();
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
        deadUI.ShowDeadUI();
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
            pauseUI.SetActive(true);
            state = GameState.Pause;
            OnGameStop?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Debug.Log("Game is Start");
            pauseUI.SetActive(false);
            state = GameState.Start;
            OnGameStart?.Invoke(this, EventArgs.Empty);
        }
    }
}

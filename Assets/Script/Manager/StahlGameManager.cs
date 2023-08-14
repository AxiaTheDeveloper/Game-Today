using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StahlGameManager : MonoBehaviour
{
    public static StahlGameManager Instance {get; private set;}
    private GameInput gameInput;

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
        state = GameState.Start;
    }
    private void Start() {
        gameInput = GameInput.Instance;
        DebugError();
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
    }
    public void ChangeState_Dead()
    {
        state = GameState.Dead;
    }
    public void ChangeState_Finish()
    {
        state = GameState.Finish;
    }

    public void PauseGame()
    {
        isPause = !isPause;
        if(isPause)
        {
            Debug.Log("Game is Pause");
            state = GameState.Pause;
        }
        else
        {
            Debug.Log("Game is Start");
            state =GameState.Start;
        }
    }
}

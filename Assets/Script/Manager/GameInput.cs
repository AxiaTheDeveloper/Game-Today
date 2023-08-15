using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance{get;private set;}

    public enum DirectionMovement
    {
        Left,Right,None
    }
    private void Awake() 
    {
        Instance = this;
    }
    public int GetInputChangeCameraView()
    {
        if(Input.GetKey(KeyCode.E)) return 1;
        else if(Input.GetKey(KeyCode.Q)) return -1;
        
        return 0;
    }
    public DirectionMovement GetInputMovement()
    {
        if(Input.GetKey(KeyCode.D)) return DirectionMovement.Right;
        else if(Input.GetKey(KeyCode.A)) return DirectionMovement.Left;
        
        return DirectionMovement.None;
    }
    public bool GetInputPause()
    {
        return Input.GetKeyDown(KeyCode.Escape);
    }
}

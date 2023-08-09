using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance{get;private set;}
    private void Awake() {
        Instance = this;
    }
    public int GetInputChangeCameraView(){
        if(Input.GetKeyDown(KeyCode.E)) return 1;
        else if(Input.GetKeyDown(KeyCode.Q)) return -1;
        
        return 0;
    }
}

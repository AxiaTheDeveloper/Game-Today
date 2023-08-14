using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerView : MonoBehaviour
{
    private GameInput gameInput;
    private StahlGameManager gameManager;
    //INGET INI ARAH HADAP KAMERA
    [SerializeField]private float rotateCameraSpeed;
    private float rotationDirection;
    private Quaternion rotationCamera;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        transform.eulerAngles = new Vector3(0f, 0f, 0f);
    }
    
    private void Start()
    {
        gameManager = StahlGameManager.Instance;
        gameInput = GameInput.Instance;
        DebugError();
    }
    private void DebugError()
    {
        if(!rb) Debug.LogError("Rigidbody rb masih kosong di CameraControllerView nama" + gameObject.name);
        if(!gameInput) Debug.LogError("GameInput gameInput masih kosong di CameraControllerView nama" + gameObject.name);
        if(!gameManager) Debug.LogError("StahlGameManager gameManager masih kosong di CameraControllerView nama" + gameObject.name);
    }
    private void Update()
    {
        if(gameManager.isStart())
        {
            rotationDirection = gameInput.GetInputChangeCameraView();
        }
        else
        {
            if(rotationDirection != 0) rotationDirection = 0;
        }
        
    }
    private void FixedUpdate()
    {
        Rotate();
    }
    private void Rotate()
    {
        rotationCamera = Quaternion.Euler(0f, rotateCameraSpeed * rotationDirection * Time.fixedDeltaTime, 0f);
        rb.MoveRotation(rb.rotation * rotationCamera);
    }
    
}

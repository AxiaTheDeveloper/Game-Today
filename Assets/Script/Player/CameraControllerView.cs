using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerView : MonoBehaviour
{
    //scrape jd player control view
    private GameInput gameInput;
    //INGET INI ARAH HADAP KAMERA
    public enum Direction{
        East, SouthEast, South, SouthWest, West, NorthWest, North, NorthEast
    }
    private Direction directionNow = Direction.North;
    [SerializeField]private float rotateCameraSpeed;
    private float rotationDirection;
    private bool hasFinishRotate;
    private Quaternion rotationCamera;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        transform.eulerAngles = new Vector3(0f, 0f, 0f);
        hasFinishRotate = true;
    }
    private void Start()
    {
        gameInput = GameInput.Instance;
        
    }
    private void Update()
    {
        rotationDirection = gameInput.GetInputChangeCameraView();
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

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
        DirectionChecker();
        // Debug.Log(directionNow + " and " + transform.rotation.y);
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
    private void DirectionChecker()
    {
        if(transform.rotation.y == 0f)
        {
            directionNow = Direction.North;
        }
        else if(transform.rotation.y > 0f && transform.rotation.y < 90f)
        {
            directionNow = Direction.NorthEast;
        }
        else if(transform.rotation.y == 90f)
        {
            directionNow = Direction.East;
        }
        else if(transform.rotation.y > 90f && transform.rotation.y < 180f)
        {
            directionNow = Direction.SouthEast;
        }
        else if(transform.rotation.y == 180f || transform.rotation.y == -180f)
        {
            directionNow = Direction.South;
        }
        else if(transform.rotation.y > -180f && transform.rotation.y < -90f)
        {
            directionNow = Direction.SouthEast;
        }
        else if(transform.rotation.y == -90)
        {
            directionNow = Direction.West;
        }
        else if(transform.rotation.y > -90f && transform.rotation.y < 0)
        {
            directionNow = Direction.SouthEast;
        }
        

    }
    public Direction GetDirection()
    {
        return directionNow;
    }
    
}

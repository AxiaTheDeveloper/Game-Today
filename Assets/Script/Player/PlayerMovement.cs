using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CameraControllerView cameraController;
    private GameInput gameInput;
    private Rigidbody rb;

    [Header("Player Movement")]
    private float directionMove;
    [SerializeField]private float moveSpeed;
    [Header("Player Sprite Rotation")]
    private Transform visualGameObject;
    

    private void Awake()
    {
        visualGameObject = gameObject.transform.GetChild(0).transform;
        cameraController = GetComponent<CameraControllerView>();
        rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        gameInput = GameInput.Instance;
    }
    //north :        kiri -1 x            kanan  1 x

    //northeast :    kiri  1 x -1 z       kanan -1 x  1 z

    //east :         kiri       1 z       kanan      -1 z

    //southeast :    kiri -1 x -1 z       kanan  1 x  1 z

    //south :        kiri  1 x            kanan -1 x

    //southwest :    kiri -1 x 1 z        kanan  1 x  -1 z

    //west :         kiri      -1 z       kanan       1 z

    //northwest :    kiri  1 x  1 z       kanan -1 x -1 z
    private void Update()
    {
        
        if(gameInput.GetInputMovement() == GameInput.DirectionMovement.Left)
        {
            visualGameObject.localScale = new Vector3(-1f, 1f, 1f);
            directionMove = -1f;
            // // directionMove.Set(-1f,0f,0f);
            // if(cameraController.GetDirection() == CameraControllerView.Direction.North)
            // {
            //     directionMove.Set(-1f,0f,0f);
            // }
            // else if(cameraController.GetDirection() == CameraControllerView.Direction.NorthEast)
            // {
            //     directionMove.Set(1f,0f,-1f);
            //     directionMove = directionMove.normalized;
            // }
            // else if(cameraController.GetDirection() == CameraControllerView.Direction.East)
            // {
            //     directionMove.Set(0f,0f,1f);
            // }
            // else if(cameraController.GetDirection() == CameraControllerView.Direction.SouthEast)
            // {
            //     directionMove.Set(-1f,0f,-1f);
            //     directionMove = directionMove.normalized;
            // }
            // else if(cameraController.GetDirection() == CameraControllerView.Direction.South)
            // {
            //     directionMove.Set(1f,0f,0f);
            // }
            // else if(cameraController.GetDirection() == CameraControllerView.Direction.SouthWest)
            // {
            //     directionMove.Set(-1f,0f,1f);
            //     directionMove = directionMove.normalized;
            // }
            // else if(cameraController.GetDirection() == CameraControllerView.Direction.West)
            // {
            //     directionMove.Set(0f,0f,-1f);
            // }
            // else if(cameraController.GetDirection() == CameraControllerView.Direction.NorthWest)
            // {
            //     directionMove.Set(1f,0f,1f);
            //     directionMove = directionMove.normalized;
            // }
        }
        else if(gameInput.GetInputMovement() == GameInput.DirectionMovement.Right)
        {
            visualGameObject.transform.localScale = new Vector3(1f, 1f, 1f);
            directionMove = 1f;
            // if(cameraController.GetDirection() == CameraControllerView.Direction.North)
            // {
            //     directionMove.Set(1f,0f,0f);
            // }
            // else if(cameraController.GetDirection() == CameraControllerView.Direction.NorthEast)
            // {
            //     directionMove.Set(-1f,0f,1f);
            //     directionMove = directionMove.normalized;
            // }
            // else if(cameraController.GetDirection() == CameraControllerView.Direction.East)
            // {
            //     directionMove.Set(0f,0f,-1f);
            // }
            // else if(cameraController.GetDirection() == CameraControllerView.Direction.SouthEast)
            // {
            //     directionMove.Set(1f,0f,1f);
            //     directionMove = directionMove.normalized;
            // }
            // else if(cameraController.GetDirection() == CameraControllerView.Direction.South)
            // {
            //     directionMove.Set(-1f,0f,0f);
            // }
            // else if(cameraController.GetDirection() == CameraControllerView.Direction.SouthWest)
            // {
            //     directionMove.Set(1f,0f,-1f);
            //     directionMove = directionMove.normalized;
            // }
            // else if(cameraController.GetDirection() == CameraControllerView.Direction.West)
            // {
            //     directionMove.Set(0f,0f,1f);
            // }
            // else if(cameraController.GetDirection() == CameraControllerView.Direction.NorthWest)
            // {
            //     directionMove.Set(-1f,0f,-1f);
            //     directionMove = directionMove.normalized;
            // }
        }
        else if(gameInput.GetInputMovement() == GameInput.DirectionMovement.None)
        {
            directionMove = 0f;
        }
        // transform.Translate(horizontalMovement);
    }
    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 cameraHorizontalMovement = transform.right;
        cameraHorizontalMovement.y = 0f;
        cameraHorizontalMovement = cameraHorizontalMovement.normalized;
        rb.MovePosition(rb.position + cameraHorizontalMovement * directionMove * moveSpeed * Time.fixedDeltaTime);
    }

}

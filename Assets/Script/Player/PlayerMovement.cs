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

    private void Update()
    {
        
        if(gameInput.GetInputMovement() == GameInput.DirectionMovement.Left)
        {
            visualGameObject.localScale = new Vector3(-1f, 1f, 1f);
            directionMove = -1f;
            
        }
        else if(gameInput.GetInputMovement() == GameInput.DirectionMovement.Right)
        {
            visualGameObject.transform.localScale = new Vector3(1f, 1f, 1f);
            directionMove = 1f;
            
        }
        else if(gameInput.GetInputMovement() == GameInput.DirectionMovement.None)
        {
            directionMove = 0f;
        }
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

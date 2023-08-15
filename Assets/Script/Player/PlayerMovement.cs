using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private StahlGameManager gameManager;

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
        gameManager = StahlGameManager.Instance;
        DebugError();
    }
    private void DebugError()
    {
        if(!gameManager) Debug.LogError("StahlGameManager gameManager masih kosong di PlayerMovement nama" + gameObject.name);
        if(!gameInput) Debug.LogError("GameInput gameInput masih kosong di PlayerMovement nama" + gameObject.name);
        if(!visualGameObject) Debug.LogError("Transform VisualGameObject di PlayerMovement kosong nama" + gameObject.name);
        if(!cameraController) Debug.LogError("CameraControllerView cameraController di PlayerMovement kosong nama" + gameObject.name);
        if(!rb) Debug.LogError("Rigidbody rb di PlayerMovement kosong nama" + gameObject.name);
    }
    private void Update()
    {
        if(gameManager.isStart())
        {
            GetDirection();
        }
        else
        {
            if(directionMove != 0) directionMove = 0f;
        }
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void GetDirection()
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
    private void Move()
    {
        Vector3 cameraHorizontalMovement = transform.right;
        cameraHorizontalMovement.y = 0f;
        cameraHorizontalMovement = cameraHorizontalMovement.normalized;
        rb.MovePosition(rb.position + cameraHorizontalMovement * directionMove * moveSpeed * Time.fixedDeltaTime);
    }

}

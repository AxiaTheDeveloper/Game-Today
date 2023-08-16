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
    private float directionMove, lastDirectionMove = 1f;
    [SerializeField]private float moveSpeed;
    private Vector3 cameraHorizontalMovement;
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
            if(directionMove != 0){
                lastDirectionMove = directionMove;
                directionMove = 0f;
            }
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
            lastDirectionMove = directionMove;
        }
        else if(gameInput.GetInputMovement() == GameInput.DirectionMovement.Right)
        {
            visualGameObject.transform.localScale = new Vector3(1f, 1f, 1f);
            directionMove = 1f;
            lastDirectionMove = directionMove;
        }
        else if(gameInput.GetInputMovement() == GameInput.DirectionMovement.None)
        {
            if(directionMove != 0)lastDirectionMove = directionMove;
            directionMove = 0f;
        }
    }
    private void Move()
    {
        cameraHorizontalMovement = transform.right;
        cameraHorizontalMovement.y = 0f;
        cameraHorizontalMovement = cameraHorizontalMovement.normalized;
        rb.MovePosition(rb.position + cameraHorizontalMovement * directionMove * moveSpeed * Time.fixedDeltaTime);
    }
    public Vector3 GetLastDirectionMove()
    {
        // Debug.Log(lastDirectionMove);
        return lastDirectionMove * cameraHorizontalMovement;
    }

}

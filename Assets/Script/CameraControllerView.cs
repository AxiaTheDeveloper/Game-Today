using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerView : MonoBehaviour
{
    //scrape jd player control view
    private GameInput gameInput;
    public enum Direction{
        North, South, East, West
    }
    private Direction directionNow = Direction.North;
    [SerializeField]private float rotateCameraSpeed;
    private bool hasFinishRotate;
    private void Awake() {
        transform.eulerAngles = new Vector3(0f, 0f, 0f);
        hasFinishRotate = true;
    }
    private void Start() {
        gameInput = GameInput.Instance;
        
    }
    void Update()
    {

        if(gameInput.GetInputChangeCameraView() == 1 && hasFinishRotate){
            hasFinishRotate = false;
            if(directionNow == Direction.North){
                directionNow = Direction.East;
            }
            else if(directionNow == Direction.East){
                directionNow = Direction.South;
            }
            else if(directionNow == Direction.South){
                directionNow = Direction.West;
            }
            else if(directionNow == Direction.West){
                directionNow = Direction.North;
            }
            RotateCamera();
        }
        else if(gameInput.GetInputChangeCameraView() == -1 && hasFinishRotate){
            hasFinishRotate = false;
            if(directionNow == Direction.North){
                directionNow = Direction.West;
            }
            else if(directionNow == Direction.West){
                directionNow = Direction.South;
            }
            else if(directionNow == Direction.South){
                directionNow = Direction.East;
            }
            else if(directionNow == Direction.East){
                directionNow = Direction.North;
            }
            RotateCamera();
        }
    }

    private void RotateCamera(){
        if(directionNow == Direction.North){
            // transform.eulerAngles = new Vector3(0f, 0f, 0f);
            LeanTween.rotateY(this.gameObject, 0f, rotateCameraSpeed).setOnComplete(
                ()=> hasFinishRotate = true
            );
        }
        else if(directionNow == Direction.East){
            // transform.eulerAngles = new Vector3(0f, 90f, 0f);
            LeanTween.rotateY(this.gameObject, 90f, rotateCameraSpeed).setOnComplete(
                ()=> hasFinishRotate = true
            );
        }
        else if(directionNow == Direction.South){
            // transform.eulerAngles = new Vector3(0f, 180f, 0f);
            LeanTween.rotateY(this.gameObject, 180f, rotateCameraSpeed).setOnComplete(
                ()=> hasFinishRotate = true
            );
        }
        else if(directionNow == Direction.West){
            // transform.eulerAngles = new Vector3(0f, 270f, 0f);
            LeanTween.rotateY(this.gameObject, 270f, rotateCameraSpeed).setOnComplete(
                ()=> hasFinishRotate = true
            );
        }

    }
}

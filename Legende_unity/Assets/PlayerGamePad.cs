using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class PlayerGamePad : MonoBehaviour
{

   PlayerControls controls;
   Vector2 movePlayer;
   Vector2 rotate;
   Animator Player;
   public int SpeedMove;
   public int ForceJump;


    void Start(){

        Player = GameObject.Find("Player").GetComponent<Animator>();
    }

    void Awake(){

        controls = new PlayerControls();

        controls.Gameplay.ButtonX.performed += ctx => Jump();

        controls.Gameplay.Move.performed += ctx => movePlayer = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => movePlayer = Vector2.zero;

        controls.Gameplay.RightStick.performed += ctx => rotate = ctx.ReadValue<Vector2>();
        controls.Gameplay.RightStick.canceled += ctx => rotate = Vector2.zero;
      
    }

    void Update(){

        Vector3 m = new Vector3(movePlayer.x, 0f, movePlayer.y)*SpeedMove* Time.deltaTime;
        transform.Translate(m, Space.World);

        Vector2 r = new Vector2(0f, rotate.x)*100f* Time.deltaTime;
        transform.Rotate(r, Space.World);





        if (movePlayer.y > 0 && movePlayer.y< 1){
            Player.SetBool("isWalking", true);
            Player.SetBool("isRunning", false);
        }

          if (movePlayer.y > 0.8){
            Player.SetBool("isRunning", true);
        }

        if (movePlayer.y == 0){
            Player.SetBool("isWalking", false);
            Player.SetBool("isRunning", false);
        }
          
    }
   

    void Jump(){

        Player.SetBool("isJump", true);
        GetComponent<Rigidbody>().AddForce(new Vector3(0,ForceJump,0), ForceMode.Impulse);
    }

    void OnEnable(){

        controls.Gameplay.Enable();
    }

     void OnDisable(){

        controls.Gameplay.Disable();
    }

}

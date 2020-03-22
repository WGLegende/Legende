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
    public int speedRotation;
    public int ForceJump;
    public GameObject camera_container;

    void Start(){

        Player = GameObject.Find("Player").GetComponent<Animator>();
    }

    void Awake(){

        controls = new PlayerControls();

        controls.Gameplay.ButtonX.performed += ctx => Jump();

        controls.Gameplay.Move.performed += ctx => movePlayer = ctx.ReadValue<Vector2>();// PASSER SUR LE JOYSTICK DE GAUCHE
        controls.Gameplay.Move.canceled += ctx => movePlayer = Vector2.zero;// PASSER SUR LE JOYSTICK DE GAUCHE

        controls.Gameplay.Move.performed += ctx => rotate = ctx.ReadValue<Vector2>(); // REPASSER SUR LE JOYSTICK DE DROITE
        controls.Gameplay.Move.canceled += ctx => rotate = Vector2.zero; // REPASSER SUR LE JOYSTICK DE DROITE
    }

    void Update(){

        // stick.x -1 = left
        // stick.x 1 = right
        // stick.y -1 = down
        // stick.y 1 = up

        if(movePlayer.x < 0 || movePlayer.x > 0 || movePlayer.y < 0 || movePlayer.y > 0){ // Mouvement left stick
            // anim.SetFloat("SpeedMove", Mathf.Abs(movePlayer.y));

            Player.SetBool("isWalking", true);
            Player.SetBool("isRunning", false);

            transform.Translate(movePlayer * SpeedMove  * Time.deltaTime * (movePlayer.y < 0 ? 0.5f : 1f), Space.Self);
        }else if(Player.GetBool("isWalking") || Player.GetBool("isRunning")){
           // anim.SetFloat("SpeedMove", 0);

            Player.SetBool("isWalking", false);
            Player.SetBool("isRunning", false);

        }

        // PAS TOUCHE CONNARD
        if(rotate.x < 0 || rotate.x > 0 || rotate.y < 0 || rotate.y > 0){ // Mouvement right stick
            transform.Rotate(0, rotate.x * speedRotation  * Time.deltaTime, 0, Space.Self); // rotate right/left character.
            if(rotate.y < -0.2 || rotate.y > 0.2){ // Rotate up/Down camera.
                float camera_Y = rotate.y * speedRotation/5  * Time.deltaTime;
                float angle = camera_container.transform.localEulerAngles.x;
                angle = (angle > 180) ? angle - 360 : angle;
                camera_Y = angle < -40 && rotate.y > 0 ? 0 : angle > 20 && rotate.y < 0?  0 : camera_Y;
                camera_container.transform.Rotate(-camera_Y, 0, 0, Space.Self);
            }
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

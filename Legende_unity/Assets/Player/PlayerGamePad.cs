using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;



public class PlayerGamePad : MonoBehaviour
{
    public static PlayerGamePad instance; 
    public bool use_multiple_jump;
   
    public GameObject Bow;
    public GameObject Arrow;
    public GameObject Sword;
    public GameObject Shield;
    public Transform originArrow;
    public GameObject projectile;
    public float puissance_de_tir;
    public float degat_sword;
    public float degat_bow;

    bool isBowman;
    bool isShooting;
    string modePlayer = "noweapon";

    PlayerControls controls;
    Vector2 movePlayer;
    Vector2 rotate;
    public int SpeedMove;
    public int speedRotation;
    public GameObject camera_container;

    Rigidbody player_rigidBody;
    Animator Player_Animator;
    bool playerIsMoving;
    bool cameraIsTurning;
    bool cameraIsBehind;

    public static bool canAttack;
    public static bool canMove;
    public static bool canJump;

    public bool PlayerIsAttack;

    

    private CharacterController characterController;

    private float verticalVelocity;
    public float player_gravity = 1f;
    public float jumpForce = 0.2f;
    private bool hasJump = false;

    //bool toggle;

   
    void Start(){

        instance = this;
        Player_Animator = GetComponent<Animator>();  
        canAttack = true;
        canMove = true;
        canJump = true;
        cameraIsBehind = true;
        characterController = GetComponent<CharacterController>();
        player_gravity/=10f;
        jumpForce/=10f;   

        Player_Animator.SetLayerWeight (1, 0); // layer 1 Sword
        Player_Animator.SetLayerWeight (2, 0); // layer 2 Bow
        Bow.SetActive(false);
        Arrow.SetActive(false);
        Sword.SetActive(false);
        Shield.SetActive(false);
        isBowman = false;
        
    }

    void Awake(){


        controls = new PlayerControls();

        controls.Gameplay.ButtonX.started += ctx => Jump();

        controls.Gameplay.ButtonB.started += ctx => ButtonB();

        controls.Gameplay.Move.performed += ctx => movePlayer = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => movePlayer = Vector2.zero;

        controls.Gameplay.RightStick.performed += ctx => rotate = ctx.ReadValue<Vector2>(); 
        controls.Gameplay.RightStick.canceled += ctx => rotate = Vector2.zero; 

        controls.Gameplay.buttonLT.started += ctx => behindPlayer();

         
    }

    void behindPlayer(){
        camera_container.transform.localEulerAngles = new Vector3(0f,0f,0f);
    }

    void Update(){

        // stick.x -1 = left
        // stick.x 1 = right
        // stick.y -1 = down
        // stick.y 1 = up

        // if(Hinput.anyGamepad.A.justPressed){
        //             Debug.Log("Test A");
        //  }

        //   if(Input.GetKeyDown("p") && modePlayer != "noweapon"){
        //     modePlayer = "noweapon";
        //       Player_Animator.SetTrigger("changeEquipement");
        // } 

        // if(Input.GetKeyDown("o") && modePlayer != "bow"){  
        //     modePlayer = "bow";
        //       Player_Animator.SetTrigger("changeEquipement");
        // }

        // if(Input.GetKeyDown("i") && modePlayer != "sword"){ 
        //      modePlayer = "sword";
        //        Player_Animator.SetTrigger("changeEquipement");
        // }
       

        playerIsMoving = movePlayer.x < 0 || movePlayer.x > 0 || movePlayer.y < 0 || movePlayer.y > 0;
        cameraIsTurning = rotate.x < 0 || rotate.x > 0 || rotate.y < 0 || rotate.y > 0;


        if(characterController.isGrounded && canMove){
            verticalVelocity = -player_gravity * Time.deltaTime;
        }else{
            verticalVelocity -= player_gravity * Time.deltaTime;
        }

        if(hasJump){
            verticalVelocity = use_multiple_jump ? (verticalVelocity + jumpForce) : jumpForce;
            hasJump = false;
        }

        characterController.Move(new Vector3(0f, verticalVelocity, 0f));

        if(playerIsMoving && canMove){ // Mouvement left stick

            Player_Animator.SetFloat("SpeedMove", (movePlayer.y));

            if(!cameraIsBehind){
                transform.localEulerAngles = new Vector3(0f, transform.localEulerAngles.y + camera_container.transform.localEulerAngles.y, 0f);
                camera_container.transform.localEulerAngles = new Vector3(camera_container.transform.localEulerAngles.x,0f,0f);
            }

            transform.Rotate(0, movePlayer.x * speedRotation  * Time.deltaTime, 0, Space.World); // rotate right/left character.
            transform.Translate(new Vector3(0f, 0f, movePlayer.y) * SpeedMove  * Time.deltaTime * (movePlayer.y < 0 ? 0.5f : 1f), Space.Self);
        }else{
            Player_Animator.SetFloat("SpeedMove", 0);
        }

        // PAS TOUCHE CONNARD
        if(cameraIsTurning){ // Mouvement right stick
            camera_container.transform.Rotate(0, rotate.x * speedRotation  * Time.deltaTime, 0, Space.World); // rotate right/left character.
            cameraIsBehind = false;
            if(rotate.y < -0.2 || rotate.y > 0.2){ // Rotate up/Down camera.
                float camera_Y = rotate.y * speedRotation/5  * Time.deltaTime;
                float angle = camera_container.transform.localEulerAngles.x;
                angle = (angle > 180) ? angle - 360 : angle;
                camera_Y = angle < -40 && rotate.y > 0 ? 0 : angle > 20 && rotate.y < 0?  0 : camera_Y;
                camera_container.transform.Rotate(-camera_Y, 0, 0, Space.Self);
            }
        }else if(playerIsMoving){
                StopAllCoroutines();

            if(camera_container.transform.localEulerAngles.y >= 0.5f || camera_container.transform.localEulerAngles.y <= -0.5f){
                float diff = camera_container.transform.localEulerAngles.y;     
                diff -= diff > 180f ? 360f : 0f;
                camera_container.transform.localEulerAngles = new Vector3(0f,diff/1.02f,0f);

            }else if(camera_container.transform.localEulerAngles.y != 0f){
                cameraIsBehind = true;
                camera_container.transform.localEulerAngles = new Vector3(camera_container.transform.localEulerAngles.x,0f,0f);
            }

        }
        
    }

    void Jump(){
        if((Player_Animator.GetBool("Grounded") || use_multiple_jump) && canJump){
            hasJump = true;
            Player_Animator.SetBool("Grounded", false);
            Player_Animator.SetBool("initiate_jump", true); 
        }
    }

    void ButtonB(){

        if(canAttack){
            if(!isBowman){
                Player_Animator.SetTrigger("attackSword1");
              
            }
            else if(isBowman){
                Player_Animator.SetTrigger("attackBow");
            }
        }
    }

    void OnEnable(){

        controls.Gameplay.Enable();
    }

    void OnDisable(){

        controls.Gameplay.Disable();
    }


    void OnControllerColliderHit(ControllerColliderHit hit)
    {  
        if(hit.gameObject.layer == 10 && !Player_Animator.GetBool("Grounded")){
            Player_Animator.SetBool("Grounded", true);
        }
    }


    void OnTriggerEnter(Collider collider){

    //    Debug.Log("enter : " + collider.gameObject.name);

        if(collider.gameObject.tag == "degatPlayer"){
            Player_Animator.SetTrigger("getHit");
            float value = enemy_manager.instance.degatForPlayer;
            player_life.instance.change_player_life((int)value);
           // print("Player a recu "+value.ToString("f0")+" de degats");

            if(collider.gameObject.name== "FlecheEnemy(Clone)"){
                Destroy(collider.gameObject);
            }
        }
    }

     void OnTriggerStay(Collider collider){
       // Debug.Log("stay : " + collider.gameObject.name);

        // if(!Player_Animator.GetBool("Grounded") && collider.gameObject.layer == 10){ 
        //     Debug.Log("STAY");
        //     Player_Animator.SetBool("Grounded", true);
        // }
    }
    
    void OnTriggerExit(Collider collider){

       
      //  Debug.Log("exit : " + collider.gameObject.name);
        // if(collider.gameObject.layer == 10){
        //     Debug.Log("EXIT");
        //     Player_Animator.SetBool("Grounded", false);
        //     Player_Animator.SetBool("initiate_jump", true); 
        // }
    }


    void ShootArrow(){ // called par anim

      //  enemy_manager.instance.degatForPlayer = degatMax;
        GameObject ProjectileClone = Instantiate(projectile,originArrow.position, originArrow.rotation);
        ProjectileClone.GetComponent<Rigidbody>().AddForce(originArrow.right *puissance_de_tir, ForceMode.Impulse);
        Destroy(ProjectileClone,5);    
    }

      void changeEquipement(){ // declenchee par anim

        if(modePlayer == "noweapon"){ 
            Player_Animator.SetLayerWeight (1, 0); // layer 1 Sword
            Player_Animator.SetLayerWeight (2, 0); // layer 2 Bow
            Bow.SetActive(false);
            Arrow.SetActive(false);
            Sword.SetActive(false);
            Shield.SetActive(false);
            isBowman = false;
        }

        if(modePlayer == "sword"){  // 1 layer Sword, weight pour la priorite
            Player_Animator.SetLayerWeight (1, 1); // layer 1 Sword
            Player_Animator.SetLayerWeight (2, 0); // layer 2 Bow
            Bow.SetActive(false);
            Arrow.SetActive(false);
            Sword.SetActive(true);
            Shield.SetActive(true);
            isBowman = false;
        }

        if(modePlayer == "bow"){ // 1 layer Sword, weight pour la priorite 
            Player_Animator.SetLayerWeight (1, 0); // layer 1 Sword
            Player_Animator.SetLayerWeight (2, 1); // layer 2 Bow
            Bow.SetActive(true);
            Arrow.SetActive(true);
            Sword.SetActive(false);
            Shield.SetActive(false);
            isBowman = true;
        }
    }

     void isAttackStart(){

        PlayerIsAttack = true;

    }
     void isAttackEnd(){

        PlayerIsAttack = false;

    }
 
 

 


    
}

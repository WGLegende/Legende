using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Cinemachine.Utility;

public class player_gamePad_manager : MonoBehaviour
{
    
    public static player_gamePad_manager instance; 
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

    //bool isBowman;
   // bool isShooting;
   // public string modePlayer = "sword";

    public int SpeedMove;
   

    Rigidbody player_rigidBody;
    public Animator Player_Animator;
    public bool player_is_moving;
  
    public bool canAttack;
    public bool canMove;
    public bool canJump;

    public bool PlayerIsAttack;

    private CharacterController characterController;

    private float verticalVelocity;
    public float player_gravity = 1f;
    public float jumpForce = 0.2f;
    private bool hasJump = false;


    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    Transform cam;
    public Transform Player;
    
    public float force_degat_recul = 3f;

    private float lastY;
    public float FallingThreshold = -0.01f;  
    public bool falling = false; 


    void Start(){

         lastY = transform.position.y;

        if(instance == null){
            instance = this;
        }

        cam = GameObject.Find("Camera").GetComponent<Transform>();
        Player_Animator = GetComponent<Animator>();  
        canAttack = true;
        canMove = true;
        canJump = true;
        characterController = GetComponent<CharacterController>();
        player_gravity/=10f;
        jumpForce/=10f;   

    }

    void Update()
    {
        if(Input.GetKeyDown("i")){

            StartCoroutine(ImpactPlayer(force_degat_recul));
            print("test impact recul");
        }  
    }

    IEnumerator ImpactPlayer(float force){
        float timer = 1;
        while(timer < 2){
            characterController.Move(Vector3.forward * Time.deltaTime * force);
            timer += timer * Time.deltaTime * 2;
            yield return null;
        }
    }


    public void player_velocity_calculation(){

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


        if(!player_is_moving || !canMove){

            Player_Animator.SetFloat("SpeedMove", 0);
            Player_Animator.SetFloat("walkSide", 0);  
            Player_sound.instance.StopMove(); // Sound Player
        } 

        float distancePerSecondSinceLastFrame = (transform.position.y - lastY) * Time.deltaTime;
        lastY = transform.position.y;  //set for next frame
        if (distancePerSecondSinceLastFrame < FallingThreshold && !falling && canJump){
            falling = true; 
            Player_Animator.SetBool("Grounded", false);
            Player_Animator.SetTrigger("is_falling");
            Player_sound.instance.StopMove(); // Sound Player
        }
        
        
    }


    public void player_movement(float left_stick_x, float left_stick_y){


        if(canMove){
            
            Vector3 direction = new Vector3(left_stick_x,0f,left_stick_y);
            float targetAngle  = Mathf.Atan2(direction.x, direction.z)* Mathf.Rad2Deg + cam.eulerAngles.y;
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            Vector3 playerAngle = Player.forward;
            Vector3 camAngle = cam.transform.forward;
            playerAngle.y = 0;
            camAngle.y = 0;
            float horizDiffAngle = Vector3.Angle(playerAngle, camAngle);
            print (direction.magnitude);

            
            characterController.Move(moveDir* direction.magnitude* SpeedMove* Time.deltaTime);

        
            // Animations Deplacement XY sans rotation
            if(lockTarget.instance.target_lock){

                Player_Animator.SetFloat("SpeedMove", left_stick_y);
                Player_Animator.SetFloat("walkSide", horizDiffAngle); 
            }
            // deplacement libre
            else{
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                if(left_stick_x != 0 || left_stick_y != 0){
                transform.rotation = Quaternion.Euler(0f,angle,0f);
                }
                    player_gamePad_manager.instance.Player_Animator.SetFloat("Blend", direction.magnitude, 0.1f, Time.deltaTime); 

                Player_Animator.SetFloat("walkSide", 0);   
            }

            // Son bruitage step
            if(characterController.isGrounded){ 
                if(direction.magnitude > 0 && direction.magnitude <= 0.7){
                    Player_sound.instance.Walk();
                }
                else if(direction.magnitude > 0.7 ){
                    Player_sound.instance.Run();
                } 
            }
        }
    }
    
    public void put_camera_behind_player(){

        Camera_control.instance.CameraBehindPlayer();
        lockTarget.instance.target_lock = true; 
    }


    public void player_jump(){
        if((Player_Animator.GetBool("Grounded") || use_multiple_jump) && canJump ){
            hasJump = true;
            canJump = false;
            Player_Animator.SetBool("Grounded", false);
            Player_Animator.SetTrigger("jump"); // test
            Player_Animator.SetBool("initiate_jump", true); 
            Player_sound.instance.StopMove(); // Sound Player
        }
    }

    public void player_attack(){
        if(canAttack){ 
            Player_Animator.SetTrigger("attack");  
        }
        if(EnemyDefense.instance != null)// on renseigne aux enemy si player attack
            enemy_manager.instance.playerAttack();
    }

    public void position_bowman(bool value){

        if(player_equipement.instance.mode_player != 1) // si en mode bow
        return;

        if(value){
            Player_Animator.SetBool("start_attack_bow",value);
            Camera_control.instance.cam_bow.Priority = 11; 
            Camera_control.instance.CameraBehindPlayer();
        }else{
            Player_Animator.SetBool("start_attack_bow",value);
            Camera_control.instance.cam_bow.Priority = 0; 
        } 
    }

    public void PlayerCanMove(bool value){
        canMove = value;
        canAttack = value;
        canJump = value;
    }


    void OnControllerColliderHit(ControllerColliderHit hit){  
        if(hit.gameObject.layer == 10 && !Player_Animator.GetBool("Grounded")){
            Player_Animator.SetBool("Grounded", true);
            falling = false;
            StartCoroutine(end_anim_Jump());
        }
    }

    IEnumerator end_anim_Jump(){
        yield return new WaitForSeconds(0.4f);
        canJump = true;
    }


    void OnTriggerEnter(Collider collider){

        if(collider.gameObject.tag == "degatPlayer"){

            Player_Animator.SetTrigger("getHit");
            //StartCoroutine(ImpactPlayer(force_degat_recul));
            float value = enemy_manager.instance.degatForPlayer;
            player_main.instance.DegatPlayerPv(value);   
           
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

    void OnParticleCollision(GameObject other){

        if(other.gameObject.tag == "degatPlayer"){

            Player_Animator.SetTrigger("getHit");
            StartCoroutine(ImpactPlayer(force_degat_recul));
            float value = enemy_manager.instance.degatForPlayer;
            player_main.instance.DegatPlayerPv(value);      
        }
    }



    // declenchee par anim
    void ShootArrow(){

        Player_sound.instance.PlayFightFx(gameObject,Player_sound.instance.FightFx[1]);
        player_equipement.instance.nbr_fleche--;
        Arrow.SetActive(false);
        if(player_equipement.instance.nbr_fleche <=0){
        player_equipement.instance.nbr_fleche = 0;
        return;
        }
        GameObject ProjectileClone = Instantiate(projectile,originArrow.position, originArrow.rotation);
        ProjectileClone.GetComponent<Rigidbody>().AddForce(originArrow.right * puissance_de_tir, ForceMode.Impulse);
        Destroy(ProjectileClone,5); 
        Invoke("arrow_display",0.2f);
    }

    void arrow_display(){
        Arrow.SetActive(true);
    }


}

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

    bool isBowman;
    bool isShooting;
    public string modePlayer = "sword";

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
    
    public float force_degat_recul = 3f;


    void Start(){

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

        //Player_Animator.SetLayerWeight (1, 0); // layer 1 Sword
       // Player_Animator.SetLayerWeight (2, 0); // layer 2 Bow
       
        changeEquipement();
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
    }


    public void player_movement(float left_stick_x, float left_stick_y){

        if(canMove){
            
            Vector3 direction = new Vector3(left_stick_x,0f,left_stick_y);
            float targetAngle  = Mathf.Atan2(direction.x, direction.z)* Mathf.Rad2Deg + cam.eulerAngles.y;
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            characterController.Move(moveDir* direction.magnitude* SpeedMove* Time.deltaTime);


            // Deplacement XY sans rotation
            if(lockTarget.instance.target_lock){

                Player_Animator.SetFloat("SpeedMove", left_stick_y);
                Player_Animator.SetFloat("walkSide", left_stick_x); 
            }
            // deplacement libre
            else{
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f,angle,0f);
                Player_Animator.SetFloat("SpeedMove", direction.magnitude); 
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
        if((Player_Animator.GetBool("Grounded") || use_multiple_jump) && canJump){
            hasJump = true;
            Player_Animator.SetBool("Grounded", false);
            Player_Animator.SetBool("initiate_jump", true); 
            Player_sound.instance.StopMove(); // Sound Player
        }
    }

    public void player_attack(){
        if(canAttack){
            if(!isBowman){
                Player_Animator.SetTrigger("attackSword1");
            }
            else if(isBowman){
                Player_Animator.SetTrigger("attackBow");
            }
        }
        if(EnemyDefense.instance != null)
            enemy_manager.instance.playerAttack();
    }

    public void PlayerCanMove(bool value){

        canMove = value;
        canAttack = value;
        canJump = value;
    }


    void OnControllerColliderHit(ControllerColliderHit hit)
    {  
        if(hit.gameObject.layer == 10 && !Player_Animator.GetBool("Grounded")){
            Player_Animator.SetBool("Grounded", true);
        }
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

        //Player_sound.instance.PlayFightFx(gameObject,Player_sound.instance.FightFx[1]);
        GameObject ProjectileClone = Instantiate(projectile,originArrow.position, originArrow.rotation);
        ProjectileClone.GetComponent<Rigidbody>().AddForce(originArrow.right *puissance_de_tir, ForceMode.Impulse);
        Destroy(ProjectileClone,5);    
    }


   // declenchee par anim
    public void changeEquipement(){ 
       
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


 


























}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public int speedRotation;
    public GameObject camera_container;

    Rigidbody player_rigidBody;
    public Animator Player_Animator;
    public bool player_is_moving;
    public bool camera_is_turning;
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

   

    void Start(){
        if(instance == null){
            instance = this;
        }

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
        // Bow.SetActive(false);
        // Arrow.SetActive(false);
        // Sword.SetActive(false);
        // Shield.SetActive(false);
        changeEquipement(modePlayer);
       
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

            Player_sound.instance.StopMove(); // Sound Player
        }
       
           
    }




    public void player_movement(float right_stick_x, float right_stick_y){
        if(canMove){
            Player_Animator.SetFloat("SpeedMove", (right_stick_y));

           // Son bruitage step
            if(characterController.isGrounded){ 
            
                if(right_stick_y > 0 && right_stick_y <= 0.7){ Player_sound.instance.Walk();}
                else if(right_stick_y > 0.7 ){ Player_sound.instance.Run();}
                else if(right_stick_y < 0 ){ Player_sound.instance.Run();}
            }
                
            if(!cameraIsBehind){
                transform.localEulerAngles = new Vector3(0f, transform.localEulerAngles.y + camera_container.transform.localEulerAngles.y, 0f);
                camera_container.transform.localEulerAngles = new Vector3(camera_container.transform.localEulerAngles.x,0f,0f);
            }
            transform.Rotate(0, right_stick_x * speedRotation  * Time.deltaTime, 0, Space.World); // rotate right/left character.
            transform.Translate(new Vector3(0f, 0f, right_stick_y) * SpeedMove  * Time.deltaTime * (right_stick_y < 0 ? 0.5f : 1f), Space.Self);
        }

        if(!camera_is_turning){ // to delete ?
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



    public void player_camera(float left_stick_x, float left_stick_y){
        camera_container.transform.Rotate(0, left_stick_x * speedRotation  * Time.deltaTime, 0, Space.World); // rotate right/left character.
        cameraIsBehind = false;
        if(left_stick_y < -0.2 || left_stick_y > 0.2){ // Rotate up/Down camera.
            float camera_Y = left_stick_y * speedRotation/5  * Time.deltaTime;
            float angle = camera_container.transform.localEulerAngles.x;
            angle = (angle > 180) ? angle - 360 : angle;
            camera_Y = angle < -40 && left_stick_y > 0 ? 0 : angle > 20 && left_stick_y < 0?  0 : camera_Y;
            camera_container.transform.Rotate(-camera_Y, 0, 0, Space.Self);
        }
    }

    public void put_camera_behind_player(){
        camera_container.transform.localEulerAngles = new Vector3(0f,0f,0f);
    }

    public void player_jump(){
        if((Player_Animator.GetBool("Grounded") || use_multiple_jump) && canJump){
            hasJump = true;
            Player_Animator.SetBool("Grounded", false);
            Player_Animator.SetBool("initiate_jump", true); 
            // Player_sound.instance.StopMove(); // Sound Player
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


    void OnControllerColliderHit(ControllerColliderHit hit)
    {  
        if(hit.gameObject.layer == 10 && !Player_Animator.GetBool("Grounded")){
            Player_Animator.SetBool("Grounded", true);
        }
    }


    void OnTriggerEnter(Collider collider){

        if(collider.gameObject.tag == "degatPlayer"){

            Player_Animator.SetTrigger("getHit");
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
            float value = enemy_manager.instance.degatForPlayer;
            player_main.instance.DegatPlayerPv(value);      
        }
    }



    // declenchee par anim
    void ShootArrow(){

        Player_sound.instance.PlayFightFx(gameObject,Player_sound.instance.FightFx[1]);
        GameObject ProjectileClone = Instantiate(projectile,originArrow.position, originArrow.rotation);
        ProjectileClone.GetComponent<Rigidbody>().AddForce(originArrow.right *puissance_de_tir, ForceMode.Impulse);
        Destroy(ProjectileClone,5);    
    }


   // declenchee par anim
    public void changeEquipement(string value){ 
       
        if(value == "noweapon"){ 
            Player_Animator.SetLayerWeight (1, 0); // layer 1 Sword
            Player_Animator.SetLayerWeight (2, 0); // layer 2 Bow
            Bow.SetActive(false);
            Arrow.SetActive(false);
            Sword.SetActive(false);
            Shield.SetActive(false);
            isBowman = false;
        }

        if(value == "sword"){  // 1 layer Sword, weight pour la priorite
            Player_Animator.SetLayerWeight (1, 1); // layer 1 Sword
            Player_Animator.SetLayerWeight (2, 0); // layer 2 Bow
            Bow.SetActive(false);
            Arrow.SetActive(false);
            Sword.SetActive(true);
            Shield.SetActive(true);
            isBowman = false;
        }

        if(value == "bow"){ // 1 layer Sword, weight pour la priorite 
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

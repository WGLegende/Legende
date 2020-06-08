using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player_main : MonoBehaviour

{
    # region Singleton

    public static player_main instance;

    void Awake()
    {
        instance = this;
    }

    #endregion

    public GameObject player;
    Animator anim;
    Animator blackout;

    public float player_current_pv;
    float player_max_pv = 100f;

    Transform playerTransform;
    Transform cam;
    Vector3 startPosition;
    Quaternion startRotation;

    public bool playerIsAlive = true;


    CharacterController controller;
    public float timeInAir = 0f;
    public float deathTimer = 3f;

 
    void Start(){ 

        playerTransform = GameObject.Find("Player").GetComponent<Transform>(); 
        anim = player.GetComponent<Animator>();
        blackout = GameObject.Find("black").GetComponent<Animator>();
        cam = GameObject.Find("cam_container").GetComponent<Transform>(); 
        startPosition = player.transform.position;
        startRotation = player.transform.rotation; 

        controller = GameObject.Find("Player").GetComponent<CharacterController>();
    }

   
    void Update(){

        if(Input.GetKeyDown("k")){

            DegatPlayerPv(50);
        }
        if(Input.GetKeyDown("space")){

            AddPlayerPv(100);
        }


        checkIfPlayerIsFalling();
      
    }

    public void AddPlayerPv(float value){

        player_current_pv += value;
        player_current_pv = Mathf.Clamp(player_current_pv, 0, 100); 
        Barre_de_Vie.instance.RefreshPvPlayerUI(player_current_pv);
    }

    public void DegatPlayerPv(float value){

        if(playerIsAlive){

            player_current_pv -= value;
            player_current_pv = Mathf.Clamp(player_current_pv, 0, 100); 
            Barre_de_Vie.instance.RefreshPvPlayerUI(player_current_pv);
        
            if(player_current_pv <= 0){
                
                StartCoroutine(playerDie());  
                playerIsAlive = false;   
                hinput.gamepad[0].StopVibration();       
            }
        }
    }


    IEnumerator playerDie(){ 

        if (timeInAir < deathTimer){

            anim.SetBool("isDead",true);
            player_gamePad_manager.canMove = false;
            player_gamePad_manager.canAttack = false;

            yield return new WaitForSeconds(1.7f); // duree anim die
        }

        blackout.SetBool("blackout",true);

        timeInAir = 0f;
        

        yield return new WaitForSeconds(1f); 

        saveEnemy.instance.restoreEnemy();

        if (level_main.instance.hasCheckPoint){
            level_main.instance.MovePlayerToCheckpoint();
        }

        else{
            playerTransform.transform.position = startPosition;
            playerTransform.transform.rotation = startRotation;
            cam.transform.localEulerAngles = new Vector3(0f,0f,0f);
            player_main.instance.AddPlayerPv(100); 
        }

        blackout.SetBool("blackout",false);

        anim.SetBool("isDead",false);
        playerIsAlive = true;   
          
        yield return new WaitForSeconds(2.5f); // durre anim recoverDie

        player_gamePad_manager.canMove = true;
        player_gamePad_manager.canAttack = true;
    }



    void checkIfPlayerIsFalling(){

        timeInAir = controller.isGrounded ? timeInAir = 0f : timeInAir += Time.deltaTime;

        if (timeInAir >= deathTimer){
            StartCoroutine(playerDie());  
            playerIsAlive = false;
        }

        if (climbtest.instance.canClimb){
             timeInAir = 0f;
        }
    }

}

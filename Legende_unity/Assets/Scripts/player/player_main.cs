using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player_main : MonoBehaviour

{
    public static player_main instance;

    public GameObject player;
    public GameObject playerKart;
    public GameObject kart;
   
    Animator anim;
    Animator blackout;

    public float player_current_pv;
    float player_max_pv = 100f;
   
    Vector3 startPosition;
    Quaternion startRotation;

    public bool playerIsAlive = true;

    CharacterController controller;
    public float timeInAir = 0f;
    public float deathTimer = 3f;

 
    void Awake(){ 

        instance = this;
        player = GameObject.Find("Player");
        anim = player.GetComponent<Animator>();
        blackout = GameObject.Find("black").GetComponent<Animator>();
        controller = GameObject.Find("Player").GetComponent<CharacterController>();
        startPosition = player.transform.position;
        startRotation = player.transform.rotation; 
    }

   
    void Update(){

        if(Input.GetKeyDown("k")){

            DegatPlayerPv(50);
        }
      
        if(player.activeSelf){
           // checkIfPlayerIsFalling();
        }
        
      
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
                Hinput.gamepad[0].StopVibration();       
            }
        }
    }


    IEnumerator playerDie(){ 

        if (timeInAir < deathTimer){

            anim.SetBool("isDead",true);
            player_gamePad_manager.instance.PlayerCanMove(false);
           
            yield return new WaitForSeconds(1.7f); // duree anim die
        }

        blackout.SetBool("blackout",true);

        timeInAir = 0f;
        
        yield return new WaitForSeconds(1f); // Black Ui

        saveEnemy.instance.restoreEnemy();

        if (level_main.instance.hasCheckPoint){
            level_main.instance.MovePlayerToCheckpoint();
        }

        else{
            player.transform.position = startPosition;
            player.transform.rotation = startRotation;
            player_main.instance.AddPlayerPv(100); 
        }
        player_gamePad_manager.instance.put_camera_behind_player();

        blackout.SetBool("blackout",false); // Back to game

        anim.SetBool("isDead",false);
        playerIsAlive = true;   
          
        yield return new WaitForSeconds(2.5f); // duree anim recoverDie

        player_gamePad_manager.instance.PlayerCanMove(true);
    }



    void checkIfPlayerIsFalling(){

        timeInAir = controller.isGrounded ? timeInAir = 0f : timeInAir += Time.deltaTime;

        if(timeInAir > 0.7f){ // todo test anim lorsque player quitte le sol sans sauter
           anim.SetTrigger("is_falling");
           Player_sound.instance.StopMove(); // Sound Player
        }

        if (timeInAir >= deathTimer){
            StartCoroutine(playerDie());  
            playerIsAlive = false;
        }

        if (climbtest.instance.canClimb){
            timeInAir = 0f;
        }
    }

}

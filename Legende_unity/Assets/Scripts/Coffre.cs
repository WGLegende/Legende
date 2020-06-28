using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coffre : MonoBehaviour
{

    Animator anim;
    bool isOpen;
    public bool petit_coffre;
    public BoxCollider Object;
  
    void Start(){

        anim = GetComponent<Animator>(); 
     
    }

    void OnTriggerEnter(Collider collider){ 

        if(collider.tag == "Player" && !isOpen){
            ButtonAction.instance.Action("Ouvrir"); 
        }
    }
  
    void OnTriggerStay(){

        if(hinput.anyGamepad.A.justPressed && !isOpen){

            isOpen = true;
            ButtonAction.instance.Hide(); 

            if(!petit_coffre){

                anim.SetTrigger("OpenCoffre");
                player_gamePad_manager.canMove = false;
                player_gamePad_manager.canAttack = false;
                player_gamePad_manager.canJump = false;
                Player_sound.instance.PlayMusicEventPlayer(Player_sound.instance.MusicEventPlayer[0]); 
                StartCoroutine(FadeMixer.StartFade(Music_sound.instance.MusicMaster, "musicMasterVolume", 2f , 0f)); // cut zic
            }
            else{
                anim.SetTrigger("OpenPetitCoffre");
                Invoke("activeObject",0.5f);
            }
        }
    }

    void OnTriggerExit(Collider collider){
        if(collider.tag == "Player"){
          ButtonAction.instance.Hide(); 
        }
    }



    void finAnim(){ // declenchee en fin anim Grand Coffre

    
        player_gamePad_manager.canMove = true;
        player_gamePad_manager.canAttack = true;
        player_gamePad_manager.canJump = true;
        StartCoroutine(FadeMixer.StartFade(Music_sound.instance.MusicMaster, "musicMasterVolume", 2f , 20f)); // remove zic
        
    }

    void activeObject(){

        Object.enabled = true;
    }

    
}

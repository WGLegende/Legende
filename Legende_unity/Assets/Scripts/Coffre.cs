using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coffre : MonoBehaviour
{

    [HideInInspector] public Animator anim;
    [HideInInspector] public bool isOpen;
    public bool petit_coffre;
    public BoxCollider Object;
  
    void Start(){

        anim = GetComponent<Animator>(); 
    }



    void OnTriggerEnter(Collider collider){ 
        if(!isOpen){
            player_actions.instance.display_actions(this,collider);  
        }
    }
  
   
    void OnTriggerExit(Collider collider){
        player_actions.instance.clear_action(collider.tag == "Player");  
    }



    void finAnim(){ // declenchee en fin anim Grand Coffre

        player_gamePad_manager.instance.PlayerCanMove(true);
        StartCoroutine(FadeMixer.StartFade(Music_sound.instance.MusicMaster, "musicMasterVolume", 2f , 20f)); // remove zic  
    }


    public void activeObject(){
        Object.enabled = true;
    }

    
}

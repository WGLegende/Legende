using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coffre : MonoBehaviour
{

    [HideInInspector] public Animator anim;
    [HideInInspector] public bool isOpen;
    public bool petit_coffre;
    BoxCollider object_collider;
    inventory_object object_a_recuperer;
  
    void Start(){

        anim = GetComponent<Animator>(); 
        object_a_recuperer = GetComponentInChildren<inventory_object>();
        if(object_a_recuperer == null){
            Debug.Log("Pas d'object dans le coffre !");
        }else
        object_collider = object_a_recuperer.GetComponent<BoxCollider>();
        object_a_recuperer.transform.localPosition = new Vector3(0f,0.07f,0f);
        
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
        object_collider.enabled = true;
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AscenseurSwitch : MonoBehaviour{

    [HideInInspector] public bool isPositionUp = true;
    [HideInInspector] public bool toggle_levier;
    [HideInInspector] public Animator anim_levier;
    [HideInInspector] public BoxCollider col;
  

    public GameObject elevator;
    [HideInInspector] public Ascenseur elevator_script;
    [HideInInspector] public Animator anim_elevator;

    [HideInInspector]public AudioSource sound_levier;

   
   
  
    void Start(){

        anim_levier = GetComponent<Animator>(); 
        elevator_script = elevator.GetComponent<Ascenseur>();
        anim_elevator = elevator.GetComponent<Animator>();
        sound_levier = GetComponent<AudioSource>();
        col = GetComponent<BoxCollider>();   
    }
   

    void OnTriggerEnter(Collider collider){ 

        if(!ame_player.instance.navy_en_attente){
            player_actions.instance.display_actions(this,collider);  
        }  
    }
  
   
    void OnTriggerExit(Collider collider){
        player_actions.instance.clear_action(collider.tag == "Player");  
    }

    void enableCollider(){ // declenchee en fin anim

        col.enabled = true;

    }

    
   

}

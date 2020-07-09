using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AscenseurSwitch : MonoBehaviour{

    [HideInInspector] public bool isPositionUp = true;
    [HideInInspector] public bool toggle_levier;
    [HideInInspector] public Animator anim_levier;
    [HideInInspector] public BoxCollider collider_is_disable;

    public GameObject elevator;
    [HideInInspector] public Ascenseur elevator_script;
    [HideInInspector] public Animator anim_elevator;

    public AudioSource sound;


   

    void Start(){

        anim_levier = GetComponent<Animator>(); 
        collider_is_disable = GetComponent<BoxCollider>();
        elevator_script = elevator.GetComponent<Ascenseur>();
        anim_elevator = elevator.GetComponent<Animator>();
        sound = GetComponent<AudioSource>();
    }
   

    void OnTriggerEnter(Collider collider){ 

        player_actions.instance.display_actions(this,collider);  
        
    }
  
   
    void OnTriggerExit(Collider collider){

        player_actions.instance.clear_action(collider.tag == "Player");  
    }
   

}

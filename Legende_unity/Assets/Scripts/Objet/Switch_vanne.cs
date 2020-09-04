using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Switch_vanne : MonoBehaviour{
    
    public GameObject vanne;
    public GameObject elevator;
    [HideInInspector] public bool oneShot;

    [HideInInspector] public Animator anim_vanne;
    [HideInInspector] public AudioSource sound_vanne;
    [HideInInspector] public Ascenseur elevator_script;

    [HideInInspector] public Animator anim_levier;
    [HideInInspector] public AudioSource sound_levier;

    
    void Start(){

        anim_vanne = vanne.GetComponent<Animator>();
        sound_vanne =vanne.GetComponent<AudioSource>();

        elevator_script = elevator.GetComponent<Ascenseur>();

        anim_levier = GetComponent<Animator>(); 
        sound_levier = GetComponent<AudioSource>();
    }

   
    void OnTriggerEnter(Collider collider){ 

        if(!oneShot){
            player_actions.instance.display_actions(this,collider);
        }   
    }
  
   
    void OnTriggerExit(Collider collider){
        player_actions.instance.clear_action(collider.tag == "Player");  
    }
}

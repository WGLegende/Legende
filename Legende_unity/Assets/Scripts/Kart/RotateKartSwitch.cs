using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateKartSwitch : MonoBehaviour{

    [HideInInspector] public bool isPositionUp = true;
    [HideInInspector] public bool toggle_levier;
    [HideInInspector] public Animator anim_levier;
    [HideInInspector] public Animator anim_kart;
    [HideInInspector] public AudioSource sound;
    [HideInInspector] public GareKart gare_kart_script;

    void Start(){

        anim_levier = GetComponent<Animator>(); 
        anim_kart = GameObject.Find("kart").GetComponent<Animator>();
        sound = GetComponent<AudioSource>(); 
        gare_kart_script = GetComponentInParent<GareKart>(); 
    }
   

    void OnTriggerEnter(Collider collider){ 

        if(gare_kart_script.kart_in_station){ // on check si le kart est dans la gare pour activer la rotation
            player_actions.instance.display_actions(this,collider);   
        }  
    }
  
   
    void OnTriggerExit(Collider collider){

        player_actions.instance.clear_action(collider.tag == "Player");  
    }

    void enableCollider(){ // declenchee en fin anim


    }

}

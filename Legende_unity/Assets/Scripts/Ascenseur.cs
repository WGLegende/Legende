using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ascenseur : MonoBehaviour{

    public bool isPositionUp = true;
    public Animation anim;


    void Start(){

        anim = GetComponent<Animation>();
        isPositionUp = true;
    }
   

    void OnTriggerEnter(Collider collider){ 

        player_actions.instance.display_actions(this,collider);  
        
    }
  
   
    void OnTriggerExit(Collider collider){

        player_actions.instance.clear_action(collider.tag == "Player");  
    }








    void elevatorPositionDown(){ // on appelle la fonction en fin d'amim
        isPositionUp = false;   
    }

    void elevatorPositionUp(){
        isPositionUp = true;  
    }
}

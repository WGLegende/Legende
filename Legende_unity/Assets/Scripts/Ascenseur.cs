using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ascenseur : MonoBehaviour{

    [HideInInspector] public bool isPositionUp = true;
    [HideInInspector] public Animator anim_elevator;
    public float vitesse = 2f;


    void Start(){

        anim_elevator = GetComponent<Animator>();
        anim_elevator.SetFloat("vitesse_deplacement",vitesse);
          
    }
   
    void elevatorPositionDown(){ // on appelle la fonction en fin d'amim
        isPositionUp = false;   
    }

    void elevatorPositionUp(){
        isPositionUp = true;  
    }

}

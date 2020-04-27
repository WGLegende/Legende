using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    public bool isInteracting;
    public enum type_collision 
    {
        chariot, 
        bouton,
        coffre,
        porte,
        cuisine,
        consommable
    };
    public type_collision Objet;

    public string nomDeMonInterractable = "tapir";
    public string actionDeMonInterractable = "rugissement";


    void OnTriggerEnter(Collider other){
        if (other.name == "Player"){
            MainInteractable.instance.NewInterraction(GetComponent<Interactable>());
        }   
    }

    void OnTriggerExit(Collider other){
        if (other.name == "Player"){
            MainInteractable.instance. ExitInteractable();
        }  
    }

    
}

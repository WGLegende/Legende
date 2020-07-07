using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aiguillage_kart : MonoBehaviour{

    public static aiguillage_kart instance; 

    public Battlehub.MeshDeformer2.SplineBase rails, right_rails, left_rails;
   
    public ChoixCircuit _choix_circuit;
    public enum ChoixCircuit{
        Droite,
        Gauche        
    }

    [HideInInspector] public bool toggle;
    [HideInInspector] public Animator anim;



    void Start(){
        if(instance == null){
            instance = this;
        } 
        anim = GetComponent<Animator>();
    }
   
  

    void OnTriggerEnter(Collider collider){

        if(collider.gameObject.tag == "PlayerKart"){

            ButtonActionKart.instance.Action("Bifurquer");
            //player_actions.instance.display_actions(this,collider);   

                if(_choix_circuit == ChoixCircuit.Gauche){ 
                    AiguillageManager.instance.next_rails = left_rails;    
                }
                else{
                    AiguillageManager.instance.next_rails = right_rails;  
                }
          
            if(!AiguillageManager.instance.SaveTrajetKart.Contains(rails)){ // on save d'ou on vient
                AiguillageManager.instance.SaveTrajetKart.Add(rails); 
            }
        }   
    }



    void OnTriggerExit(Collider collider){
        if(collider.gameObject.tag == "PlayerKart"){
            ButtonActionKart.instance.Hide();  
            //player_actions.instance.clear_action(false);  
        }    
    }



    void OnTriggerStay(Collider collider){

        if(collider.gameObject.tag == "PlayerKart"){

            if(Input.GetKeyDown("joystick button 0")){ // A
                toggle = !toggle;

                if(toggle){
                    _choix_circuit = ChoixCircuit.Gauche;
                    AiguillageManager.instance.next_rails = left_rails;
                    anim.SetBool("switch",true);
                }
                else{
                    _choix_circuit = ChoixCircuit.Droite;
                    AiguillageManager.instance.next_rails = right_rails;
                    anim.SetBool("switch",false);
                }  
            }
        }    
    }

   
   
}

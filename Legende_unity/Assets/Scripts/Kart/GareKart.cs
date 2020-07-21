using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;


public class GareKart : MonoBehaviour{

    public static GareKart instance;

    public typeGare _type_gare;
    public enum typeGare{
        Depart,
        Station,
        Terminus       
    }

    public bool kart_in_station;

    [HideInInspector] public Transform chariot_container;
    [HideInInspector] public Animator ui_chariot;

    
    void Start(){
         
        if(instance == null){
            instance = this;
        }
    
        chariot_container = GameObject.Find("Chariot_Container").GetComponent<Transform>();
        ui_chariot = GameObject.Find("UI_Chariot").GetComponent<Animator>();
    }


    void OnTriggerEnter(Collider collider){

        if(collider.gameObject.tag == "PlayerKart" && Mathf.Abs(kart_manager.instance.vitesse_actuelle) <= 1){
            player_actions.instance.display_actions(this,collider);   
        }   
    }

    void OnTriggerStay(Collider collider){ 

        if(collider.gameObject.tag == "PlayerKart"){
            
            if(Mathf.Abs(kart_manager.instance.vitesse_actuelle) > 1 && _type_gare == typeGare.Station) 
            return; // si trop vite a une station, on fait rien

            if(!kart_in_station){

                kart_in_station = true;
                player_actions.instance.display_actions(this,collider);   

                if(_type_gare == typeGare.Depart){
                    kart_manager.instance.canMoveRecul = false;
                    StartCoroutine(auto_freinage());  
                }
                else if (_type_gare == typeGare.Terminus){
                    kart_manager.instance.canMoveAvance = false;
                    StartCoroutine(auto_freinage());   
                }
               
            }
        }
    }

    void OnTriggerExit(Collider collider) {
        
        if(collider.gameObject.tag =="PlayerKart"){   
            kart_in_station = false;  
            kart_manager.instance.canMoveAvance = true; 
            kart_manager.instance.canMoveRecul = true;
            player_actions.instance.clear_action_kart(true);  
        }
    } 

    public IEnumerator auto_freinage(){

        kart_manager.instance.frein_auto = true;
        kart_manager.instance.vitesse_demandee = 0f;

        while(kart_manager.instance.SplineFollow.Speed > 0f){
            kart_manager.instance.SplineFollow.Speed -= Time.deltaTime * 0.2f;
        }

        kart_manager.instance.frein_auto = false;
        kart_manager.instance.vitesse_actuelle = 0f;
        kart_manager.instance.SplineFollow.Speed = 0f;

        yield return null;
    }
}

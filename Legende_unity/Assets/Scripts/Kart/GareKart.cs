using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GareKart : MonoBehaviour
{

    public typeGare _type_gare;
    public enum typeGare{
        Depart,
        Station,
        End         
    }
    bool coroutine;
    
    void OnTriggerStay(Collider collider){ 

        if(collider.gameObject.name == "PlayerKart"){

            // si trop vite a une station, on fait rien
            if(Mathf.Abs(kart_manager.instance.vitesse_actuelle) > 1 && _type_gare == typeGare.Station) 
            return;

            EnterChariot.instance.kart_in_station = true;
            ButtonActionKart.instance.Action("Descendre"); 
       
            switch (_type_gare){
                case typeGare.Depart : kart_manager.instance.canMoveRecul = false;                    
                break;

                case typeGare.End : kart_manager.instance.canMoveAvance = false;
                                   // kart_manager.instance.frein(true);
                                    kart_manager.instance.SplineFollow.Speed = 0f;
                                    print("freinage auto fin ligne");
                                   
                break;
            }
        }

    }

    void OnTriggerExit(Collider collider) {
        
        if(collider.gameObject.name =="PlayerKart"){   
            EnterChariot.instance.kart_in_station = false;  
            kart_manager.instance.canMoveAvance = true; 
            kart_manager.instance.canMoveRecul = true;
            kart_manager.instance.velocity_chariot = 20f;
            ButtonActionKart.instance.Hide();
        }

    } 


    IEnumerator decelereKart(){

        float value =  kart_manager.instance.SplineFollow.Speed;
        print("startCoroutinr");
        coroutine = true;

        while(value > 0.5){

            value -= value * Time.deltaTime/10;
            kart_manager.instance.SplineFollow.Speed = value;
            print(value);
            if(value < 0.5){
                kart_manager.instance.SplineFollow.Speed = 0f;
            
         
               // StopAllCoroutines();
            } 
        }
        yield return null;
      //  coroutine = false;
           
    }

}

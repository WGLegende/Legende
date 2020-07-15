using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckPointKart : MonoBehaviour
{
    public static CheckPointKart instance;

    ParticleSystem particule;
   
    bool justOnce;
   // public float save_positon_kart;
    
    void Start(){
        
        instance = this;
        particule = GetComponent<ParticleSystem>();
       
    }

    void OnTriggerEnter(Collider other){

        if (other.name == "kart"){

            if(!justOnce){

                particule.startColor = Color.green;
                justOnce = true; 
               // save_positon_kart = kart_manager.instance.SplineFollow.T;
                level_main.instance.CheckpointSavePositionKart(kart_manager.instance.SplineFollow.T);
            }   
        }   
    }


}

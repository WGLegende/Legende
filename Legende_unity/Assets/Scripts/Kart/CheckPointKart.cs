using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckPointKart : MonoBehaviour
{
    public static CheckPointKart instance;

    ParticleSystem particule;
    bool justOnce;

    void Start(){
        
        instance = this;
        particule = GetComponent<ParticleSystem>();
       
    }

    void OnTriggerEnter(Collider other){

        if (other.name == "kart"){

            if(!justOnce){

                ParticleSystem.MainModule psmain;
                psmain = particule.main;
                psmain.startColor = Color.green;
                justOnce = true; 
                level_main.instance.CheckpointSavePositionKart(kart_manager.instance.SplineFollow.T);
            }   
        }   
    }


}
